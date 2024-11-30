using Microsoft.EntityFrameworkCore;
using Books.DAL.DTOs;
using Books.Core;
using Books.DAL.Configurations;

namespace Books.DAL;

public class BooksContext: DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<TradeRequest> TradeRequests { get; set; }

    public BooksContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Options.ConnectionString;
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddBookEntityConfiguration();
        modelBuilder.AddUserEntityConfiguration();
        modelBuilder.AddTradeRequestEntityConfiguration();
    }
}
