using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dal.Configurations;

/// <summary>
/// Конфигурация Transaction
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id");
        
        builder.Property(p => p.TransactionDate)
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("transaction_data");
        
        builder.Property(p => p.MoneyAmount)
            .IsRequired()
            .HasColumnName("money_amount");
        
        builder.Property(p => p.TransactionType)
            .IsRequired()
            .HasColumnName("transaction_type");
        
        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnName("userId");
        
        builder.Property(p => p.CardId)
            .IsRequired()
            .HasColumnName("cardId");
        
        builder.HasOne(p => p.User)
            .WithMany(p => p.Transactions)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Card)
            .WithMany(p => p.Transactions)
            .HasForeignKey(p => p.CardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}