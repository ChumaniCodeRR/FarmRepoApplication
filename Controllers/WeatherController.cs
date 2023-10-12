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

    public class WeatherController : ControllerBase
    {
        private readonly IWeather weatherService;

        public WeatherController(IWeather service)
        {
            this.weatherService = service;
        }

        [AllowAnonymous]
        [HttpGet("GetAllWeatherCondition")]
        public async Task<IActionResult> GetAllWeatherCondition()
        {
            var data = await this.weatherService.GetAllWeatherCondition();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetWeatherByIdLocation")]
        public async Task<IActionResult> GetWeatherByIdLocation(int weather_id)
        {
            var data = await this.weatherService.GetWeatherByIdLocation(weather_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("CreateWeatherForcast")]
        public async Task<IActionResult> CreateWeatherForcast(WeatherModal _data)
        {
            var data = await this.weatherService.CreateWeatherForcast(_data);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPut("UpdateWeatherForcast")]
        public async Task<IActionResult> UpdateWeatherForcast(WeatherModal _data, int weather_id)
        {
            var data = await this.weatherService.UpdateWeatherForcast(_data, weather_id);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpDelete("RemoveWeatherForcast")]
        public async Task<IActionResult> RemoveWeatherForcast(int weather_id)
        {
            var data = await this.weatherService.RemoveWeatherForcast(weather_id);
            return Ok(data);
        }
    }
}
