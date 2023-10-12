using Application_test_repo.Repos;
using Application_test_repo.Services;
using Application_test_repo.Repos.Models;
using Application_test_repo.Modal;
using Application_test_repo.Helper;

namespace Application_test_repo.Services
{
    public interface IWeather
    {
        Task<List<WeatherModal>> GetAllWeatherCondition();
        Task<WeatherModal> GetWeatherByIdLocation(int weather_id);
        Task<APIResponse> RemoveWeatherForcast(int weather_id);
        Task<APIResponse> CreateWeatherForcast(WeatherModal data);
        Task<APIResponse> UpdateWeatherForcast(WeatherModal data, int weather_id);
    
    }
}
