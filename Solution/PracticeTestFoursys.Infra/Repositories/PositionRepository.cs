using Microsoft.EntityFrameworkCore;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Domain.Entities;
using PracticeTestFoursys.Infra.Context;

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

        public IQueryable<Position> GetPositionbyClientQuery(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Position>> GetTop10PositionsByValueAsync()
        {
            var top10Positions = await _context.Positions
           .OrderByDescending(p => p.Value)
           .Take(10)
           .ToListAsync();

            return top10Positions;
        }

        //public IQueryable<Position> GetPositionbyClientQuery(string clientId)
        //{
        //    var query = from p in _context.Positions
        //                   where p.ClientId == clientId
        //                   group p by p.PositionId into g
        //                   select new
        //                   {
        //                       PositionId = g.Key,
        //                       MaxDate = g.Max(x => x.Date)
        //                   };

        //    //// Consulta principal para obter as posições que correspondem à data máxima
        //    //var query = from position in _context.Positions
        //    //            join maxDate in maxDates 
        //    //            on new { position.PositionId, position.Date } 
        //    //                equals new { maxDate.PositionId, maxDate.MaxDate }
        //    //            select position;

        //    return query;
        //}

    }
}
