using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Interfaces
{
    public interface ICityReader
    {
        public Task<Dictionary<int, string>> ReadCitiesAsync(string filePath);
    }
}
