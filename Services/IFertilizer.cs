using Application_test_repo.Repos;
using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface IFertilizer
    {
        Task<List<FertilizerModal>> GetAllFertilizers();
        Task<FertilizerModal> GetFertilizerById(int fertilizer_id);
        Task<APIResponse> RemoveFertilizer(int fertilizer_id);
        Task<APIResponse> CreateFertilizer(FertilizerModal data);
        Task<APIResponse> UpdateFertilizer(FertilizerModal data, int fertilizer_id);
    }
}
