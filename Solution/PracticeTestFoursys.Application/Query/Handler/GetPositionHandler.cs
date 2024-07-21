using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using MediatR;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Application.ViewModels;
using PracticeTestFoursys.Domain.Entities;

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
            List<PositionModel> positionModels = new List<PositionModel>();
            var latestPositionsQuery = _positionRepository.FindByClientId(request.ClientId)
                .GroupBy(p => p.PositionId)
                .Select(g => g.OrderByDescending(p => p.Date).FirstOrDefault());

            List<Position> positions = latestPositionsQuery.ToList();
            positionModels = _mapper.Map<List<PositionModel>>(positions);
            return positionModels;
        }

        public async Task<ProductPositionSummaryModel> Handle(GetPositionbyClientSummaryQuery request, CancellationToken cancellationToken)
        {
            ProductPositionSummaryModel productPositionSummaryModel = new ProductPositionSummaryModel();
            var latestPositionsQuery = _positionRepository.FindByClientId(request.ClientId)
                .GroupBy(p => p.PositionId)
                .Select(g => g.OrderByDescending(p => p.Date).FirstOrDefault());
            var productPositionSummaries = latestPositionsQuery
           .GroupBy(p => p.ProductId)
           .Select(g => new ProductPositionSummary
           {
               ProductId = g.Key,
               TotalValue = g.Sum(p => p.Value)
           }).ToList();

            productPositionSummaryModel = _mapper.Map<ProductPositionSummaryModel>(productPositionSummaries);
            return productPositionSummaryModel;
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
