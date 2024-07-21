using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Console.Contracts;
using PracticeTestFoursys.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;



public class JsonDataProcessor : IJsonDataProcessor
{
    private readonly ILogger<JsonDataProcessor> _logger;
    private readonly IApiDataFetcher _dataFetcher;
    private readonly IPositionRepository _positionRepository;
    private readonly string table = "Positions";
    private readonly string columns = "positionid, productid, clientid, date , value, quantity";
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "X-Test-Key", "krq5XKX-jbp0ftv*ntw" }
        };

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDataProcessor"/> class.
    /// </summary>
    /// <param name="dataFetcher">The data fetcher used to retrieve data from an API.</param>
    /// <param name="positionRepository">The repository used to persist the data.</param>
    /// <param name="logger">The logger instance for logging information and errors.</param>
    public JsonDataProcessor(IApiDataFetcher dataFetcher, IPositionRepository positionRepository, ILogger<JsonDataProcessor> logger)
    {
        _dataFetcher = dataFetcher;
        _positionRepository = positionRepository;
        _logger = logger;
    }

    /// <summary>
    /// Processes the data asynchronously by fetching it from an API and storing it in a repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task ProcessDataAsync()
    {
        try
        {
            var listBuffer = new List<Position>();
            int totalProgress = 1;
            int sumProgress = 0;
            Stopwatch stopwatch = new Stopwatch();
            
            _logger.LogInformation("Start fetch data from client");
            Stream? stream = await _dataFetcher.FetchDataAsync("/candidate/positions", headers);

            JsonDocumentOptions documentOptions = new JsonDocumentOptions { AllowTrailingCommas = true };
            
            using (JsonDocument? jsonDocument = await JsonDocument.ParseAsync(stream, documentOptions))
            {
                var enumerator = jsonDocument.RootElement.EnumerateArray();
                totalProgress = jsonDocument.RootElement.GetArrayLength();
                stopwatch.Start();
                _logger.LogInformation("Start bulk process");
                foreach (var element in enumerator)
                {
                    string a = element.GetRawText();
                    var item = JsonSerializer.Deserialize<Position>(element.GetRawText());

                    if (item != null)
                    {
                        listBuffer.Add(item);

                        if (listBuffer.Count >= 50000)
                        {
                            _positionRepository.BulkMergeEF(listBuffer);
                            sumProgress += listBuffer.Count;
                            listBuffer.Clear();
                            _logger.LogInformation($"Process Data {((double)sumProgress / totalProgress) * 100}%");
                        }
                    }
                }
            }

            if (listBuffer.Count > 0)
            {
                _positionRepository.BulkMergeEF(listBuffer);
            }

            TimeSpan elapsed = stopwatch.Elapsed;
            stopwatch.Stop();

            _logger.LogInformation($"Time elapsed: {elapsed.TotalSeconds} seconds");
            _logger.LogInformation("Finish bulk process");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} - {ex.InnerException}");
        }
    }
}