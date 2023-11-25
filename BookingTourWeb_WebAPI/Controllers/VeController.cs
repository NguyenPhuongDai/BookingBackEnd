using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TourBookingWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VesController : ControllerBase
    {
        /*private readonly DVMayBayContext _context;
        public VesController(DVMayBayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ve>>> GetVes()
        {
            return Ok(await _context.Ves
                .Include(p => p.Chitietves)
                .ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ve>> GetVe(long id)
        {
            if (_context.Ves == null)
            {
                return NotFound();
            }
            var Ve = await _context.Ves.FindAsync(id);

            if (Ve == null)
            {
                return NotFound();
            }

            return Ve;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVe(long id, Ve Ve)
        {
            if (id != Ve.MaVe)
            {
                return BadRequest();
            }

            _context.Entry(Ve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeExists(id))
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

        // POST: api/Ves
        [HttpPost]
        public async Task<ActionResult<Ve>> PostVe(Ve Ve)
        {
            if (_context.Ves == null)
            {
                return Problem("Entity set 'DvmayBayContext.Ves'  is null.");
            }
            _context.Ves.Add(Ve);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VeExists(Ve.MaVe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVe", new { id = Ve.MaVe }, Ve);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVe(long id)
        {
            if (_context.Ves == null)
            {
                return NotFound();
            }
            var Ve = await _context.Ves.FindAsync(id);
            if (Ve == null)
            {
                return NotFound();
            }

            _context.Ves.Remove(Ve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeExists(long id)
        {
            return (_context.Ves?.Any(e => e.MaVe == id)).GetValueOrDefault();
        }*/
    }
}
