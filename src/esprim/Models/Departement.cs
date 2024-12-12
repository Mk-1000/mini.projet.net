using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class Departement
    {
        [Key]
        public int CodeDepartement { get; set; }
        public string NomDepartement { get; set; }

        // Navigation properties
        public ICollection<Classe> Classes { get; set; } = new List<Classe>(); // Default empty list
        public ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>(); // Default empty list
    }
}
