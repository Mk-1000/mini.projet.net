using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class ClasseController : Controller
{
    private readonly MyDbContext _context;

    public ClasseController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Classe/Index
    public async Task<IActionResult> Index(int? id, string actionType, [Bind("CodeClasse,NomClasse,CodeGroupe,CodeDepartement")] Classe classe)
    {
        if (actionType == "Create" && ModelState.IsValid)
        {
            _context.Add(classe);
            await _context.SaveChangesAsync();
        }
        else if (actionType == "Edit" && id != null && ModelState.IsValid)
        {
            var existingClasse = await _context.Classes.FindAsync(id);
            if (existingClasse != null)
            {
                existingClasse.NomClasse = classe.NomClasse;
                existingClasse.CodeGroupe = classe.CodeGroupe;
                existingClasse.CodeDepartement = classe.CodeDepartement;
                _context.Update(existingClasse);
                await _context.SaveChangesAsync();
            }
        }
        else if (actionType == "Delete" && id != null)
        {
            var classeToDelete = await _context.Classes.FindAsync(id);
            if (classeToDelete != null)
            {
                _context.Classes.Remove(classeToDelete);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve and display all classes
        var classes = await _context.Classes
            .Include(c => c.Groupe)
            .Include(c => c.Departement)
            .ToListAsync();
        ViewBag.Groups = _context.Groupes.ToList();
        ViewBag.Departements = _context.Departements.ToList();

        return View(classes);
    }


    // GET: Classe/Create
    public IActionResult Create()
    {
        // Populate dropdowns for groups and departments
        ViewBag.Groups = _context.Groupes.ToList();
        ViewBag.Departements = _context.Departements.ToList();
        return View();
    }

    // POST: Classe/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CodeClasse,NomClasse,CodeGroupe,CodeDepartement")] Classe classe)
    {
        if (ModelState.IsValid)
        {
            _context.Add(classe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Reload groups and departments if the model is invalid
        ViewBag.Groups = _context.Groupes.ToList();
        ViewBag.Departements = _context.Departements.ToList();
        return View(classe);
    }

    // GET: Classe/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var classe = await _context.Classes
            .Include(c => c.Groupe)
            .Include(c => c.Departement)
            .FirstOrDefaultAsync(c => c.CodeClasse == id);

        if (classe == null)
        {
            return NotFound();
        }

        ViewBag.Groups = _context.Groupes.ToList();
        ViewBag.Departements = _context.Departements.ToList();
        return View(classe);
    }

    // POST: Classe/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CodeClasse,NomClasse,CodeGroupe,CodeDepartement")] Classe classe)
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

        ViewBag.Groups = _context.Groupes.ToList();
        ViewBag.Departements = _context.Departements.ToList();
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
            .Include(c => c.Groupe)
            .Include(c => c.Departement)
            .FirstOrDefaultAsync(c => c.CodeClasse == id);

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
