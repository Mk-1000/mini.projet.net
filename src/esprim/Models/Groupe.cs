using System.ComponentModel.DataAnnotations;

namespace mini.project.Models
{
    public class Groupe
    {
        [Key]
        public int CodeGroupe { get; set; }
        public string NomGroupe { get; set; }

        // Navigation property
        public Classe Classe { get; set; }
    }
}
