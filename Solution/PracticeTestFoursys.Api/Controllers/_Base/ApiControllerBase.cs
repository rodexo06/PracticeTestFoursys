using PracticeTestFoursys.Application.ViewModels._Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using PracticeTestFoursys.Application.Commands._Base;
using MediatR;

namespace PracticeTestFoursys.Api.Controllers._Base {
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ApiControllerBase<T> : ControllerBase {
        protected readonly IMapper _mapper;
        private IMediator _mediator;

        public ApiControllerBase(IMapper mapper) => _mapper = mapper;
        internal IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult Result(ResponseState responseState)
        {
            var resp = _mapper.Map<RequestResult>(responseState);

            if (resp?.Success ?? false)
                return Ok(resp);
            else
                return BadRequest(resp);
        }

        protected IActionResult Result(Exception exception)
        {
            return BadRequest(new RequestResult(exception));
        }

        protected IActionResult ResultForGet(ResponseState responseState)
        {
            var resp = _mapper.Map<RequestResult>(responseState);

            if (resp?.Success ?? false)
            {
                if (resp.Data != null)
                    return Ok(resp);
                else
                    return NoContent();
            }
            else
            {
                return BadRequest(resp);
            }
        }

        private ILogger<T>? _logger;

        internal ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>()
            ?? throw new InvalidOperationException("ILogger não está registrado no DI container.");
    }
}
