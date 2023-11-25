using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Numerics;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangsController : ControllerBase
    {/*
        private readonly DVMayBayContext _context;
        public KhachHangsController(DVMayBayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Khachhang>>> GetKhachHangs()
        {
            if (_context.Khachhangs == null)
            {
                return NotFound();
            }
            return Ok(await _context.Khachhangs
                .Include(p =>p.Ves)
                .ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Khachhang>> GetKhachHang(long id)
        {
            if (_context.Khachhangs == null)
            {
                return NotFound();
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);

            if (khachhang == null)
            {
                return NotFound();
            }

            return khachhang;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(long id, Khachhang khachhang)
        {
            if (id != khachhang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachhang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/Khachhangs
        [HttpPost]
        public async Task<ActionResult<Khachhang>> PostKhachhang(Khachhang khachhang)
        {
            if (_context.Khachhangs == null)
            {
                return Problem("Entity set 'DvmayBayContext.Khachhangs'  is null.");
            }
            _context.Khachhangs.Add(khachhang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachhang.MaKh))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachhang", new { id = khachhang.MaKh }, khachhang);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachhang(long id)
        {
            if (_context.Khachhangs == null)
            {
                return NotFound();
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }

            _context.Khachhangs.Remove(khachhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(long id)
        {
            return (_context.Khachhangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
        */
    }
}
