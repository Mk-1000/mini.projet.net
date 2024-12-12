using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class FicheAbsence
    {
        [Key]
        public int CodeFicheAbsence { get; set; }
        public DateTime DateJour { get; set; }
        public int CodeMatiere { get; set; }
        public int CodeEnseignant { get; set; }
        public int CodeClasse { get; set; }

        // Navigation properties
        public Matiere Matiere { get; set; }
        public Enseignant Enseignant { get; set; }
        public Classe Classe { get; set; }
        public ICollection<FicheAbsenceSeance> FichesAbsenceSeances { get; set; } = new List<FicheAbsenceSeance>(); // Default empty list
        public ICollection<LigneFicheAbsence> LignesFicheAbsence { get; set; } = new List<LigneFicheAbsence>(); // Default empty list
    }
}
