using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Configurations;

internal static class BookEntityConfiguration
{
    internal static void AddBookEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany(b => b.TradeRequests)
            .WithOne(t => t.Book);

        modelBuilder.Entity<Book>()
            .Property(u => u.Name)
            .HasMaxLength(40);

        modelBuilder.Entity<Book>()
            .Property(u => u.Author)
            .HasMaxLength(40);

        modelBuilder.Entity<Book>()
            .Property(u => u.Genre)
            .HasMaxLength(40);
    }
}
