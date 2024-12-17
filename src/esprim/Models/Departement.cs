using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class Departement
    {
        [Key]
        public int CodeDepartement { get; set; }

        [Required]
        [StringLength(100)]  // Limiting the length of the department name
        public string NomDepartement { get; set; }

        // Navigation properties (one-to-many relationship with Classe and Enseignant)
        public ICollection<Classe> Classes { get; set; } = new List<Classe>();
        public ICollection<Enseignant> Enseignants { get; set; } = new List<Enseignant>();
    }
}
