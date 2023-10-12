using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application_test_repo.Modal;
using Application_test_repo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Application_test_repo.Controllers
{
    [Authorize]
    [EnableRateLimiting("fixedwindow")]
    [Route("api/[controller]")]
    [ApiController]

    public class FertilizerController : ControllerBase
    {
        private readonly IFertilizer fertilizerService;

        public FertilizerController(IFertilizer service)
        {
            this.fertilizerService = service;

        }

        [AllowAnonymous]
        [HttpGet("GetAllFertilizers")]
        public async Task<IActionResult> GetAllFertilizers()
        {
            var data = await this.fertilizerService.GetAllFertilizers();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetFertilizerById")]
        public async Task<IActionResult> GetFertilizerById(int fertilizer_id)
        {
            var data = await this.fertilizerService.GetFertilizerById(fertilizer_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("CreateFertilizer")]
        public async Task<IActionResult> CreateFertilizer(FertilizerModal _data)
        {
            var data = await this.fertilizerService.CreateFertilizer(_data);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPut("UpdateFertilizer")]
        public async Task<IActionResult> UpdateFertilizer(FertilizerModal _data, int fertilizer_id)
        {
            var data = await this.fertilizerService.UpdateFertilizer(_data, fertilizer_id);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpDelete("RemoveFertilizer")]
        public async Task<IActionResult> RemoveFertilizer(int fertilizer_id)
        {
            var data = await this.fertilizerService.RemoveFertilizer(fertilizer_id);
            return Ok(data);
        }

    }
}
