using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class FicheAbsenceSeance
    {
        [Key]
        public int CodeFicheAbsence { get; set; }
        public int CodeSeance { get; set; }

        // Navigation properties
        public FicheAbsence FicheAbsence { get; set; }
        public Seance Seance { get; set; }
    }
}
