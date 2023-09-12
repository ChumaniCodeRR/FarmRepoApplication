using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface ICrop
    {
        Task<List<CropModal>> GetAllCrops();
        Task<CropModal> GetCropById(string crop_id);
        Task<APIResponse> RemoveCrop(string crop_id);
        Task<APIResponse> CreateCrop(CropModal data);
        Task<APIResponse> UpdateCrop(CropModal data, string crop_id);
    }
}
