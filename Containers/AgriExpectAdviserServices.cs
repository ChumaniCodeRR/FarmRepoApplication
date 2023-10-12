using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;


namespace Application_test_repo.Containers
{
    public class AgriExpectAdviserServices : IAgriExpectAdviser
    {
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<AgriExpectAdviserServices> _logger;

        public AgriExpectAdviserServices(Test_DBContext context, IMapper mapper, ILogger<AgriExpectAdviserServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> CreateAgriExpect(AgriExpectAdviserModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblAgriExpectAdviser _agriexper = this.mapper.Map<AgriExpectAdviserModal, TblAgriExpectAdviser>(data);
                await this._dbContext.TblAgriExpectAdvisers.AddAsync(_agriexper);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.ExpertId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<AgriExpectAdviserModal>> GetAllAgriExpect()
        {
            List<AgriExpectAdviserModal> _response = new List<AgriExpectAdviserModal>();

            var _data = await this._dbContext.TblAgriExpectAdvisers.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblAgriExpectAdviser>, List<AgriExpectAdviserModal>>(_data);
            }
            return _response;

        }

        public async Task<AgriExpectAdviserModal> GetAgriExpectByCode(int expert_id)
        {
            AgriExpectAdviserModal _response = new AgriExpectAdviserModal();

            var _data = await this._dbContext.TblAgriExpectAdvisers.FindAsync(expert_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblAgriExpectAdviser, AgriExpectAdviserModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveAgriExpect(int expert_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _agriexper = await this._dbContext.TblAgriExpectAdvisers.FindAsync(expert_id);
                if (_agriexper != null)
                {
                    this._dbContext.TblAgriExpectAdvisers.Remove(_agriexper);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = expert_id.ToString();
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

        public async Task<APIResponse> UpdateAgriExpect(AgriExpectAdviserModal data, int expert_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _agriexper = await this._dbContext.TblAgriExpectAdvisers.FindAsync(expert_id);
                if (_agriexper != null)
                {
                    _agriexper.ContactDetails = data.ContactDetails;
                    _agriexper.Address = data.Address;
                    
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = expert_id.ToString();

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
