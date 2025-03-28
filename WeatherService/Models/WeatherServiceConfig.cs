using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Models
{
    public class WeatherServiceConfig
    {
        public string OpenWeatherApiKey { get; set; }
        public string InputFilePath { get; set; }
        public string OutputDirectory { get; set; }
        public string BaseURL { get; set; }
    }
}
