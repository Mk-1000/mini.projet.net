using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class EtudiantController : Controller
{
    private readonly MyDbContext _context;

    public EtudiantController(MyDbContext context)
    {
        _context = context;
    }

    // Index Action - Fetch students with their classes
    public async Task<IActionResult> Index()
    {
        // Ensure you include classes in the context if you need to display them in the view
        var etudiants = await _context.Etudiants.Include(e => e.Classe).ToListAsync();

        // Populate ViewBag with classes from the database
        ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "CodeClasse", "NomClasse");

        return View(etudiants);
    }


    // Create Action - Get the list of classes for the dropdown
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "CodeClasse", "NomClasse");
        return View();
    }

    // Create Action - Handle the form submission for adding a new student
    [HttpPost]
    public async Task<IActionResult> Create(Etudiant etudiant)
    {
        if (ModelState.IsValid)
        {
            _context.Add(etudiant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "CodeClasse", "NomClasse", etudiant.CodeClasse);
        return View(etudiant);
    }

    // Edit Action - Get the list of classes and the student to edit
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        if (etudiant == null)
        {
            return NotFound();
        }
        ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "CodeClasse", "NomClasse", etudiant.CodeClasse);
        return View(etudiant);
    }

    // Edit Action - Handle the form submission for editing a student
    [HttpPost]
    public async Task<IActionResult> Edit(Etudiant etudiant)
    {
        if (ModelState.IsValid)
        {
            _context.Update(etudiant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Classes = new SelectList(await _context.Classes.ToListAsync(), "CodeClasse", "NomClasse", etudiant.CodeClasse);
        return View(etudiant);
    }

    // Delete Action - Delete a student
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        if (etudiant != null)
        {
            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
