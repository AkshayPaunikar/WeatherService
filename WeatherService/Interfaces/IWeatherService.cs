using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Models;

namespace WeatherService.Interfaces
{
    public interface IWeatherService
    {
        public Task<WeatherData> GetWeatherForCityAsync(int cityId);
    }
}
