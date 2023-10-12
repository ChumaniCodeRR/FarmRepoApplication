using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class FertilizerServices : IFertilizer
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<FertilizerServices> _logger;

        public FertilizerServices(Test_DBContext context, IMapper mapper, ILogger<FertilizerServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> CreateFertilizer(FertilizerModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblFertilizer _fertilizer = this.mapper.Map<FertilizerModal, TblFertilizer>(data);
                await this._dbContext.TblFertilizers.AddAsync(_fertilizer);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.FertilizerId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<FertilizerModal>> GetAllFertilizers()
        {
            List<FertilizerModal> _response = new List<FertilizerModal>();

            var _data = await this._dbContext.TblFertilizers.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblFertilizer>, List<FertilizerModal>>(_data);
            }
            return _response;

        }

        public async Task<FertilizerModal> GetFertilizerById(int fertilizer_id)
        {
            FertilizerModal _response = new FertilizerModal();

            var _data = await this._dbContext.TblFertilizers.FindAsync(fertilizer_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblFertilizer, FertilizerModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveFertilizer(int fertilizer_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _fertilizer = await this._dbContext.TblFertilizers.FindAsync(fertilizer_id);
                if (_fertilizer != null)
                {
                    this._dbContext.TblFertilizers.Remove(_fertilizer);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = fertilizer_id.ToString();
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

        public async Task<APIResponse> UpdateFertilizer(FertilizerModal data, int fertilizer_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _fertilizer = await this._dbContext.TblFertilizers.FindAsync(fertilizer_id);
                if (_fertilizer != null)
                {

                    _fertilizer.Fname = data.Fname;
                    _fertilizer.Frate = data.Frate;
                    _fertilizer.Quantity = data.Quantity;
                    _fertilizer.CropId = data.CropId;

                    response.Result = fertilizer_id.ToString();

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

