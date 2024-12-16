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
    public class EtudiantController : ControllerBase
    {
        private readonly MyDbContext _context;

        public EtudiantController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etudiant>>> GetEtudiants()
        {
            return await _context.Etudiants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Etudiant>> GetEtudiant(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null) return NotFound();
            return etudiant;
        }

        [HttpPost]
        public async Task<ActionResult<Etudiant>> CreateEtudiant([FromBody] Etudiant etudiant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Etudiants.Add(etudiant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEtudiant), new { id = etudiant.CodeEtudiant }, etudiant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEtudiant(int id, [FromBody] Etudiant etudiant)
        {
            if (id != etudiant.CodeEtudiant) return BadRequest();
            _context.Entry(etudiant).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Etudiants.Any(e => e.CodeEtudiant == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtudiant(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null) return NotFound();

            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
