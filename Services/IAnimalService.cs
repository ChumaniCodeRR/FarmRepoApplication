using Application_test_repo.Repos;
using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface IAnimalService
    {
        Task<List<AnimalModal>> GetAllFarmAnimals();
        Task<AnimalModal> GetFarmAnimalById(int animal_id);
        Task<APIResponse> RemoveFarmAnimal(int animal_id);
        Task<APIResponse> CreateFarmAnimal(AnimalModal data);
        Task<APIResponse> UpdateFarmAnimal(AnimalModal data, int animal_id);
    }
}
