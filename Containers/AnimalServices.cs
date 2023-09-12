using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class AnimalServices : IAnimalService
    {
        /*
        Task<List<AnimalModal>> GetAllFarmAnimals();
        Task<AnimalModal> GetFarmAnimalById(int animal_id);
        Task<APIResponse> RemoveFarmAnimal(int animal_id);
        Task<APIResponse> CreateFarmAnimal(AnimalModal data);
        Task<APIResponse> UpdateFarmAnimal(AnimalModal data, int animal_id);
        */

        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<AnimalServices> _logger;

        public AnimalServices(Test_DBContext context, IMapper mapper, ILogger<AnimalServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> UpdateFarmAnimal(AnimalModal data, int animal_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _animal = await this._dbContext.TblAnimals.FindAsync(animal_id);
                if (_animal != null)
                {

                    _animal.Name = data.Name;
                    _animal.Gender = data.Gender;
                    _animal.TagNumber = data.TagNumber;
                    _animal.Weight = data.Weight;
                    _animal.Price = data.Price;
                    _animal.ProducedBorn = data.ProducedBorn;
                    _animal.Status = data.Status;
                    _animal.MedicalId = data.MedicalId;
                    _animal.BredCode = data.BredCode;

                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = animal_id.ToString();

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

        public async Task<APIResponse> CreateFarmAnimal(AnimalModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblAnimal _animal = this.mapper.Map<AnimalModal, TblAnimal>(data);
                await this._dbContext.TblAnimals.AddAsync(_animal);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.AnimalId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<AnimalModal>> GetAllFarmAnimals()
        {
            List<AnimalModal> _response = new List<AnimalModal>();

            var _data = await this._dbContext.TblAnimals.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblAnimal>, List<AnimalModal>>(_data);
            }
            return _response;

        }

        public async Task<AnimalModal> GetFarmAnimalById(int animal_id)
        {
            AnimalModal _response = new AnimalModal();

            var _data = await this._dbContext.TblAnimals.FindAsync(animal_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblAnimal, AnimalModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveFarmAnimal(int animal_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _animal = await this._dbContext.TblAnimals.FindAsync(animal_id);
                if (_animal != null)
                {
                    this._dbContext.TblAnimals.Remove(_animal);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = animal_id.ToString();
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
