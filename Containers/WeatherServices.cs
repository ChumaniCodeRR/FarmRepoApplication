using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Repos;
using Application_test_repo.Modal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application_test_repo.Helper;

namespace Application_test_repo.Containers
{
    public class WeatherServices : IWeather
    {

        private readonly Test_DBContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<WeatherServices> _logger;

        public WeatherServices(Test_DBContext context, IMapper mapper, ILogger<WeatherServices> logger)
        {
            this._dbContext = context;
            this.mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> CreateWeatherForcast(WeatherModal data)
        {
            APIResponse response = new APIResponse();

            try
            {
                this._logger.LogInformation("Create Begins");
                TblWeather _weather = this.mapper.Map<WeatherModal, TblWeather>(data);
                await this._dbContext.TblWeathers.AddAsync(_weather);
                await this._dbContext.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Result = data.WeatherId.ToString();
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.ErrorMessage = ex.Message;
                this._logger.LogError(ex.Message, ex);

            }
            return response;
        }

        public async Task<List<WeatherModal>> GetAllWeatherCondition()
        {
            List<WeatherModal> _response = new List<WeatherModal>();

            var _data = await this._dbContext.TblWeathers.ToListAsync();

            if (_data != null)
            {
                _response = this.mapper.Map<List<TblWeather>, List<WeatherModal>>(_data);
            }
            return _response;

        }

        public async Task<WeatherModal> GetWeatherByIdLocation(int weather_id)
        {
            WeatherModal _response = new WeatherModal();

            var _data = await this._dbContext.TblWeathers.FindAsync(weather_id);

            if (_data != null)
            {
                _response = this.mapper.Map<TblWeather, WeatherModal>(_data);
            }
            return _response;
        }

        public async Task<APIResponse> RemoveWeatherForcast(int weather_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _weather = await this._dbContext.TblWeathers.FindAsync(weather_id);
                if (_weather != null)
                {
                    this._dbContext.TblWeathers.Remove(_weather);
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = weather_id.ToString();
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

        public async Task<APIResponse> UpdateWeatherForcast(WeatherModal data, int weather_id)
        {
            APIResponse response = new APIResponse();

            try
            {
                var _weather = await this._dbContext.TblWeathers.FindAsync(weather_id);
                if (_weather != null)
                {
                    _weather.Location = data.Location;
                    _weather.Temperature = data.Temperature;
                    _weather.Humidity = data.Humidity;
                    
                    await this._dbContext.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Result = weather_id.ToString();

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
