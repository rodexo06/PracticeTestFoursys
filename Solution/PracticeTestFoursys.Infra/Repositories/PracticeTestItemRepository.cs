using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Domain.Entities;
using PracticeTestFoursys.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Infra.Repositories
{
    public class PracticeTestItemRepository : BaseRepository<PracticeTestItem>, IPracticeTestItemRepository
    {
        public PracticeTestItemRepository(PracticeTestFoursysContext context) : base(context)
        {

        }

    }
}
