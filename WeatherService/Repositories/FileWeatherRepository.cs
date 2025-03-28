using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherService.Interfaces;
using WeatherService.Models;

namespace WeatherService.Repositories
{
    class FileWeatherRepository : IWeatherRepository
    {
        private readonly WeatherServiceConfig _config;
        

        public FileWeatherRepository(WeatherServiceConfig config)           
        {
            _config = config;            
        }

        public async Task SaveWeatherDataAsync(WeatherData weatherData)
        {
            try
            {
                Directory.CreateDirectory(_config.OutputDirectory);

                var fileName = $"{weatherData.CityId}_{weatherData.Date:yyyyMMdd}.json";
                var filePath = Path.Combine(_config.OutputDirectory, fileName);

                var json = JsonSerializer.Serialize(weatherData,
                    new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(filePath, json);
                Log.Information($"Saved weather data for {weatherData.CityName}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error saving weather data: {ex.Message}");
                throw;
            }
        }
    }
}
