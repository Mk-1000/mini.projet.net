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
    public class EnseignantController : ControllerBase
    {
        private readonly MyDbContext _context;

        public EnseignantController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enseignant>>> GetEnseignants()
        {
            return await _context.Enseignants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enseignant>> GetEnseignant(int id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null) return NotFound();
            return enseignant;
        }

        [HttpPost]
        public async Task<ActionResult<Enseignant>> CreateEnseignant([FromBody] Enseignant enseignant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Enseignants.Add(enseignant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnseignant), new { id = enseignant.CodeEnseignant }, enseignant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnseignant(int id, [FromBody] Enseignant enseignant)
        {
            if (id != enseignant.CodeEnseignant) return BadRequest();
            _context.Entry(enseignant).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Enseignants.Any(e => e.CodeEnseignant == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnseignant(int id)
        {
            var enseignant = await _context.Enseignants.FindAsync(id);
            if (enseignant == null) return NotFound();

            _context.Enseignants.Remove(enseignant);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
