using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Interfaces;
using WeatherService.Models;

namespace WeatherService.Services
{
    class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherServiceConfig _config;
        

        public OpenWeatherService(
            HttpClient httpClient,
            WeatherServiceConfig config
            )
        {
            _httpClient = httpClient;
            _config = config;
            
        }

        public async Task<WeatherData> GetWeatherForCityAsync(int cityId)
        {
            try
            {
                var url = _config.BaseURL + $"?id={cityId}&appid={_config.OpenWeatherApiKey}&units=metric";

                var response = await _httpClient.GetStringAsync(url);
                var json = JObject.Parse(response);

                return new WeatherData
                {
                    CityId = cityId,
                    CityName = json["name"].ToString(),
                    Date = DateTime.UtcNow,
                    Temperature = json["main"]["temp"].Value<double>(),
                    Humidity = json["main"]["humidity"].Value<double>(),
                    Description = json["weather"][0]["description"].ToString()
                };
            }
            catch (Exception ex)
            {
                Log.Error($"Error retrieving weather for city {cityId}: {ex.Message}");
                throw;
            }
        }
    }
}
