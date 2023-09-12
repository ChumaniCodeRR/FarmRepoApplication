using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface ISeedService
    {
        Task<List<SeedModal>> GetAllSeeds();
        Task<SeedModal> GetBredBySeedID(int seed_id);
        Task<APIResponse> RemoveSeed(int seed_id);
        Task<APIResponse> CreateSeed(SeedModal data);
        Task<APIResponse> UpdateSeed(SeedModal data, int seed_id);
    }
}
