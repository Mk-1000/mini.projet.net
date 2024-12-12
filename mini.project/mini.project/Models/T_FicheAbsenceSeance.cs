namespace mini.project.Models
{
    public class T_FicheAbsenceSeance
    {
        public int CodeFicheAbsence { get; set; }
        public int CodeSeance { get; set; }

        // Navigation properties
        public T_FicheAbsence FicheAbsence { get; set; }
        public T_Seance Seance { get; set; }
    }
}
