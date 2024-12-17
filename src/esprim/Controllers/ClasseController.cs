using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ClasseController : Controller
{
    private readonly MyDbContext _context;

    public ClasseController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Classe/Index
    public async Task<IActionResult> Index()
    {
        // Fetch all required data for drop-downs
        ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "CodeDepartement", "NomDepartement");
        ViewBag.Groupes = new SelectList(await _context.Groupes.ToListAsync(), "CodeGroupe", "NomGroupe");
        ViewBag.Matieres = new SelectList(await _context.Matieres.ToListAsync(), "CodeMatiere", "NomMatiere");

        // Fetch all classes
        var classes = await _context.Classes.ToListAsync();
        return View(classes);
    }

    // POST: Classe/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Classe classe)
    {
        if (ModelState.IsValid)
        {
            _context.Add(classe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Repopulate drop-downs in case of validation error
        ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "CodeDepartement", "NomDepartement");
        ViewBag.Groupes = new SelectList(await _context.Groupes.ToListAsync(), "CodeGroupe", "NomGroupe");
        ViewBag.Matieres = new SelectList(await _context.Matieres.ToListAsync(), "CodeMatiere", "NomMatiere");

        return View(nameof(Index), await _context.Classes.ToListAsync()); // Return to Index view
    }

    // GET: Classe/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classe = await _context.Classes.FindAsync(id);
        if (classe == null)
        {
            return NotFound();
        }

        // Populate dropdowns for editing
        ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "CodeDepartement", "NomDepartement", classe.CodeDepartement);
        ViewBag.Groupes = new SelectList(await _context.Groupes.ToListAsync(), "CodeGroupe", "NomGroupe", classe.CodeGroupe);
        ViewBag.Matieres = new SelectList(await _context.Matieres.ToListAsync(), "CodeMatiere", "NomMatiere", classe.CodeMatiere);

        return View(classe);
    }

    // POST: Classe/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CodeClasse,NomClasse,CodeGroupe,CodeDepartement,CodeMatiere")] Classe classe)
    {
        if (id != classe.CodeClasse)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(classe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseExists(classe.CodeClasse))
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

        // Repopulate drop-downs if validation fails
        ViewBag.Departements = new SelectList(await _context.Departements.ToListAsync(), "CodeDepartement", "NomDepartement", classe.CodeDepartement);
        ViewBag.Groupes = new SelectList(await _context.Groupes.ToListAsync(), "CodeGroupe", "NomGroupe", classe.CodeGroupe);
        ViewBag.Matieres = new SelectList(await _context.Matieres.ToListAsync(), "CodeMatiere", "NomMatiere", classe.CodeMatiere);

        return View(classe);
    }

    // GET: Classe/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classe = await _context.Classes
            .FirstOrDefaultAsync(m => m.CodeClasse == id);
        if (classe == null)
        {
            return NotFound();
        }

        return View(classe);
    }

    // POST: Classe/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var classe = await _context.Classes.FindAsync(id);
        _context.Classes.Remove(classe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClasseExists(int id)
    {
        return _context.Classes.Any(e => e.CodeClasse == id);
    }
}
