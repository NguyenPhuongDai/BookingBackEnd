using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTourWeb_WebAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingTourWeb_WebAPI.Controllers;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinChuyenBaysController : ControllerBase
    {
        private readonly DvmayBayContext _context;

        public ThongTinChuyenBaysController(DvmayBayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThongTinChuyenBay>>> GetThongTinChuyenBay()
        {
            //return await _context.ThongTinChuyenBay.ToListAsync();
            var thongtinchuyenbay = (from chuyenbay in _context.Chuyenbays
                                     join maybay in _context.Maybays
                                     on chuyenbay.MaMayBay equals maybay.MaMayBay
                                     select new ThongTinChuyenBay()
                                     {
                                         MaChuyenBay = chuyenbay.MaChuyenBay,
                                         MaMayBay = chuyenbay.MaMayBay,
                                         TenMayBay = maybay.TenMayBay,
                                         NoiXuatPhat = chuyenbay.NoiXuatPhat,
                                         NoiDen = chuyenbay.NoiDen,
                                         NgayXuatPhat = chuyenbay.NgayXuatPhat,
                                         GioBay = chuyenbay.GioBay,
                                         SoLuongVeBsn = chuyenbay.SoLuongVeBsn,
                                         SoLuongVeEco = chuyenbay.SoLuongVeEco,
                                         DonGia = chuyenbay.DonGia,
                                     })
            .ToListAsync();
            //return Ok(thongtinchuyenbay);
            return await thongtinchuyenbay;
        }
        //===========================================================================

        [HttpGet("{maChuyenBay}")]
        public async Task<ActionResult<IEnumerable<ThongTinChuyenBay>>> GetThongTinChuyenBayByMaChuyenBay(string maChuyenBay)
        {
            var thongtinchuyenbay = await (from chuyenbay in _context.Chuyenbays
                                           join maybay in _context.Maybays
                                           on chuyenbay.MaMayBay equals maybay.MaMayBay
                                           where chuyenbay.MaChuyenBay.Contains(maChuyenBay)
                                           select new ThongTinChuyenBay()
                                           {
                                               MaChuyenBay = chuyenbay.MaChuyenBay,
                                               MaMayBay = chuyenbay.MaMayBay,
                                               TenMayBay = maybay.TenMayBay,
                                               NoiXuatPhat = chuyenbay.NoiXuatPhat,
                                               NoiDen = chuyenbay.NoiDen,
                                               NgayXuatPhat = chuyenbay.NgayXuatPhat,
                                               GioBay = chuyenbay.GioBay,
                                               SoLuongVeBsn = chuyenbay.SoLuongVeBsn,
                                               SoLuongVeEco = chuyenbay.SoLuongVeEco,
                                               DonGia = chuyenbay.DonGia,
                                           })
            .ToListAsync();

            if (thongtinchuyenbay == null)
            {
                return NotFound("ThongTinChuyenBay not found.");
            }

            return thongtinchuyenbay;
        }


        //===========================================================================

        [HttpPost]
        public async Task<ActionResult<ThongTinChuyenBay>> PostThongTinChuyenBay(ThongTinChuyenBay thongTinChuyenBay)
        {
            if (thongTinChuyenBay == null)
            {
                return BadRequest("Invalid data.");
            }

            // Chuyển đổi dữ liệu từ ThongTinChuyenBay sang Chuyenbay
            var chuyenbay = new Chuyenbay
            {
                MaChuyenBay = thongTinChuyenBay.MaChuyenBay,
                MaMayBay = thongTinChuyenBay.MaMayBay,
                GioBay = thongTinChuyenBay.GioBay,
                NoiXuatPhat = thongTinChuyenBay.NoiXuatPhat,
                NoiDen = thongTinChuyenBay.NoiDen,
                NgayXuatPhat = thongTinChuyenBay.NgayXuatPhat,
                DonGia = thongTinChuyenBay.DonGia,
                SoLuongVeBsn = thongTinChuyenBay.SoLuongVeBsn,
                SoLuongVeEco = thongTinChuyenBay.SoLuongVeEco,
            };

            _context.Chuyenbays.Add(chuyenbay);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ThongTinChuyenBayExists(chuyenbay.MaChuyenBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetThongTinChuyenBay", new { id = thongTinChuyenBay.MaChuyenBay }, thongTinChuyenBay);
        }



        //============================================================================

        [HttpPut("{maChuyenBay}")]
        public async Task<IActionResult> PutThongTinChuyenBay(string maChuyenBay, ThongTinChuyenBay thongTinChuyenBay)
        {
            if (maChuyenBay != thongTinChuyenBay.MaChuyenBay)
            {
                return BadRequest("Mismatched IDs.");
            }

            var chuyenbay = await _context.Chuyenbays.FindAsync(maChuyenBay);

            if (chuyenbay == null)
            {
                return NotFound("Chuyenbay not found.");
            }

            // Cập nhật thông tin Chuyenbay
            chuyenbay.MaMayBay = thongTinChuyenBay.MaMayBay;
            chuyenbay.GioBay = thongTinChuyenBay.GioBay;
            chuyenbay.NoiXuatPhat = thongTinChuyenBay.NoiXuatPhat;
            chuyenbay.NoiDen = thongTinChuyenBay.NoiDen;
            chuyenbay.NgayXuatPhat = thongTinChuyenBay.NgayXuatPhat;
            chuyenbay.DonGia = thongTinChuyenBay.DonGia;
            chuyenbay.SoLuongVeBsn = thongTinChuyenBay.SoLuongVeBsn;
            chuyenbay.SoLuongVeEco = thongTinChuyenBay.SoLuongVeEco;

            _context.Entry(chuyenbay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongTinChuyenBayExists(maChuyenBay))
                {
                    return NotFound("Chuyenbay not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //============================================================================

        [HttpDelete("{maChuyenBay}")]
        public async Task<IActionResult> DeleteThongTinChuyenBay(string maChuyenBay)
        {
            if (_context.ThongTinChuyenBay == null)
            {
                return NotFound();
            }

            var chuyenbay = await _context.Chuyenbays.FindAsync(maChuyenBay);

            if (chuyenbay == null)
            {
                return NotFound("Chuyenbay not found.");
            }

            _context.Chuyenbays.Remove(chuyenbay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThongTinChuyenBayExists(string id)
        {
            return (_context.ThongTinChuyenBay?.Any(e => e.MaChuyenBay == id)).GetValueOrDefault();
        }
    }
}
