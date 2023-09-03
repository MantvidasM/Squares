using Microsoft.AspNetCore.Mvc;
using SquaresApi.Dto;
using SquaresApi.Interfaces;

namespace SquaresApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SquaresController : Controller
	{
        private readonly ISquaresService _squaresService;

        public SquaresController(ISquaresService squaresService)
		{
            _squaresService = squaresService;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult GetSquares()
        {
            var squares = _squaresService.GetSquares();
            return Ok(squares);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ImportPointsAsync(IFormFile file)
        {
            var imported = await _squaresService.ImportPoints(file);
            if (!imported)
            {
                return Problem("", "Import points failed");
            }
            return Ok("Points imported successfully");
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult CreatePoint([FromBody] PointDto point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _squaresService.CreatePoint(point);
            if (!created)
            {
                return Problem("", "Create point failed");
            }
            return Ok("Point created successfully");
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult DeletePoint([FromBody] PointDto point)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleted = _squaresService.DeletePoint(point);
            if (!deleted)
            {
                return Problem("", "Delete point failed");
            }
            return Ok("Point deleted successfully");
        }
    }
}
