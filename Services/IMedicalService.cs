
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface IMedicalService
    {
        Task<List<MedicalModal>> GetAllMedical();
        Task<MedicalModal> GetMedicalById(int medical_id);
        Task<APIResponse> RemoveMedical(int medical_id);
        Task<APIResponse> CreateMedical(MedicalModal data);
        Task<APIResponse> UpdateMedical(MedicalModal data, int medical_id);
    }
}
