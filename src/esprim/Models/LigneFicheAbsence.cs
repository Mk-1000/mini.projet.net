using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini.project.Models
{
    public class LigneFicheAbsence
    {
        [Key]
        public int CodeLigneFicheAbsence { get; set; }
        [ForeignKey("Etudiant")]
        public int CodeEtudiant { get; set; }
        [ForeignKey("FicheAbsence")]
        public int CodeFicheAbsence { get; set; }

        public bool IsAbsent { get; set; }

        public Etudiant? Etudiant { get; set; }
        public FicheAbsence? FicheAbsence { get; set; }
    }
}
