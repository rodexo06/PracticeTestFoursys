using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeTestFoursys.Domain.Entities;

namespace PracticeTestFoursys.Infra.Context.Config
{
    public class PositionConfig
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("positions");


            builder.Property(p => p.PositionId)
                .IsRequired()
                .HasColumnName("positionid")
                .HasMaxLength(100);

            builder.Property(p => p.Date)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(p => p.ProductId)
                .IsRequired()
                .HasColumnName("productid")
                .HasMaxLength(100); 

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasColumnName("clientid")
                .HasMaxLength(100);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("value")
                .HasColumnType("decimal(18, 8)");

            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnName("quantity")
                .HasColumnType("decimal(18, 8)");

            builder.HasKey(p => new { p.Date, p.PositionId});
        }
    }
}