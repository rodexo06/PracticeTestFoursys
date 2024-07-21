using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PracticeTestFoursys.Api.Controllers._Base;
using PracticeTestFoursys.Application.Query;

namespace PracticeTestFoursys.Api.Controllers
{
    public class PositionsController : ApiControllerBase<PositionsController>
    {

        public PositionsController(IMapper mapper) : base(mapper) { }

        [HttpGet("/client/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPositionbyClient([FromRoute] string id)
        {
            try
            {
                var command = new GetPositionbyClientQuery() { ClientId = id };
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Result(ex);
            }
        }

        [HttpGet("/client/{clientId}/summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPositionbyClientSummary([FromRoute] string id)
        {
            try
            {
                var command = new GetPositionbyClientSummaryQuery() { ClientId = id };
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Result(ex);
            }
        }

        [HttpGet("top10")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPositionTop10()
        {
            try
            {
                var command = new GetPositionTop10Query();
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Result(ex);
            }
        }
    }
}
