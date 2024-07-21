using PracticeTestFoursys.Domain.Entities;
using PracticeTestFoursys.Application.Mapping;
using AutoMapper;

namespace PracticeTestFoursys.Application.ViewModels
{
    public class PositionModel : IMapFrom<Position>
    {
        public required string PracticeTestItemId { get; set; }
        public required string PositionId { get; set; }
        public required string ProductId { get; set; }
        public required string ClientId { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Value { get; set; }
        public required decimal Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PositionModel, Position>().ReverseMap();
        }
    }
}
