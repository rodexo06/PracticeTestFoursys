using Microsoft.EntityFrameworkCore;

namespace PracticeTestFoursys.Infra.Context
{
    public class PracticeTestFoursysContext : DbContext
    {

        public PracticeTestFoursysContext(DbContextOptions<PracticeTestFoursysContext> options) : base(options)
        {
        }
    }
}
