using Microsoft.EntityFrameworkCore;
using mini.project.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<T_Classe> Classes { get; set; }
    public DbSet<T_Matiere> Matieres { get; set; }
    public DbSet<T_Seance> Seances { get; set; }
    public DbSet<T_Groupe> Groupes { get; set; }
    public DbSet<T_FicheAbsence> FichesAbsence { get; set; }
    public DbSet<T_FicheAbsenceSeance> FichesAbsenceSeances { get; set; }
    public DbSet<T_LigneFicheAbsence> LignesFicheAbsence { get; set; }
    public DbSet<T_Departement> Departements { get; set; }
    public DbSet<T_Grade> Grades { get; set; }
    public DbSet<T_Enseignant> Enseignants { get; set; }
    public DbSet<T_Etudiant> Etudiants { get; set; }
}
