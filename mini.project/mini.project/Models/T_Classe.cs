using System;
using System.Collections.Generic;

namespace mini.project.Models
{
    public class T_Classe
    {
        public int CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public int CodeGroupe { get; set; }
        public int CodeDepartement { get; set; }

        // Navigation properties
        public T_Departement Departement { get; set; }
        public ICollection<T_Groupe> Groupes { get; set; }
        public ICollection<T_FicheAbsence> FichesAbsence { get; set; }
        public ICollection<T_Etudiant> Etudiants { get; set; }
    }
}
