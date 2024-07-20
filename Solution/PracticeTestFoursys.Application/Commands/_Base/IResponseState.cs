namespace PracticeTestFoursys.Application.Commands._Base
{
    public interface IResponseState
    {
        object? Data { get; }
        bool Success { get; }

        ResponseState Response(object? data = null);

    }
}
