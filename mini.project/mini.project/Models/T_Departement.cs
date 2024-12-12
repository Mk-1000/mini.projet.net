namespace mini.project.Models
{
    public class T_Departement
    {
        public int CodeDepartement { get; set; }
        public string NomDepartement { get; set; }

        // Navigation properties
        public ICollection<T_Classe> Classes { get; set; }
        public ICollection<T_Enseignant> Enseignants { get; set; }
    }
}
