using Microsoft.EntityFrameworkCore;
using NotesByDayApi.Common.Models;

namespace NotesByDayApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(e => e.Notes)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        });
    }
}