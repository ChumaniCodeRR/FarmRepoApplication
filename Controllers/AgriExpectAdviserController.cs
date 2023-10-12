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

    public class AgriExpectAdviserController : ControllerBase
    {
        private readonly IAgriExpectAdviser AgriExpectAdviser;

        public AgriExpectAdviserController(IAgriExpectAdviser service)
        {
            this.AgriExpectAdviser = service;
        }

        [AllowAnonymous]
        [HttpGet("GetAllAgriExpect")]
        public async Task<IActionResult> GetAllAgriExpect()
        {
            var data = await this.AgriExpectAdviser.GetAllAgriExpect();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetAgriExpectByCode")]
        public async Task<IActionResult> GetAgriExpectByCode(int expert_id)
        {
            var data = await this.AgriExpectAdviser.GetAgriExpectByCode(expert_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("CreateAgriExpect")]
        public async Task<IActionResult> CreateAgriExpect(AgriExpectAdviserModal _data)
        {
            var data = await this.AgriExpectAdviser.CreateAgriExpect(_data);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPut("UpdateAgriExpect")]
        public async Task<IActionResult> UpdateAgriExpect(AgriExpectAdviserModal _data, int expert_id)
        {
            var data = await this.AgriExpectAdviser.UpdateAgriExpect(_data, expert_id);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpDelete("RemoveAgriExpect")]
        public async Task<IActionResult> RemoveAgriExpect(int expert_id)
        {
            var data = await this.AgriExpectAdviser.RemoveAgriExpect(expert_id);
            return Ok(data);
        }

    }
}
