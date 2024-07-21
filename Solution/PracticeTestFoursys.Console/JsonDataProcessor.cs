using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Console.Contracts;
using PracticeTestFoursys.Domain.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



public class JsonDataProcessor : IJsonDataProcessor
{
    private readonly IApiDataFetcher _dataFetcher;
    private readonly string table = "Positions";
    private readonly string columns = "positionid, productid, clientid, date , value, quantity";
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "X-Test-Key", "krq5XKX-jbp0ftv*ntw" }
        };


    IPositionRepository _positionRepository;
    public JsonDataProcessor(IApiDataFetcher dataFetcher, IPositionRepository positionRepository)
    {
        _dataFetcher = dataFetcher;
        _positionRepository = positionRepository;
    }

    public async Task ProcessDataAsync()
    {

        Stopwatch stopwatch = new Stopwatch();
        Console.WriteLine($"{DateTime.Now}: Start fetch data from client");
        var stream = await _dataFetcher.FetchDataAsync("/candidate/positions", headers);
        var listBuffer = new List<Position>();
        var documentOptions = new JsonDocumentOptions { AllowTrailingCommas = true };
        int totalProgress = 1;
        int sumProgress = 0;

        using (var jsonDocument = await JsonDocument.ParseAsync(stream, documentOptions))
        {
            var enumerator = jsonDocument.RootElement.EnumerateArray();
            totalProgress = jsonDocument.RootElement.GetArrayLength(); 
            stopwatch.Start();
            Console.WriteLine($"{DateTime.Now}: Start bulk process"); 
            foreach (var element in enumerator)
            {
                string a = element.GetRawText();
                var item = JsonSerializer.Deserialize<Position>(element.GetRawText());

                if (item != null)
                {
                    listBuffer.Add(item);

                    if (listBuffer.Count >= 50000)
                    {
                        _positionRepository.BulkInsertBinaryImporter(listBuffer, table, columns);
                        sumProgress += listBuffer.Count;
                        listBuffer.Clear();
                        Console.WriteLine($"{DateTime.Now}: Process Data {sumProgress / totalProgress}%");
                    }
                }
            }
        }

        if (listBuffer.Count > 0)
        {
            _positionRepository.BulkInsertBinaryImporter(listBuffer, table, columns);
        }
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine($"Time elapsed: {elapsed.TotalSeconds} seconds");
        Console.WriteLine($"{DateTime.Now}:  Finish bulk process");
    }
}