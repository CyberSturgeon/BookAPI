using Microsoft.EntityFrameworkCore;
using Books.DAL.DTOs;
using Books.Core;

namespace Books.DAL;

public class BooksContext: DbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Book> Book { get; set; }

    public DbSet<TradeRequest> TradeRequest { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Options.ConnectionString;
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(u => u.Books)
        .WithMany(b => b.Users);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Trades)
        .WithOne(t => t.Buyer);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Trades)
        .WithOne(t => t.Owner);

        modelBuilder.Entity<Book>()
        .HasMany(b => b.TradeRequests)
        .WithOne(t => t.Book);
    }
}
