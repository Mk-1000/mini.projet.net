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
    public class GroupeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GroupeController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Groupe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Groupe>>> GetGroupes()
        {
            return await _context.Groupes.Include(g => g.Classe).ToListAsync();
        }

        // GET: api/Groupe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Groupe>> GetGroupe(int id)
        {
            var groupe = await _context.Groupes
                .Include(g => g.Classe)
                .FirstOrDefaultAsync(g => g.CodeGroupe == id);

            if (groupe == null)
                return NotFound();

            return groupe;
        }

        // POST: api/Groupe
        [HttpPost]
        public async Task<ActionResult<Groupe>> CreateGroupe([FromBody] Groupe groupe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Groupes.Add(groupe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroupe), new { id = groupe.CodeGroupe }, groupe);
        }

        // PUT: api/Groupe/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupe(int id, [FromBody] Groupe groupe)
        {
            if (id != groupe.CodeGroupe)
                return BadRequest();

            _context.Entry(groupe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Groupes.Any(g => g.CodeGroupe == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Groupe/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupe(int id)
        {
            var groupe = await _context.Groupes.FindAsync(id);
            if (groupe == null)
                return NotFound();

            _context.Groupes.Remove(groupe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
