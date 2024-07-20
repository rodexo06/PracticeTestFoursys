using FluentValidation;

using PracticeTestFoursys.Application.Commands._Base;

using MediatR;

using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application.Mediator.Pipelines {
    [ExcludeFromCodeCoverage]
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : ResponseState {
        private readonly IEnumerable<IValidator<TRequest?>> _validators;
        private readonly IResponseState _commandResponse;

        public ValidatorBehavior(
            IEnumerable<IValidator<TRequest?>> validators,
            IResponseState commandResponse)
        {
            _validators = validators;
            _commandResponse = commandResponse;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            //if (failures.Count != 0)
                //return (TResponse)_commandResponse.ResponseWithNotification(request.GetType(), failures);

            return await next();
        }
    }
}
