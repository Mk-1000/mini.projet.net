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
    public class FicheAbsenceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public FicheAbsenceController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FicheAbsence>>> GetFicheAbsences()
        {
            return await _context.FichesAbsence.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FicheAbsence>> GetFicheAbsence(int id)
        {
            var ficheAbsence = await _context.FichesAbsence.FindAsync(id);
            if (ficheAbsence == null) return NotFound();
            return ficheAbsence;
        }

        [HttpPost]
        public async Task<ActionResult<FicheAbsence>> CreateFicheAbsence([FromBody] FicheAbsence ficheAbsence)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.FichesAbsence.Add(ficheAbsence);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFicheAbsence), new { id = ficheAbsence.CodeFicheAbsence }, ficheAbsence);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFicheAbsence(int id, [FromBody] FicheAbsence ficheAbsence)
        {
            if (id != ficheAbsence.CodeFicheAbsence) return BadRequest();
            _context.Entry(ficheAbsence).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FichesAbsence.Any(f => f.CodeFicheAbsence == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFicheAbsence(int id)
        {
            var ficheAbsence = await _context.FichesAbsence.FindAsync(id);
            if (ficheAbsence == null) return NotFound();

            _context.FichesAbsence.Remove(ficheAbsence);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
