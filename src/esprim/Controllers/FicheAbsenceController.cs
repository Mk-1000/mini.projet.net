using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;
using System.Threading.Tasks;

public class FicheAbsenceController : Controller
{
    private readonly MyDbContext _context;

    public FicheAbsenceController(MyDbContext context)
    {
        _context = context;
    }

    // Action to show the form for marking absence
    public IActionResult MarkAbsence(int classeId)
    {
        var classe = _context.Classes
                             .Include(c => c.Etudiants)
                             .FirstOrDefault(c => c.CodeClasse == classeId);

        if (classe == null)
        {
            return NotFound(); // If the class is not found, return a 404 error
        }
        return View(classe); // Return the class data to the view for absence marking
    }
    [HttpPost]
    public async Task<IActionResult> MarkAbsence(int classeId, int[] etudiantIds, int matiereId, DateTime date, bool isAbsent)
    {
        if (etudiantIds == null || etudiantIds.Length == 0)
        {
            // Handle case where no students are selected
            ModelState.AddModelError("", "No students selected.");
            return View();  // Optionally return a view with an error message
        }

        // Loop through all selected students
        foreach (var etudiantId in etudiantIds)
        {
            // Create FicheAbsence record (this is the absence record for the class)
            var ficheAbsence = new FicheAbsence
            {
                CodeClasse = classeId,  // Use the correct class ID
                CodeMatiere = matiereId,
                DateJour = date,
                CodeEnseignant = 1 // Set a placeholder or derive this from the logged-in user, if necessary
            };

            await _context.AddAsync(ficheAbsence);  // Add the FicheAbsence record to the DB
            await _context.SaveChangesAsync();  // Save changes to the database

            // Create LigneFicheAbsence record (this stores the individual student absence data)
            var ligneFicheAbsence = new LigneFicheAbsence
            {
                CodeEtudiant = etudiantId,
                CodeFicheAbsence = ficheAbsence.CodeFicheAbsence,
                IsAbsent = isAbsent
            };

            await _context.AddAsync(ligneFicheAbsence);  // Add the LigneFicheAbsence to the DB
            await _context.SaveChangesAsync();  // Save changes to the database
        }

        return RedirectToAction("Index");  // Redirect to another view, you can change it to a more specific view
    }


}
