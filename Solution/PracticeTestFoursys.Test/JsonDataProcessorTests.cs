using Moq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;
using PracticeTestFoursys.Console.Contracts;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Domain.Entities;

//TODO VALIDAR SE OS TIPO DE DADO DOS CAMPOS ESTAO CORRETOS
//TODO VALIDAR SE OS CAMPOS ESTAO ESTOURANDO O TAMANHO
//TODO VALIDAR SE OS CAMPOS ESTAO CORRETOS


namespace PracticeTestFoursys.Test
{
    public class JsonDataProcessorTests
    {
        [Fact]
        public async Task ProcessDataAsync_ShouldFetchDataAndProcessSuccessfully()
        {
            // Arrange
            var mockDataFetcher = new Mock<IApiDataFetcher>();
            var mockPositionRepository = new Mock<IPositionRepository>();
            var mockLogger = new Mock<ILogger<JsonDataProcessor>>();

            // Sample JSON with fewer items to trigger BulkMergeEF easily in tests
            //var sampleJson = "[{\"PositionId\":1,\"ProductId\":1,\"ClientId\":1,\"Date\":\"2024-07-21T00:00:00Z\",\"Value\":100.0,\"Quantity\":10}]";
            var sampleJson = "[{\"positionId\":\"ukezch\",\"productId\":\"ulfata\",\"clientId\":\"873.715.603-96\",\"date\":\"2024-06-17\",\"value\":158884.391841036991570,\"quantity\":2526.3605645405500000}]";
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sampleJson));

            mockDataFetcher.Setup(m => m.FetchDataAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()))
                           .ReturnsAsync(stream);

            var processor = new JsonDataProcessor(mockDataFetcher.Object, mockPositionRepository.Object, mockLogger.Object);

            // Act
            await processor.ProcessDataAsync();

            // Assert
            mockDataFetcher.Verify(m => m.FetchDataAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
            mockPositionRepository.Verify(m => m.BulkMergeEF(It.IsAny<List<Position>>()), Times.AtLeastOnce);
            
            mockLogger.Verify(l => l.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.AtLeastOnce);
        }
    }
}