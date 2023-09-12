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
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService animalService;

        public AnimalController(IAnimalService service)
        {
            this.animalService = service;
        }

        [AllowAnonymous]
        [HttpGet("GetAllFarmAnimals")]
        public async Task<IActionResult> GetAllFarmAnimals()
        {
            var data = await this.animalService.GetAllFarmAnimals();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetFarmAnimalById")]
        public async Task<IActionResult> GetFarmAnimalById(int animal_id)
        {
            var data = await this.animalService.GetFarmAnimalById(animal_id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("CreateFarmAnimal")]
        public async Task<IActionResult> CreateFarmAnimal(AnimalModal _data)
        {
            var data = await this.animalService.CreateFarmAnimal(_data);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpPut("UpdateFarmAnimal")]
        public async Task<IActionResult> UpdateFarmAnimal(AnimalModal _data, int animal_id)
        {
            var data = await this.animalService.UpdateFarmAnimal(_data, animal_id);
            return Ok(data);
        }
        [AllowAnonymous]
        [HttpDelete("RemoveFarmAnimal")]
        public async Task<IActionResult> RemoveFarmAnimal(int animal_id)
        {
            var data = await this.animalService.RemoveFarmAnimal(animal_id);
            return Ok(data);
        }
    }
}
