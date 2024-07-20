
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application.ViewModels._Base {
    [ExcludeFromCodeCoverage]
    public class RequestResult {
        public List<RequestResultErrorItem> Errors { get; set; }
        public object? Data { get; set; }
        public bool Success { get; set; }

        public RequestResult()
        {
            Errors = new List<RequestResultErrorItem>();
        }

        public RequestResult(Exception ex)
        {
            Success = false;
            Errors = new List<RequestResultErrorItem>
            {
                new RequestResultErrorItem()
                {
                    Message = ex.ToString()
                }
            };
        }

        public RequestResult(object data)
        {
            Success = true;
            Data = data;
            Errors = new List<RequestResultErrorItem>();
        }

        public RequestResult(Exception ex, ILogger logger, string method)
        {
            Success = false;
            Errors = new List<RequestResultErrorItem>
            {
                new RequestResultErrorItem()
                {
                    Message = ex.ToString()
                }
            };
        }

    }
}
