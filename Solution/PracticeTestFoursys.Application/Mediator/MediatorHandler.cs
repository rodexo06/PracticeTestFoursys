using PracticeTestFoursys.Application.Commands._Base;

using MediatR;

using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application.Mediator {
    [ExcludeFromCodeCoverage]
    public class MediatorHandler : IMediatorHandler {
        private readonly IMediator _mediator;
        public MediatorHandler(
           IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResponseState> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }
    }
}
