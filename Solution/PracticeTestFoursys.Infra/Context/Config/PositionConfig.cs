using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeTestFoursys.Domain.Entities;

namespace PracticeTestFoursys.Infra.Context.Config
{
    public class PositionConfig
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Position");

            builder.HasKey(p => new { p.PositionId, p.Date });

            builder.Property(p => p.PositionId)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.ProductId)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnType("decimal(18, 8)");

            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18, 8)");
        }
    }
}