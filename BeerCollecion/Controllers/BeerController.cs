using BeerCollectionBLL.DTO;
using BeerCollectionBLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerCollecion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        IBeerService _beerService;
        public BeerController(IBeerService beerService)
        {
            _beerService = beerService;
        }
        [HttpPost("add-beer")]
        public async Task<IActionResult> AddBeer(BeerCreatedDto beer)
        {
            try
            {
                var beerId = await _beerService.AddBearAsync(beer);
                return CreatedAtAction(nameof(GetBeerById), new { id = beerId }, new { Message = "Beer added successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting data with message:{ex.Message}");
            }
        }
        [HttpGet("get-beer-by-id/{id}")]
        public async Task<IActionResult> GetBeerById(int id)
        {
            try
            {
                var beer = await _beerService.GetBeerByIdAsync(id);
                if (beer == null)
                    return NotFound();
                return Ok(beer);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"An error occurred while getting data with message:{ex.Message}");
            }
        }

        [HttpGet("get-all-beers")]
        public async Task<IActionResult> GetAllBeers()
        {
            try
            {
                var beerDtos = await _beerService.GetAllBeersAsync();
                return Ok(beerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting data with message:{ex.Message}");
            }
        }

        [HttpGet("get-beers-by-name")]
        public async Task<IActionResult> GetBeersByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Search term cannot be empty.");
            }

            try
            {
                var beerDtos = await _beerService.SearchBeersByNameAsync(name);

                if (!beerDtos.Any())
                {
                    return NotFound("No beers found matching the search term.");
                }

                return Ok(beerDtos); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting data with message:{ex.Message}");
            }
        }
        [HttpPut("update-rating/{id}")]
        public async Task<IActionResult> UpdateBeerRating(int id, [FromBody] List<decimal> ratingList)
        {
            try
            {
                await _beerService.UpdateBeerRatingAsync(id, ratingList);
                return Ok("Beer rating updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting data with message:{ex.Message}");
            }
        }
    }
}
