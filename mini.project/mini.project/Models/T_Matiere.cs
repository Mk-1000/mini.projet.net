namespace mini.project.Models
{
    public class T_Matiere
    {
        public int CodeMatiere { get; set; }
        public string NomMatiere { get; set; }
        public int NbreHeureCoursParSemaine { get; set; }
        public int NbreHeureTDParSemaine { get; set; }
        public int NbreHeureTPParSemaine { get; set; }

        // Navigation property
        public ICollection<T_FicheAbsence> FichesAbsence { get; set; }
    }
}
