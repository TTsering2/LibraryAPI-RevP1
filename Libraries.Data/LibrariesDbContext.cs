using Microsoft.EntityFrameworkCore;
using Libraries.Models;

namespace Libraries.Data;

public class LibrariesDbContext : DbContext {
    public LibrariesDbContext() : base() {}
    public LibrariesDbContext(DbContextOptions<LibrariesDbContext> options) : base(options) {}
    public DbSet<Book> Books {get; set;}
    public DbSet<Genre> Genres {get; set;}

    public DbSet<Author> Authors {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId);
    }
}