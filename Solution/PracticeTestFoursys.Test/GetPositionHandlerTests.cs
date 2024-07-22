
using AutoMapper;
using Moq;
using PracticeTestFoursys.Application.Query;
using PracticeTestFoursys.Application.Query.Handler;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Application.ViewModels;
using PracticeTestFoursys.Domain.Entities;
using Xunit;

namespace PracticeTestFoursys.Test
{
    public class GetPositionHandlerTests
    {
        private readonly Mock<IPositionRepository> _mockPositionRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetPositionHandler _handler;

        public GetPositionHandlerTests()
        {
            _mockPositionRepository = new Mock<IPositionRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetPositionHandler(_mockMapper.Object, _mockPositionRepository.Object);
        }

        [Fact]
        public async Task Handle_GetPositionbyClientQuery_ShouldReturnMappedPositions()
        {
            // Arrange
            var clientId = "test-client-id";
            var positions = new List<Position>
        {
            new Position { PositionId = "1", Date = new DateTime(2023, 1, 1), ProductId = "product-1", ClientId = clientId, Value = 100, Quantity = 10 },
            new Position { PositionId = "1", Date = new DateTime(2023, 2, 1), ProductId = "product-1", ClientId = clientId, Value = 200, Quantity = 20 },
            new Position { PositionId = "2", Date = new DateTime(2023, 3, 1), ProductId = "product-2", ClientId = clientId, Value = 300, Quantity = 30 }
        };

            _mockPositionRepository.Setup(repo => repo.FindByClientId(clientId)).Returns(positions.AsQueryable());
            _mockMapper.Setup(m => m.Map<List<PositionModel>>(It.IsAny<List<Position>>())).Returns(new List<PositionModel>());

            var query = new GetPositionbyClientQuery { ClientId = clientId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockPositionRepository.Verify(repo => repo.FindByClientId(clientId), Times.Once);
            _mockMapper.Verify(m => m.Map<List<PositionModel>>(It.IsAny<List<Position>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GetPositionbyClientSummaryQuery_ShouldReturnMappedSummary()
        {
            // Arrange
            var clientId = "test-client-id";
            var positions = new List<Position>
        {
            new Position { PositionId = "1", ProductId = "product-1", Date = new DateTime(2023, 1, 1), ClientId = clientId, Value = 100, Quantity = 10 },
            new Position { PositionId = "1", ProductId = "product-1", Date = new DateTime(2023, 2, 1), ClientId = clientId, Value = 200, Quantity = 20 },
            new Position { PositionId = "2", ProductId = "product-2", Date = new DateTime(2023, 3, 1), ClientId = clientId, Value = 300, Quantity = 30 }
        };

            _mockPositionRepository.Setup(repo => repo.FindByClientId(clientId)).Returns(positions.AsQueryable());
            _mockMapper.Setup(m => m.Map<ProductPositionSummaryModel>(It.IsAny<List<ProductPositionSummary>>())).Returns(new ProductPositionSummaryModel());

            var query = new GetPositionbyClientSummaryQuery { ClientId = clientId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockPositionRepository.Verify(repo => repo.FindByClientId(clientId), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductPositionSummaryModel>(It.IsAny<List<ProductPositionSummary>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GetPositionTop10Query_ShouldReturnMappedTop10Positions()
        {
            // Arrange
            var positions = new List<Position>
        {
            new Position { PositionId = "1", Date = new DateTime(2023, 1, 1), ProductId = "product-1", ClientId = "client-1", Value = 100, Quantity = 10 },
            // Add more positions as needed for the test
        };

            _mockPositionRepository.Setup(repo => repo.GetTop10PositionsByValueAsync()).ReturnsAsync(positions);
            _mockMapper.Setup(m => m.Map<List<PositionModel>>(It.IsAny<List<Position>>())).Returns(new List<PositionModel>());

            var query = new GetPositionTop10Query();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mockPositionRepository.Verify(repo => repo.GetTop10PositionsByValueAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<List<PositionModel>>(It.IsAny<List<Position>>()), Times.Once);
        }
    }
}
