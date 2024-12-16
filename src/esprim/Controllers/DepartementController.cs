using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

namespace esprim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DepartementController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
            return await _context.Departements.Include(d => d.Classes).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departement>> GetDepartement(int id)
        {
            var departement = await _context.Departements
                .Include(d => d.Classes)
                .FirstOrDefaultAsync(d => d.CodeDepartement == id);

            if (departement == null) return NotFound();
            return departement;
        }

        [HttpPost]
        public async Task<ActionResult<Departement>> CreateDepartement([FromBody] Departement departement)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Departements.Add(departement);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartement), new { id = departement.CodeDepartement }, departement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartement(int id, [FromBody] Departement departement)
        {
            if (id != departement.CodeDepartement) return BadRequest();

            _context.Entry(departement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departements.Any(d => d.CodeDepartement == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartement(int id)
        {
            var departement = await _context.Departements.FindAsync(id);
            if (departement == null) return NotFound();

            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
