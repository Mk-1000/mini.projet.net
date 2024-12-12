using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class LigneFicheAbsence
    {
        [Key]
        public int CodeFicheAbsence { get; set; }
        public int CodeEtudiant { get; set; }

        // Navigation properties
        public FicheAbsence FicheAbsence { get; set; }
        public Etudiant Etudiant { get; set; }
    }
}
