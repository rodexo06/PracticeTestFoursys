using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application.Commands._Base
{
    [ExcludeFromCodeCoverage]
    public class ResponseState : IResponseState
    {
        public object? Data { get; private set; }

        public bool Success { get; set; }

        public ResponseState Response(object? data = null)
        {
            Data = data;
            return this;
        }
    }
}


// TODO IMPLEMENTAR CATCH DE ERROS