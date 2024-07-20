using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PracticeTestFoursys.Api.Controllers._Base;

namespace PracticeTestFoursys.Api.Controllers
{
    public class PositionsController : ApiControllerBase<PositionsController>
    {

        public PositionsController(IMapper mapper) : base(mapper) { }

        //[HttpGet("/client/{clientId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetPositionbyClient()
        //{
        //    try
        //    {
        //        //return View();
        //        //ResponseState 
        //        //return ResultForGet(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result(ex);
        //    }
        //}

        //[HttpGet("/client/{clientId}/summary")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetPositionbyClientSummary()
        //{
        //    try
        //    {
        //        //return View();
        //        //ResponseState 
        //        //return ResultForGet(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result(ex);
        //    }
        //}

        //[HttpGet("top10")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> GetPositionTop10()
        //{
        //    try
        //    {
        //        //return View();
        //        //ResponseState 
        //        //return ResultForGet(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result(ex);
        //    }
        //}
    }
}
