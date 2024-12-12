using System;
using System.Collections.Generic;

namespace mini.project.Models
{
    public class T_FicheAbsence
    {
        public int CodeFicheAbsence { get; set; }
        public DateTime DateJour { get; set; }
        public int CodeMatiere { get; set; }
        public int CodeEnseignant { get; set; }
        public int CodeClasse { get; set; }

        // Navigation properties
        public T_Matiere Matiere { get; set; }
        public T_Enseignant Enseignant { get; set; }
        public T_Classe Classe { get; set; }
        public ICollection<T_FicheAbsenceSeance> FichesAbsenceSeances { get; set; }
        public ICollection<T_LigneFicheAbsence> LignesFicheAbsence { get; set; }
    }
}
