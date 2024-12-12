using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class AbsenceReportController : Controller
{
    private readonly MyDbContext _context;

    public AbsenceReportController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult TotalAbsences(DateTime? startDate, DateTime? endDate)
    {
        if (!startDate.HasValue || !endDate.HasValue)
        {
            // Handle missing dates (you could also set default dates if needed)
            return View();
        }

        var absences = _context.LignesFicheAbsence
            .Where(l => l.FicheAbsence.DateJour >= startDate.Value && l.FicheAbsence.DateJour <= endDate.Value)
            .Include(l => l.Etudiant)
            .Include(l => l.FicheAbsence)
            .ThenInclude(f => f.Matiere)
            .ToList();

        return View(absences); // Pass the data to the view
    }
}
