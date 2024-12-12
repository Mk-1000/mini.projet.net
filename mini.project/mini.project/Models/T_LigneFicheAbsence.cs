namespace mini.project.Models
{
    public class T_LigneFicheAbsence
    {
        public int CodeFicheAbsence { get; set; }
        public int CodeEtudiant { get; set; }

        // Navigation properties
        public T_FicheAbsence FicheAbsence { get; set; }
        public T_Etudiant Etudiant { get; set; }
    }
}
