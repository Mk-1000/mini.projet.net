using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;
using System.Threading.Tasks;
using System.Linq;

public class EtudiantController : Controller
{
    private readonly MyDbContext _context;

    public EtudiantController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Etudiant/Index
    public async Task<IActionResult> Index()
    {
        var etudiants = await _context.Etudiants.Include(e => e.Classe).ToListAsync();
        ViewBag.Classes = await _context.Classes.ToListAsync();
        return View(etudiants);
    }

    // POST: Etudiant/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,CodeClasse")] Etudiant etudiant)
    {
        if (ModelState.IsValid)
        {
            _context.Add(etudiant);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Etudiant added successfully!";
            return RedirectToAction(nameof(Index));
        }
        TempData["ErrorMessage"] = "Error creating Etudiant.";
        return RedirectToAction(nameof(Index));
    }

    // POST: Etudiant/Edit
    [HttpPost]
    public async Task<IActionResult> Edit([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,CodeClasse")] Etudiant etudiant)
    {
        if (ModelState.IsValid)
        {
            _context.Update(etudiant);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Etudiant updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        TempData["ErrorMessage"] = "Error updating Etudiant.";
        return RedirectToAction(nameof(Index));
    }

    // POST: Etudiant/Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        if (etudiant != null)
        {
            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Etudiant deleted successfully!";
        }
        else
        {
            TempData["ErrorMessage"] = "Etudiant not found.";
        }
        return RedirectToAction(nameof(Index));
    }
}
