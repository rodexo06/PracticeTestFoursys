using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application.ViewModels._Base {
    [ExcludeFromCodeCoverage]
    public class RequestResultErrorItem {
        public string? Key { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? PropertyName { get; set; }
    }
}
