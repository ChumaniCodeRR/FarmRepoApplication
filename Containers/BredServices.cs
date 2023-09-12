using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;


namespace Application_test_repo.Containers
{
    public class BredServices : IBredService
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<BredServices> _logger;

        public BredServices(Test_DBContext context, IMapper mapper, ILogger<BredServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> CreateBred(BredModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblBred _bred = this.mapper.Map<BredModal, TblBred>(data);
                await this._dbContext.TblBreds.AddAsync(_bred);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.BredCode;
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<BredModal>> GetAllBreds()
        {
            List<BredModal> _response = new List<BredModal>();

            var _data = await this._dbContext.TblBreds.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblBred>, List<BredModal>>(_data);
            }
            return _response;

        }

        public async Task<BredModal> GetBredByCode(string bred_code)
        {
            BredModal _response = new BredModal();

            var _data = await this._dbContext.TblBreds.FindAsync(bred_code);

            if (_data != null)
            {
                _response = this.mapper.Map<TblBred, BredModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveBred(string bred_code)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _bred = await this._dbContext.TblBreds.FindAsync(bred_code);
                if (_bred != null)
                {
                    this._dbContext.TblBreds.Remove(_bred);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = bred_code.ToString();
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

        public async Task<APIResponse> UpdateBred(BredModal data, string bred_code)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _bred = await this._dbContext.TblBreds.FindAsync(bred_code);
                if (_bred != null)
                {
                   
                    _bred.Species = data.Species;
                    _bred.Tags = data.Tags;
                    _bred.BredDate = data.BredDate;

                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = bred_code.ToString();
                    
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

