using PracticeTestFoursys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Application.Repositories
{
    public interface IPositionRepository : IBaseRepository<Position>
    {
        IQueryable<Position> FindByClientId(string clientId);
        Task<List<Position>> GetTop10PositionsByValueAsync();

    }
}
