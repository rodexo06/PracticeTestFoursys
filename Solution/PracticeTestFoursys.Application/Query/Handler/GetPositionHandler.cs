using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MediatR;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Application.ViewModels;
using PracticeTestFoursys.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PracticeTestFoursys.Application.Query.Handler
{
    public class GetPositionHandler : IRequestHandler<GetPositionbyClientQuery, List<PositionModel>>,
                                    IRequestHandler<GetPositionbyClientSummaryQuery, ProductPositionSummaryModel>,
                                    IRequestHandler<GetPositionTop10Query, List<PositionModel>>
    {
        private readonly IMapper _mapper;
        private readonly IPositionRepository _positionRepository;

        public GetPositionHandler(IMapper mapper, IPositionRepository positionRepository)
        {
            _mapper = mapper;
            _positionRepository = positionRepository;
        }

        // retornar os registros do ultimo positionid do cliente x
        public async Task<List<PositionModel>> Handle(GetPositionbyClientQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ClientId))
            {
                throw new ArgumentNullException(nameof(request.ClientId));
            }
            var positions = _positionRepository.FindByClientId(request.ClientId).ToList();
            var maxDate = positions.Max(x => x.Date);
            var maxPositionId = positions.First(x => x.Date == maxDate).PositionId;
            var filteredPositions = positions.Where(x => x.PositionId == maxPositionId).ToList();
            return _mapper.Map<List<PositionModel>>(filteredPositions);
        }

        public async Task<ProductPositionSummaryModel> Handle(GetPositionbyClientSummaryQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ClientId))
            {
                throw new ArgumentNullException(nameof(request.ClientId));
            }

            var latestPositions = _positionRepository.FindByClientId(request.ClientId)
                .GroupBy(p => p.PositionId)
                .Select(g => g.OrderByDescending(p => p.Date).FirstOrDefault());

            var productPositionSummaries = latestPositions
                .GroupBy(p => p.ProductId)
                .Select(g => new ProductPositionSummary
                {
                    ProductId = g.Key,
                    TotalValue = g.Sum(p => p.Value)
                }).ToList();

            return _mapper.Map<ProductPositionSummaryModel>(productPositionSummaries);
        }

        public async Task<List<PositionModel>> Handle(GetPositionTop10Query request, CancellationToken cancellationToken)
        {
            List<PositionModel> positionModels = new List<PositionModel>();
            var top10Positions = _positionRepository.GetTop10PositionsByValueAsync();
            List<Position> positions = top10Positions.GetAwaiter().GetResult();
            positionModels = _mapper.Map<List<PositionModel>>(positions);
            return positionModels;
        }
    }
}
