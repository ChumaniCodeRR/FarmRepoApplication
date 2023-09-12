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
    public class CropController : ControllerBase
    {
        private readonly ICrop cropService;

        public CropController(ICrop service)
        {
            this.cropService = service;

        }

        [AllowAnonymous]
        [HttpGet("GetAllCrops")]
        public async Task<IActionResult> GetAllCrops()
        {
            var data = await this.cropService.GetAllCrops();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetCropById")]
        public async Task<IActionResult> GetCropById(string crop_id)
        {
            var data = await this.cropService.GetCropById(crop_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPost("CreateCrop")]
        public async Task<IActionResult> CreateCrop(CropModal _data)
        {
            var data = await this.cropService.CreateCrop(_data);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPut("UpdateCrop")]
        public async Task<IActionResult> UpdateCrop(CropModal _data, string crop_id)
        {
            var data = await this.cropService.UpdateCrop(_data, crop_id);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpDelete("RemoveCrop")]
        public async Task<IActionResult> RemoveCrop(string crop_id)
        {
            var data = await this.cropService.RemoveCrop(crop_id);
            return Ok(data);
        }
    }
}
