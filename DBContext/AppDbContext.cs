using Microsoft.EntityFrameworkCore;
namespace testapi.DBContext;
public class AppDbContext : DbContext
{
    public DbSet<Artikel> Artikel { get; set; } 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("DB_CONNECTION Environment Variable ist nicht gesetzt!");
        if (!optionsBuilder.IsConfigured){
        optionsBuilder.UseNpgsql(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping: C# Artikel -> PostgreSQL artikel
            modelBuilder.Entity<Artikel>().ToTable("artikel");
            modelBuilder.Entity<Artikel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ArtikelName).HasColumnName("artikel_name");
                entity.Property(e => e.ArtikelCode).HasColumnName("artikel_code");
                entity.Property(e => e.ArtikelPreis).HasColumnName("artikel_preis");
                entity.Property(e => e.ArtikelBeschreibung).HasColumnName("artikel_beschreibung");
                entity.Property(e => e.ArtikelHersteller).HasColumnName("artikel_hersteller");
                entity.Property(e => e.ArtikelBestand).HasColumnName("artikel_bestand");
            });

        }

}