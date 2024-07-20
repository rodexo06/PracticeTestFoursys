

using PracticeTestFoursys.Console.Contracts;

public class ApiDataFetcher : IApiDataFetcher
{
    private readonly HttpClient _httpClient;

    public ApiDataFetcher(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> FetchDataAsync(string endpoint, Dictionary<string, string> headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }
}