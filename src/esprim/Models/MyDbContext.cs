using Microsoft.EntityFrameworkCore;

namespace mini.project.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        // DbSet properties
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<FicheAbsence> FichesAbsence { get; set; }
        public DbSet<FicheAbsenceSeance> FichesAbsenceSeances { get; set; }
        public DbSet<LigneFicheAbsence> LignesFicheAbsence { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }

        // OnModelCreating configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classe>()
                .HasKey(c => c.CodeClasse);

            modelBuilder.Entity<Departement>()
                .HasKey(d => d.CodeDepartement);

            modelBuilder.Entity<Enseignant>()
                .HasKey(e => e.CodeEnseignant);

            modelBuilder.Entity<Etudiant>()
                .HasKey(e => e.CodeEtudiant);

            modelBuilder.Entity<FicheAbsence>()
                .HasKey(f => f.CodeFicheAbsence);

            modelBuilder.Entity<FicheAbsenceSeance>()
                .HasKey(f => new { f.CodeFicheAbsence, f.CodeSeance });

            modelBuilder.Entity<LigneFicheAbsence>()
                .HasKey(l => new { l.CodeFicheAbsence, l.CodeEtudiant });

            modelBuilder.Entity<Matiere>()
                .HasKey(m => m.CodeMatiere);

            modelBuilder.Entity<Groupe>()
                .HasKey(g => g.CodeGroupe);

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.CodeGrade);

        }
    }
}
