using Books.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Books.DAL.Configurations;

internal static class TradeRequestEntityConfiguration
{
    internal static void AddTradeRequestEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TradeRequest>()
            .HasOne(t => t.Owner);

        modelBuilder.Entity<TradeRequest>()
            .Property(u => u.TradeStatus)
            .HasMaxLength(40);
    }
}
