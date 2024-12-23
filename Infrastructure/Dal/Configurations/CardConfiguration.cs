using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

/// <summary>
/// Конфигурация Card
/// </summary>
public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id");
        
        builder.Property(p => p.Number)
            .IsRequired()
            .HasColumnName("number");
        
        builder.Property(p => p.Cvv)
            .IsRequired()
            .HasColumnName("cvv");
        
        builder.Property(p => p.Pin)
            .IsRequired()
            .HasColumnName("pin");
        
        builder.Property(p => p.ExpirationDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("expiration_date");
        
        builder.Property(p => p.Balance)
            .IsRequired()
            .HasColumnName("balance");
        
        builder.Property(p => p.Currency)
            .IsRequired()
            .HasColumnName("currency");
        
        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnName("userId");
        
        builder.HasOne(p => p.User)
            .WithMany(p => p.Cards)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.Transactions)
            .WithOne(p => p.Card)
            .HasForeignKey(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}