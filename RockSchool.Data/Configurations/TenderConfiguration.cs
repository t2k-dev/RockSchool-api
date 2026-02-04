using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RockSchool.Domain.Entities;

namespace RockSchool.Data.Configurations;

public class TenderConfiguration : IEntityTypeConfiguration<Tender>
{
    public void Configure(EntityTypeBuilder<Tender> builder)
    {
        builder.ToTable("Tenders");
        builder.HasKey(t => t.TenderId);
        
        builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.PaidOn).IsRequired();
        builder.Property(t => t.TenderType).IsRequired().HasConversion<int>();
        builder.Property(t => t.SubscriptionId).IsRequired();
        
        builder.HasOne(t => t.Subscription).WithMany(s => s.Tenders).HasForeignKey("SubscriptionId");
    }
}