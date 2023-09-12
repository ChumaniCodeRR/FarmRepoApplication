using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class SeedServices : ISeedService
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<SeedServices> _logger;

        public SeedServices(Test_DBContext context, IMapper mapper, ILogger<SeedServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<SeedModal>> GetAllSeeds()
        {
            List<SeedModal> _response = new List<SeedModal>();

            var _data = await this._dbContext.TblSeeds.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblSeed>, List<SeedModal>>(_data);
            }
            return _response;

        }

        public async Task<APIResponse> CreateSeed(SeedModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblSeed _seed = this.mapper.Map<SeedModal, TblSeed>(data);
                await this._dbContext.TblSeeds.AddAsync(_seed);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.SeedId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<SeedModal> GetBredBySeedID(int seed_id)
        {
            SeedModal _response = new SeedModal();

            var _data = await this._dbContext.TblSeeds.FindAsync(seed_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblSeed, SeedModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveSeed(int seed_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _seed = await this._dbContext.TblSeeds.FindAsync(seed_id);
                if (_seed != null)
                {
                    this._dbContext.TblSeeds.Remove(_seed);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = seed_id.ToString();
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

        public async Task<APIResponse> UpdateSeed(SeedModal data, int seed_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _seed = await this._dbContext.TblSeeds.FindAsync(seed_id);
                if (_seed != null)
                {
                    _seed.SeedName = data.SeedName;
                    _seed.Rate = data.Rate;
                    _seed.Category = data.Category;
                    _seed.Type = data.Type;
                   

                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = seed_id.ToString();
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
