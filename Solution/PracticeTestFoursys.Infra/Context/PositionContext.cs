using Microsoft.EntityFrameworkCore;
using PracticeTestFoursys.Domain.Entities;
using System.Reflection;

namespace PracticeTestFoursys.Infra.Context
{
    public class PositionContext : DbContext
    {

        public PositionContext(DbContextOptions<PositionContext> options) : base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Position>()
                .HasKey(p => new { p.PositionId, p.Date });
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLowerInvariant());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLowerInvariant());
                }
            }
        }
    }
}
