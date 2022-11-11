using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Models.Mediatr.MonthlyProficCommands;

namespace Movie_Library_Final_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthlyReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MonthlyReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("Monthly Report")]
        public async Task<IActionResult> GetAllMovies(int month, int year)
        {
            var result = await _mediator.Send(new GetMonthlyProfitReportCommand(month, year));
            return StatusCode((int)result.StatusCode, new { result.Value, result.Message });
        }
    }
}
