using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esprim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeanceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SeanceController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Seance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
        {
            // Include the related FichesAbsenceSeances (if needed)
            return await _context.Seances.Include(s => s.FichesAbsenceSeances).ToListAsync();
        }

        // GET: api/Seance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seances
                .Include(s => s.FichesAbsenceSeances) // Including related FichesAbsenceSeances
                .FirstOrDefaultAsync(s => s.CodeSeance == id);

            if (seance == null)
                return NotFound();

            return seance;
        }

        // POST: api/Seance
        [HttpPost]
        public async Task<ActionResult<Seance>> CreateSeance([FromBody] Seance seance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeance), new { id = seance.CodeSeance }, seance);
        }

        // PUT: api/Seance/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeance(int id, [FromBody] Seance seance)
        {
            if (id != seance.CodeSeance)
                return BadRequest();

            _context.Entry(seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Seances.Any(s => s.CodeSeance == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Seance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
                return NotFound();

            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
