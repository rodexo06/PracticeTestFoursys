using AutoMapper;

using System.Diagnostics.CodeAnalysis;
using PracticeTestFoursys.Application.Commands._Base;

[ExcludeFromCodeCoverage]
public abstract class CommandHandlerBase
{
    protected readonly IMapper _mapper;
    protected readonly IResponseState _responseState;

    protected CommandHandlerBase(
        IMapper mapper,
        IResponseState responseState)
    {
        _mapper = mapper;
        _responseState = responseState;
    }
}
