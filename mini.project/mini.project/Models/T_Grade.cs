namespace mini.project.Models
{
    public class T_Grade
    {
        public int CodeGrade { get; set; }
        public string NomGrade { get; set; }

        // Navigation property
        public ICollection<T_Enseignant> Enseignants { get; set; }
    }
}
