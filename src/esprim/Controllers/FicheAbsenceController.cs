using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class FicheAbsenceController : Controller
{
    private readonly MyDbContext _context;

    public FicheAbsenceController(MyDbContext context)
    {
        _context = context;
    }

    // GET: FicheAbsence/Index
    public async Task<IActionResult> Index()
    {
        var fichesAbsence = await _context.FichesAbsence
                                    .Include(f => f.Matiere)
                                    .Include(f => f.Enseignant)
                                    .Include(f => f.Classe)
                                    .ToListAsync();

        ViewData["MatiereList"] = new SelectList(_context.Matieres, "CodeMatiere", "NomMatiere");
        ViewData["EnseignantList"] = new SelectList(_context.Enseignants, "CodeEnseignant", "Nom");
        ViewData["ClasseList"] = new SelectList(_context.Classes, "CodeClasse", "NomClasse");

        return View(fichesAbsence);
    }

    // POST: FicheAbsence/Create
    [HttpPost]
    public async Task<IActionResult> Create(FicheAbsence ficheAbsence)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ficheAbsence);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "FicheAbsence created successfully!";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Error creating FicheAbsence.";
        return RedirectToAction(nameof(Index));
    }

    // POST: FicheAbsence/Edit
    [HttpPost]
    public async Task<IActionResult> Edit(FicheAbsence ficheAbsence)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ficheAbsence);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "FicheAbsence updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Error updating FicheAbsence.";
            }
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: FicheAbsence/Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var ficheAbsence = await _context.FichesAbsence.FindAsync(id);
        if (ficheAbsence != null)
        {
            _context.FichesAbsence.Remove(ficheAbsence);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "FicheAbsence deleted successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "FicheAbsence not found.";
        }
        return RedirectToAction(nameof(Index));
    }
}
