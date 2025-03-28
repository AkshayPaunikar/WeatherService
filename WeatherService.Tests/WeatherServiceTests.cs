using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Interfaces;
using WeatherService.Models;

namespace WeatherService.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherForCity_ReturnsValidWeatherData()
        {
            var mockHttp = new Mock<IWeatherService>();
            mockHttp.Setup(x => x.GetWeatherForCityAsync(It.IsAny<int>()))
                .ReturnsAsync(new WeatherData { CityName = "London", Temperature = 15 });

            var weatherData = await mockHttp.Object.GetWeatherForCityAsync(2643741);

            Assert.NotNull(weatherData);

            Assert.Equal("London", weatherData.CityName);
        }
    }

}

