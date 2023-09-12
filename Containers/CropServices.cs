using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class CropServices : ICrop
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<CropServices> _logger;

        public CropServices(Test_DBContext context, IMapper mapper, ILogger<CropServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> UpdateCrop(CropModal data, string crop_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _crop = await this._dbContext.TblCrops.FindAsync(crop_id);
                if (_crop != null)
                {

                    _crop.CropName = data.CropName;
                    _crop.Quantity = data.Quantity;
                   
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = crop_id.ToString();

                }
                else
                {
                    response.ResponseCode = 404;
                    response.ErrorMessage = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> CreateCrop(CropModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblCrop _crop = this.mapper.Map<CropModal, TblCrop>(data);
                await this._dbContext.TblCrops.AddAsync(_crop);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.CropId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<CropModal>> GetAllCrops()
        {
            List<CropModal> _response = new List<CropModal>();

            var _data = await this._dbContext.TblCrops.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblCrop>, List<CropModal>>(_data);
            }
            return _response;

        }

        public async Task<CropModal> GetCropById(string crop_id)
        {
            CropModal _response = new CropModal();

            var _data = await this._dbContext.TblCrops.FindAsync(crop_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblCrop, CropModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveCrop(string crop_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _crop = await this._dbContext.TblCrops.FindAsync(crop_id);
                if (_crop != null)
                {
                    this._dbContext.TblCrops.Remove(_crop);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = crop_id.ToString();
                }
                else
                {
                    response.ResponseCode = 404;
                    response.ErrorMessage = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
