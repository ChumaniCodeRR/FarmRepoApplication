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
    public class SeedController : ControllerBase
    {
        private readonly ISeedService seedService;

        public SeedController(ISeedService service)
        {
            this.seedService = service;

        }

        [AllowAnonymous]
        [HttpGet("GetAllSeeds")]
        public async Task<IActionResult> GetAllSeeds()
        {
            var data = await this.seedService.GetAllSeeds();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetBredBySeedID")]
        public async Task<IActionResult> GetBredBySeedID(int seed_id)
        {
            var data = await this.seedService.GetBredBySeedID(seed_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPost("CreateSeed")]
        public async Task<IActionResult> CreateSeed(SeedModal _data)
        {
            var data = await this.seedService.CreateSeed(_data);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPut("UpdateSeed")]
        public async Task<IActionResult> UpdateSeed(SeedModal _data, int seed_id)
        {
            var data = await this.seedService.UpdateSeed(_data, seed_id);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpDelete("RemoveSeed")]
        public async Task<IActionResult> RemoveSeed(int seed_id)
        {
            var data = await this.seedService.RemoveSeed(seed_id);
            return Ok(data);
        }
    }
}
