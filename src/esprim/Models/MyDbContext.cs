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
            // Primary Key configurations
            modelBuilder.Entity<Classe>().HasKey(c => c.CodeClasse);
            modelBuilder.Entity<Groupe>().HasKey(g => g.CodeGroupe);
            modelBuilder.Entity<Departement>().HasKey(d => d.CodeDepartement);

            // Relationships and Foreign Key configurations
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Groupe)
                .WithMany(g => g.Classes)
                .HasForeignKey(c => c.CodeGroupe)
                .OnDelete(DeleteBehavior.SetNull);  // Cascade or SetNull based on your business rules

            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Departement)
                .WithMany(d => d.Classes)
                .HasForeignKey(c => c.CodeDepartement)
                .OnDelete(DeleteBehavior.SetNull);  // Cascade or SetNull based on your business rules

            // Other relationships can be configured here as needed
            modelBuilder.Entity<Enseignant>()
                .HasOne(e => e.Departement)
                .WithMany(d => d.Enseignants)
                .HasForeignKey(e => e.CodeDepartement);

            // Define other foreign key constraints for your models as well
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
