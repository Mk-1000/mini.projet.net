using System.ComponentModel.DataAnnotations;
namespace mini.project.Models
{
    public class Groupe
    {
        [Key]
        public int CodeGroupe { get; set; }
        public string NomGroupe { get; set; }

        // Navigation property
        public ICollection<Classe> Classes { get; set; } = new List<Classe>(); // Default empty list
    }
}