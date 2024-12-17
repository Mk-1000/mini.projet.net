using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class MatiereController : Controller
{
    private readonly MyDbContext _context;

    public MatiereController(MyDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var matieres = await _context.Matieres.ToListAsync();
        return View(matieres);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Matiere matiere)
    {
        if (ModelState.IsValid)
        {
            _context.Add(matiere);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(matiere);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Matiere matiere)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(matiere);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Matieres.Any(e => e.CodeMatiere == matiere.CodeMatiere))
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
        return View(matiere);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var matiere = await _context.Matieres.FindAsync(id);
        if (matiere != null)
        {
            _context.Matieres.Remove(matiere);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
