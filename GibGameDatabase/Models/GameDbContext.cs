using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GibGameDatabase.Models;

public class GameDbContext : DbContext
{
    public GameDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "GibGameDb.db");
    }

    public DbSet<GameEntry> GameEntries { get; set; }
    public DbSet<FailedEntry> FailedGameEntries { get; set; }

    public string DbPath { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameEntry>().Property(p => p.Languages)
            .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<string>>(v));
        modelBuilder.Entity<GameEntry>().Property(p => p.Hardware)
            .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<List<string>>(v));
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}