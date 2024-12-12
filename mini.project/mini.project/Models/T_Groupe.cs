namespace mini.project.Models
{
    public class T_Groupe
    {
        public int CodeGroupe { get; set; }
        public string NomGroupe { get; set; }

        // Navigation property
        public T_Classe Classe { get; set; }
    }
}
