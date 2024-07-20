using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTestFoursys.Console.Contracts
{
    public interface IApiDataFetcher
    {
        Task<Stream> FetchDataAsync(string endpoint, Dictionary<string, string> headers);
    }
}
