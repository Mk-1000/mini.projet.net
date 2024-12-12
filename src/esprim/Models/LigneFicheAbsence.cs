using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class LigneFicheAbsence
    {
        [Key]
        public int CodeLigneFicheAbsence { get; set; }  // Unique key for this line record
        public int CodeEtudiant { get; set; }  // Student ID
        public int CodeFicheAbsence { get; set; }  // FicheAbsence ID (Foreign Key)

        public bool IsAbsent { get; set; }  // Indicates if the student was absent

        // Navigation properties
        public Etudiant Etudiant { get; set; }  // Associated student
        public FicheAbsence FicheAbsence { get; set; }  // Associated FicheAbsence record
    }
}
