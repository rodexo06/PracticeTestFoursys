using PracticeTestFoursys.Application.Commands._Base;

namespace PracticeTestFoursys.Application.Mediator {
    public interface IMediatorHandler {
        Task<ResponseState> EnviarComando<T>(T comando) where T : Command;
    }
}
