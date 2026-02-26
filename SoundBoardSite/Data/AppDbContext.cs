using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AudioFile> AudioFiles => Set<AudioFile>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<AudioFileCategory> AudioFileCategories => Set<AudioFileCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite PK for join table
        modelBuilder.Entity<AudioFileCategory>()
            .HasKey(x => new { x.AudioFileId, x.CategoryId });

        modelBuilder.Entity<AudioFileCategory>()
            .HasOne(x => x.AudioFile)
            .WithMany(a => a.AudioFileCategories)
            .HasForeignKey(x => x.AudioFileId);

        modelBuilder.Entity<AudioFileCategory>()
            .HasOne(x => x.Category)
            .WithMany(c => c.AudioFileCategories)
            .HasForeignKey(x => x.CategoryId);

        // Optional: enforce unique category names
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}