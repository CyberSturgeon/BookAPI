using Books.DAL.Configurations;
using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL;

public class BooksContext: DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<TradeRequest> TradeRequests { get; set; }

    public BooksContext(DbContextOptions<BooksContext> opts) : base(opts)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddBookEntityConfiguration();
        modelBuilder.AddUserEntityConfiguration();
        modelBuilder.AddTradeRequestEntityConfiguration();
    }
}
