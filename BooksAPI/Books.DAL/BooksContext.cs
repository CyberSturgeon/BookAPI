using Microsoft.EntityFrameworkCore;
using Books.DAL.DTOs;
using Books.Core;

namespace Books.DAL;

public class BooksContext: DbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Book> Book { get; set; }

    public DbSet<TradeRequest> TradeRequest { get; set; }

    public BooksContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Options.ConnectionString;
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region RelationShips

        modelBuilder.Entity<User>()
        .HasMany(u => u.Books)
        .WithMany(b => b.Users);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Trades)
        .WithOne(t => t.Buyer);

        modelBuilder.Entity<TradeRequest>()
        .HasOne(t => t.Owner);

        modelBuilder.Entity<Book>()
        .HasMany(b => b.TradeRequests)
        .WithOne(t => t.Book);

        #endregion

        #region Columns

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasMaxLength(15);

        modelBuilder.Entity<Book>()
            .Property(u => u.Name)
            .HasMaxLength(40);

        modelBuilder.Entity<Book>()
            .Property(u => u.Author)
            .HasMaxLength(40);

        modelBuilder.Entity<Book>()
            .Property(u => u.Genre)
            .HasMaxLength(40);

        modelBuilder.Entity<TradeRequest>()
            .Property(u => u.TradeStatus)
            .HasMaxLength(40);

        #endregion
    }
}
