using AutoMapper;
using PracticeTestFoursys.Application.Mapping;
using PracticeTestFoursys.Domain.Entities;


namespace PracticeTestFoursys.Application.ViewModels
{
    public class ProductPositionSummaryModel : IMapFrom<ProductPositionSummary>
    {
        public string ProductId { get; set; }
        public decimal TotalValue { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductPositionSummaryModel, ProductPositionSummary>().ReverseMap();
        }
    }
}
