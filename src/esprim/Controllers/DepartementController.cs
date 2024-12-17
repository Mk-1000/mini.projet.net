using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class DepartementController : Controller
{
    private readonly MyDbContext _context;

    public DepartementController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Departement/Index
    public async Task<IActionResult> Index()
    {
        var departements = await _context.Departements.ToListAsync();
        return View(departements);
    }

    // GET: Departement/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Departement/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CodeDepartement,NomDepartement")] Departement departement)
    {
        if (ModelState.IsValid)
        {
            _context.Add(departement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(departement);
    }

    // GET: Departement/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var departement = await _context.Departements.FindAsync(id);
        if (departement == null)
        {
            return NotFound();
        }
        return View(departement);
    }

    // POST: Departement/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CodeDepartement,NomDepartement")] Departement departement)
    {
        if (id != departement.CodeDepartement)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(departement);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(departement.CodeDepartement))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(departement);
    }

    // GET: Departement/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var departement = await _context.Departements
            .FirstOrDefaultAsync(m => m.CodeDepartement == id);
        if (departement == null)
        {
            return NotFound();
        }

        return View(departement);
    }

    // POST: Departement/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var departement = await _context.Departements.FindAsync(id);
        _context.Departements.Remove(departement);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DepartementExists(int id)
    {
        return _context.Departements.Any(e => e.CodeDepartement == id);
    }
}
