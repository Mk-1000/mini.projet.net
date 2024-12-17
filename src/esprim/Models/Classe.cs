using mini.project.Models;
using System.ComponentModel.DataAnnotations;
namespace mini.project.Models
{
    public class Classe
    {
        [Key]
        public int CodeClasse { get; set; }
        public string NomClasse { get; set; }
        public int CodeGroupe { get; set; }
        public int CodeDepartement { get; set; }
        public int CodeMatiere { get; set; }

        // Navigation properties
        public Departement Departement { get; set; }
        public Groupe Groupe { get; set; }  // Reference to Groupe
        public ICollection<FicheAbsence> FichesAbsence { get; set; } = new List<FicheAbsence>(); // Default empty list
        public ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>(); // Default empty list
    }
}