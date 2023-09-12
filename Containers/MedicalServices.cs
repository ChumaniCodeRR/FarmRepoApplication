using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class MedicalServices : IMedicalService
    {
        //testt
        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<MedicalServices> _logger;
        public MedicalServices(Test_DBContext context, IMapper mapper, ILogger<MedicalServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;

        }

        public async Task<APIResponse> CreateMedical(MedicalModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblMedical _medical = this.mapper.Map<MedicalModal, TblMedical>(data);
                await this._dbContext.TblMedicals.AddAsync(_medical);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.MedicalId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<MedicalModal>> GetAllMedical()
        {
            List<MedicalModal> _response = new List<MedicalModal>();

            var _data = await this._dbContext.TblMedicals.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblMedical>, List<MedicalModal>>(_data);
            }
            return _response;

        }

        public async Task<MedicalModal> GetMedicalById(int medical_id)
        {
            MedicalModal _response = new MedicalModal();

            var _data = await this._dbContext.TblMedicals.FindAsync(medical_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblMedical, MedicalModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveMedical(int medical_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _medical = await this._dbContext.TblMedicals.FindAsync(medical_id);
                if (_medical != null)
                {
                    this._dbContext.TblMedicals.Remove(_medical);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = medical_id.ToString();
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

        public async Task<APIResponse> UpdateMedical(MedicalModal data, int medical_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _medical = await this._dbContext.TblMedicals.FindAsync(medical_id);
                if (_medical != null)
                {
                    _medical.ProblemDescrip = data.ProblemDescrip;
                    _medical.SolutionDescrip = data.SolutionDescrip;
                    _medical.Date = data.Date;
                    _medical.DosageNeeded = data.DosageNeeded;
                   
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = medical_id.ToString();
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
