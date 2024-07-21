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

        public DbSet<Position> Position { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
