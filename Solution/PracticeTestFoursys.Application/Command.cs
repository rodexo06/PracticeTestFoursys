using MediatR;
using System.ComponentModel.DataAnnotations;

using System.Diagnostics.CodeAnalysis;
using PracticeTestFoursys.Application.Commands._Base;

[ExcludeFromCodeCoverage]
public abstract class Command : Message, IRequest<ResponseState>
{
    public DateTime Timestamp { get; private set; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}

