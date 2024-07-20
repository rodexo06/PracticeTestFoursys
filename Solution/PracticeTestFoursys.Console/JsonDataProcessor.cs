using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Console.Contracts;
using PracticeTestFoursys.Domain.Entities;
using System.Collections.ObjectModel;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;



public class JsonDataProcessor : IJsonDataProcessor
{
    private readonly IApiDataFetcher _dataFetcher;
    private readonly string table = "PracticeTestItem";
    private readonly string columns = "cIdPracticeTestItemId, cPositionId, cProductId, cClientId, dDate , nValue, nQuantity";
    private readonly Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "X-Test-Key", "krq5XKX-jbp0ftv*ntw" }
        };


    IPracticeTestItemRepository _practiceTestItemRepository;
    public JsonDataProcessor(IApiDataFetcher dataFetcher, IPracticeTestItemRepository practiceTestItemRepository)
    {
        _dataFetcher = dataFetcher;
        _practiceTestItemRepository = practiceTestItemRepository;
    }

    public async Task ProcessDataAsync()
    {

        Console.WriteLine($"Start fetch data from client");
        var stream = await _dataFetcher.FetchDataAsync("/candidate/positions", headers);
        var listBuffer = new List<PracticeTestItem>();
        var documentOptions = new JsonDocumentOptions { AllowTrailingCommas = true };
        int totalProgress = 0;
        int sumProgress = 0;

        using (var jsonDocument = await JsonDocument.ParseAsync(stream, documentOptions))
        {
            var enumerator = jsonDocument.RootElement.EnumerateArray();
            totalProgress = enumerator.Count();
            Console.WriteLine($"Start bulk process");
            foreach (var element in enumerator)
            {
                var item = JsonSerializer.Deserialize<PracticeTestItem>(element.GetRawText());

                if (item != null)
                {
                    listBuffer.Add(item);

                    if (listBuffer.Count >= 50000)
                    {
                        await _practiceTestItemRepository.BulkInsertBinaryImporter(listBuffer, table, columns);
                        listBuffer.Clear();
                        sumProgress += listBuffer.Count;
                        Console.WriteLine($"Process Data {sumProgress / totalProgress}%");
                    }
                }
            }
        }

        if (listBuffer.Count > 0)
        {
            await _practiceTestItemRepository.BulkInsertBinaryImporter(listBuffer, table, columns);
        }
        Console.WriteLine($"Finish bulk process");
    }
}