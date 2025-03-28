using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Interfaces;

namespace WeatherService.Services
{
    public class CityReader : ICityReader
    {

        public async Task<Dictionary<int, string>> ReadCitiesAsync(string filePath)
        {
            var cities = new Dictionary<int, string>();

            try
            {
                var lines = await File.ReadAllLinesAsync(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int cityId))
                    {
                        cities[cityId] = parts[1];
                    }
                }

                Log.Information($"Loaded {cities.Count} cities from {filePath}");
                return cities;
            }
            catch (Exception ex)
            {
                Log.Error($"Error reading cities file: {ex.Message}");
                throw;
            }
        }
    }
}
