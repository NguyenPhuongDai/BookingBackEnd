using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingTourWeb_WebAPI.Models;

namespace BookingTourWeb_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaybaysController : ControllerBase
    {
        private readonly DvmayBayContext _context;

        public MaybaysController(DvmayBayContext context)
        {
            _context = context;
        }

        // GET: api/Maybays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maybay>>> GetMaybays()
        {
          if (_context.Maybays == null)
          {
              return NotFound();
          }
            return await _context.Maybays.ToListAsync();
        }

        // GET: api/Maybays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maybay>> GetMaybay(string id)
        {
          if (_context.Maybays == null)
          {
              return NotFound();
          }
            var maybay = await _context.Maybays.FindAsync(id);

            if (maybay == null)
            {
                return NotFound();
            }

            return maybay;
        }

        // PUT: api/Maybays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaybay(string id, Maybay maybay)
        {
            if (id != maybay.MaMayBay)
            {
                return BadRequest();
            }

            _context.Entry(maybay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaybayExists(id))
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

        // POST: api/Maybays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maybay>> PostMaybay(Maybay maybay)
        {
          if (_context.Maybays == null)
          {
              return Problem("Entity set 'DvmayBayContext.Maybays'  is null.");
          }
            _context.Maybays.Add(maybay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaybayExists(maybay.MaMayBay))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaybay", new { id = maybay.MaMayBay }, maybay);
        }

        // DELETE: api/Maybays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaybay(string id)
        {
            if (_context.Maybays == null)
            {
                return NotFound();
            }
            var maybay = await _context.Maybays.FindAsync(id);
            if (maybay == null)
            {
                return NotFound();
            }

            _context.Maybays.Remove(maybay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaybayExists(string id)
        {
            return (_context.Maybays?.Any(e => e.MaMayBay == id)).GetValueOrDefault();
        }
    }
}
