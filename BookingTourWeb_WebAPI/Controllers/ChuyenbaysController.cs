using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyenbaysController : ControllerBase
    {
        private readonly DvmayBayContext _context;

        public ChuyenbaysController(DvmayBayContext context)
        {
            _context = context;
        }

        // GET: api/Chuyenbays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chuyenbay>>> GetChuyenbays()
        {
            if (_context.Chuyenbays == null)
            {
                return NotFound();
            }
            return await _context.Chuyenbays.ToListAsync();
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ChuyenbayViewModel>>> GetChuyenbays()
        //{
        //    var chuyenbays = await _context.Chuyenbays.ToListAsync();
        //        var chuyenbaysViewModel = chuyenbays.Select(chuyenbay => new ChuyenbayViewModel
        //        {
        //            MaChuyenBay = chuyenbay.MaChuyenBay,
        //            MaMayBay = chuyenbay.MaMayBay,
        //            GioBay = chuyenbay.GioBay.ToString("c"), // Chuyển đổi TimeSpan thành chuỗi
        //            NoiXuatPhat = chuyenbay.NoiXuatPhat,
        //            NoiDen = chuyenbay.NoiDen,
        //            NgayXuatPhat = chuyenbay.NgayXuatPhat,
        //            DonGia = chuyenbay.DonGia,
        //            SoLuongVeBsn = chuyenbay.SoLuongVeBsn,
        //            SoLuongVeEco = chuyenbay.SoLuongVeEco
        //        }).ToList();

        //    return chuyenbaysViewModel;
        //}



        [HttpGet("{id}")]
        public async Task<ActionResult<Chuyenbay>> GetChuyenbay(string id)
        {
          if (_context.Chuyenbays == null)
          {
              return NotFound();
          }
            var chuyenbay = await _context.Chuyenbays.FindAsync(id);

            if (chuyenbay == null)
            {
                return NotFound();
            }

            return chuyenbay;
        }

        // PUT: api/Chuyenbays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChuyenbay(string id, Chuyenbay chuyenbay)
        {
            if (id != chuyenbay.MaChuyenBay)
            {
                return BadRequest();
            }

            _context.Entry(chuyenbay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuyenbayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chuyenbays
        [HttpPost]
        public async Task<ActionResult<Chuyenbay>> PostChuyenbay(Chuyenbay chuyenbay)
        {
            if (_context.Chuyenbays == null)
            {
                return Problem("Entity set 'DvmayBayContext.Chuyenbays'  is null.");
            }
            _context.Chuyenbays.Add(chuyenbay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChuyenbayExists(chuyenbay.MaChuyenBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChuyenbay", new { id = chuyenbay.MaChuyenBay }, chuyenbay);
        }


        //[HttpPost]
        //public async Task<ActionResult<ChuyenbayViewModel>> PostChuyenbay(ChuyenbayViewModel chuyenbayViewModel)
        //{
        //    if (chuyenbayViewModel == null)
        //    {
        //        return BadRequest("Invalid data.");
        //    }

        //    // Chuyển đổi dữ liệu từ ChuyenbayViewModel thành Chuyenbay
        //    var chuyenbay = new Chuyenbay
        //    {
        //        MaChuyenBay = chuyenbayViewModel.MaChuyenBay,
        //        MaMayBay = chuyenbayViewModel.MaMayBay,
        //        // Chuyển đổi chuỗi GioBay thành TimeSpan
        //        GioBay = TimeSpan.ParseExact(chuyenbayViewModel.GioBay, "c", CultureInfo.InvariantCulture),
        //        NoiXuatPhat = chuyenbayViewModel.NoiXuatPhat,
        //        NoiDen = chuyenbayViewModel.NoiDen,
        //        NgayXuatPhat = chuyenbayViewModel.NgayXuatPhat,
        //        DonGia = chuyenbayViewModel.DonGia,
        //        SoLuongVeBsn = chuyenbayViewModel.SoLuongVeBsn,
        //        SoLuongVeEco = chuyenbayViewModel.SoLuongVeEco
        //    };

        //    _context.Chuyenbays.Add(chuyenbay);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ChuyenbayExists(chuyenbay.MaChuyenBay))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    // Trả về lớp ViewModel để hiển thị lại dữ liệu đã được lưu
        //    return CreatedAtAction("GetChuyenbay", new { id = chuyenbay.MaChuyenBay }, chuyenbayViewModel);
        //}




        // DELETE: api/Chuyenbays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChuyenbay(string id)
        {
            if (_context.Chuyenbays == null)
            {
                return NotFound();
            }
            var chuyenbay = await _context.Chuyenbays.FindAsync(id);
            if (chuyenbay == null)
            {
                return NotFound();
            }

            _context.Chuyenbays.Remove(chuyenbay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChuyenbayExists(string id)
        {
            return (_context.Chuyenbays?.Any(e => e.MaChuyenBay == id)).GetValueOrDefault();
        }
    }
}
