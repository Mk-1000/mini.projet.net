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
    public class ClasseController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ClasseController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classe>>> GetClasses()
        {
            return await _context.Classes.Include(c => c.Departement).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classe>> GetClasse(int id)
        {
            var classe = await _context.Classes.Include(c => c.Departement).FirstOrDefaultAsync(c => c.CodeClasse == id);
            if (classe == null) return NotFound();
            return classe;
        }

        [HttpPost]
        public async Task<ActionResult<Classe>> CreateClasse([FromBody] Classe classe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Classes.Add(classe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClasse), new { id = classe.CodeClasse }, classe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClasse(int id, [FromBody] Classe classe)
        {
            if (id != classe.CodeClasse) return BadRequest();

            _context.Entry(classe).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Classes.Any(e => e.CodeClasse == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasse(int id)
        {
            var classe = await _context.Classes.FindAsync(id);
            if (classe == null) return NotFound();

            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
