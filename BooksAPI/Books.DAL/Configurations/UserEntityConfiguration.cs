using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Configurations;

internal static class UserEntityConfiguration
{
    internal static void AddUserEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Books)
            .WithMany(b => b.Users);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Trades)
            .WithOne(t => t.Buyer);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasMaxLength(15);
    }
}
