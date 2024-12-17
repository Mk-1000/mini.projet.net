using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini.project.Models
{
    public class FicheAbsence
    {
        [Key]
        public int CodeFicheAbsence { get; set; }
        public DateTime DateJour { get; set; }

        [ForeignKey("Matiere")]
        public int CodeMatiere { get; set; }
        [ForeignKey("Enseignant")]
        public int CodeEnseignant { get; set; }
        [ForeignKey("Classe")]
        public int CodeClasse { get; set; }

        // Navigation properties
        public Matiere? Matiere { get; set; }
        public Enseignant? Enseignant { get; set; }
        public Classe? Classe { get; set; }
        public ICollection<FicheAbsenceSeance> FichesAbsenceSeances { get; set; } = new List<FicheAbsenceSeance>(); // Default empty list
        public ICollection<LigneFicheAbsence> LignesFicheAbsence { get; set; } = new List<LigneFicheAbsence>(); // Default empty list
    }
}
