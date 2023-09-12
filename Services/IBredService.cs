using Application_test_repo.Repos;
using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface IBredService
    {
        Task<List<BredModal>> GetAllBreds();
        Task<BredModal> GetBredByCode(string bred_code);
        Task<APIResponse> RemoveBred(string bred_code);
        Task<APIResponse> CreateBred(BredModal data);
        Task<APIResponse> UpdateBred(BredModal data, string bred_code);
    }
}
