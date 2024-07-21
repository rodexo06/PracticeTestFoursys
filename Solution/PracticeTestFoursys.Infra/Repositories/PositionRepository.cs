using Microsoft.EntityFrameworkCore;
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
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(PositionContext context) : base(context)
        {

        }
        public IQueryable<Position> FindByClientId(string clientId)
        {
            return _context.Positions.Where(x => x.ClientId == clientId);
        }
        public async Task<List<Position>> GetTop10PositionsByValueAsync()
        {
            var top10Positions = await _context.Positions
           .OrderByDescending(p => p.Value)
           .Take(10)
           .ToListAsync();

            return top10Positions;
        }
    }
}
