using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class GroupeController : Controller
{
    private readonly MyDbContext _context;

    public GroupeController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Groupe/Index
    public async Task<IActionResult> Index()
    {
        var groupes = await _context.Groupes.ToListAsync();
        return View(groupes);
    }

    // GET: Groupe/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Groupe/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CodeGroupe,NomGroupe")] Groupe groupe)
    {
        if (ModelState.IsValid)
        {
            _context.Add(groupe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(groupe);
    }

    // GET: Groupe/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var groupe = await _context.Groupes.FindAsync(id);
        if (groupe == null)
        {
            return NotFound();
        }
        return View(groupe);
    }

    // POST: Groupe/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CodeGroupe,NomGroupe")] Groupe groupe)
    {
        if (id != groupe.CodeGroupe)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(groupe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupeExists(groupe.CodeGroupe))
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
        return View(groupe);
    }

    // GET: Groupe/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var groupe = await _context.Groupes
            .FirstOrDefaultAsync(m => m.CodeGroupe == id);
        if (groupe == null)
        {
            return NotFound();
        }

        return View(groupe);
    }

    // POST: Groupe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var groupe = await _context.Groupes.FindAsync(id);
        _context.Groupes.Remove(groupe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GroupeExists(int id)
    {
        return _context.Groupes.Any(e => e.CodeGroupe == id);
    }
}
