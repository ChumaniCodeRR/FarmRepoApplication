using Application_test_repo.Helper;
using Application_test_repo.Modal;

namespace Application_test_repo.Services
{
    public interface IAgriExpectAdviser
    {
        Task<List<AgriExpectAdviserModal>> GetAllAgriExpect();
        Task<AgriExpectAdviserModal> GetAgriExpectByCode(int expert_id);
        Task<APIResponse> RemoveAgriExpect(int expert_id);
        Task<APIResponse> CreateAgriExpect(AgriExpectAdviserModal data);
        Task<APIResponse> UpdateAgriExpect(AgriExpectAdviserModal data, int expert_id);
    }
}
