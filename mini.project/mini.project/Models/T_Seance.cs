using System;

namespace mini.project.Models
{
    public class T_Seance
    {
        public int CodeSeance { get; set; }
        public string NomSeance { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }

        // Navigation property
        public ICollection<T_FicheAbsenceSeance> FichesAbsenceSeances { get; set; }
    }
}
