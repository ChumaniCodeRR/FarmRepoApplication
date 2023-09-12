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
    public class MedicalController : ControllerBase
    {
        private readonly IMedicalService medicalService;

        public MedicalController(IMedicalService service)
        {
            this.medicalService = service;
           
        }

        [AllowAnonymous]
        [HttpGet("GetAllMedical")]
        public async Task<IActionResult> GetAllMedical()
        {
            var data = await this.medicalService.GetAllMedical();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetMedicalById")]
        public async Task<IActionResult> GetMedicalById(int medical_id)
        {
            var data = await this.medicalService.GetMedicalById(medical_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPost("CreateMedical")]
        public async Task<IActionResult> CreateMedical(MedicalModal _data)
        {
            var data = await this.medicalService.CreateMedical(_data);
            return Ok(data);
        }

        [HttpPut("UpdateMedical")]
        public async Task<IActionResult> UpdateMedical(MedicalModal _data, int medical_id)
        {
            var data = await this.medicalService.UpdateMedical(_data, medical_id);
            return Ok(data);
        }

        [HttpDelete("RemoveMedical")]
        public async Task<IActionResult> RemoveMedical(int medical_id)
        {
            var data = await this.medicalService.RemoveMedical(medical_id);
            return Ok(data);
        }

    }
}
