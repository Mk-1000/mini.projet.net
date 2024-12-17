using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class EtudiantController : Controller
{
    private readonly MyDbContext _context;

    public EtudiantController(MyDbContext context)
    {
        _context = context;
    }

    // GET: Etudiant/Index
    public async Task<IActionResult> Index(int? id, string actionType, [Bind("CodeEtudiant,Nom,Prenom,DateNaissance,NumInscription,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
    {
        if (actionType == "Create" && ModelState.IsValid)
        {
            _context.Add(etudiant);
            await _context.SaveChangesAsync();
        }
        else if (actionType == "Edit" && id != null && ModelState.IsValid)
        {
            var existingEtudiant = await _context.Etudiants.FindAsync(id);
            if (existingEtudiant != null)
            {
                existingEtudiant.Nom = etudiant.Nom;
                existingEtudiant.Prenom = etudiant.Prenom;
                existingEtudiant.DateNaissance = etudiant.DateNaissance;
                existingEtudiant.NumInscription = etudiant.NumInscription;
                existingEtudiant.Adresse = etudiant.Adresse;
                existingEtudiant.Mail = etudiant.Mail;
                existingEtudiant.Tel = etudiant.Tel;
                existingEtudiant.CodeClasse = etudiant.CodeClasse;
                _context.Update(existingEtudiant);
                await _context.SaveChangesAsync();
            }
        }
        else if (actionType == "Delete" && id != null)
        {
            var etudiantToDelete = await _context.Etudiants.FindAsync(id);
            if (etudiantToDelete != null)
            {
                _context.Etudiants.Remove(etudiantToDelete);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve and display all students
        var etudiants = await _context.Etudiants
            .Include(e => e.Classe)
            .ToListAsync();
        ViewBag.Classes = _context.Classes.ToList();

        return View(etudiants);
    }

    // GET: Etudiant/Create
    public IActionResult Create()
    {
        // Populate dropdown for classes
        ViewBag.Classes = _context.Classes.ToList();
        return View();
    }

    // POST: Etudiant/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CodeEtudiant,Nom,Prenom,DateNaissance,NumInscription,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
    {
        if (ModelState.IsValid)
        {
            _context.Add(etudiant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Reload classes if the model is invalid
        ViewBag.Classes = _context.Classes.ToList();
        return View(etudiant);
    }

    // GET: Etudiant/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var etudiant = await _context.Etudiants
            .Include(e => e.Classe)
            .FirstOrDefaultAsync(e => e.CodeEtudiant == id);

        if (etudiant == null)
        {
            return NotFound();
        }

        ViewBag.Classes = _context.Classes.ToList();
        return View(etudiant);
    }

    // POST: Etudiant/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CodeEtudiant,Nom,Prenom,DateNaissance,NumInscription,Adresse,Mail,Tel,CodeClasse")] Etudiant etudiant)
    {
        if (id != etudiant.CodeEtudiant)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(etudiant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(etudiant.CodeEtudiant))
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

        ViewBag.Classes = _context.Classes.ToList();
        return View(etudiant);
    }

    // GET: Etudiant/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var etudiant = await _context.Etudiants
            .Include(e => e.Classe)
            .FirstOrDefaultAsync(e => e.CodeEtudiant == id);

        if (etudiant == null)
        {
            return NotFound();
        }

        return View(etudiant);
    }

    // POST: Etudiant/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        _context.Etudiants.Remove(etudiant);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EtudiantExists(int id)
    {
        return _context.Etudiants.Any(e => e.CodeEtudiant == id);
    }
}
