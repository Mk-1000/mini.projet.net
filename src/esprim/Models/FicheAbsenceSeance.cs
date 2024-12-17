using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini.project.Models
{
    public class FicheAbsenceSeance
    {
        [Key]
        [ForeignKey("FicheAbsence")]
        public int CodeFicheAbsence { get; set; }
        [ForeignKey("Seance")]
        public int CodeSeance { get; set; }

        // Navigation properties
        public FicheAbsence? FicheAbsence { get; set; }
        public Seance? Seance { get; set; }
    }
}
