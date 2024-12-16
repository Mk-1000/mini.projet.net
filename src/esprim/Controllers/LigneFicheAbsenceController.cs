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
    public class LigneFicheAbsenceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LigneFicheAbsenceController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/LigneFicheAbsence
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigneFicheAbsence>>> GetLignesFicheAbsence()
        {
            return await _context.LignesFicheAbsence.Include(l => l.Etudiant)
                                                     .Include(l => l.FicheAbsence)
                                                     .ToListAsync();
        }

        // GET: api/LigneFicheAbsence/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LigneFicheAbsence>> GetLigneFicheAbsence(int id)
        {
            var ligne = await _context.LignesFicheAbsence
                                      .Include(l => l.Etudiant)
                                      .Include(l => l.FicheAbsence)
                                      .FirstOrDefaultAsync(l => l.CodeLigneFicheAbsence == id);

            if (ligne == null)
                return NotFound();

            return ligne;
        }

        // POST: api/LigneFicheAbsence
        [HttpPost]
        public async Task<ActionResult<LigneFicheAbsence>> CreateLigneFicheAbsence([FromBody] LigneFicheAbsence ligneFicheAbsence)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.LignesFicheAbsence.Add(ligneFicheAbsence);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLigneFicheAbsence), new { id = ligneFicheAbsence.CodeLigneFicheAbsence }, ligneFicheAbsence);
        }

        // PUT: api/LigneFicheAbsence/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLigneFicheAbsence(int id, [FromBody] LigneFicheAbsence ligneFicheAbsence)
        {
            if (id != ligneFicheAbsence.CodeLigneFicheAbsence)
                return BadRequest();

            _context.Entry(ligneFicheAbsence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LignesFicheAbsence.Any(l => l.CodeLigneFicheAbsence == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/LigneFicheAbsence/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLigneFicheAbsence(int id)
        {
            var ligne = await _context.LignesFicheAbsence.FindAsync(id);
            if (ligne == null)
                return NotFound();

            _context.LignesFicheAbsence.Remove(ligne);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
