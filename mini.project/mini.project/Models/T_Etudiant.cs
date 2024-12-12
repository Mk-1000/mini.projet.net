namespace mini.project.Models
{
    public class T_Etudiant
    {
        public int CodeEtudiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public int CodeClasse { get; set; }
        public string NumInscription { get; set; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }

        // Navigation property
        public T_Classe Classe { get; set; }
    }
}
