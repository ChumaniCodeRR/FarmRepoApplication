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
    public class BredController : ControllerBase
    {
        private readonly IBredService bredService;

        public BredController(IBredService service)
        {
            this.bredService = service;

        }

        [AllowAnonymous]
        [HttpGet("GetAllBreds")]
        public async Task<IActionResult> GetAllBreds()
        {
            var data = await this.bredService.GetAllBreds();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetBredByCode")]
        public async Task<IActionResult> GetBredByCode(string bred_code)
        {
            var data = await this.bredService.GetBredByCode(bred_code);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPost("CreateBred")]
        public async Task<IActionResult> CreateBred(BredModal _data)
        {
            var data = await this.bredService.CreateBred(_data);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPut("UpdateMedical")]
        public async Task<IActionResult> UpdateBred(BredModal _data, string bred_code)
        {
            var data = await this.bredService.UpdateBred(_data, bred_code);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpDelete("RemoveBred")]
        public async Task<IActionResult> RemoveBred(string bred_code)
        {
            var data = await this.bredService.RemoveBred(bred_code);
            return Ok(data);
        }

    }
}
