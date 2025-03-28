using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WeatherService.Interfaces;
using WeatherService.Models;
using WeatherService.Repositories;
using WeatherService.Services;

namespace WeatherService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Set the base path
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load JSON file
            .Build();


            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(config["FilePaths:Logs"], rollingInterval: RollingInterval.Day)
            .CreateLogger();

            // Dependency Injection Setup
            var services = new ServiceCollection()
                .AddSingleton(new WeatherServiceConfig
                {
                    OpenWeatherApiKey = config["OpenWeatherApi:Key"],
                    InputFilePath = config["FilePaths:InputFilePath"],
                    OutputDirectory = config["FilePaths:OutputDirectory"],
                    BaseURL = config["OpenWeatherApi:BaseURL"]
                })
                .AddHttpClient<IWeatherService, OpenWeatherService>()
                .Services
                .AddTransient<ICityReader, CityReader>()
                .AddTransient<IWeatherRepository, FileWeatherRepository>();
            

            var serviceProvider = services.BuildServiceProvider();

            var cityReader = serviceProvider.GetRequiredService<ICityReader>();
            var weatherService = serviceProvider.GetRequiredService<IWeatherService>();
            var weatherRepository = serviceProvider.GetRequiredService<IWeatherRepository>();
            

            try
            {
                var cities = await cityReader.ReadCitiesAsync("input/cities.txt");

                foreach (var city in cities)
                {
                    var weatherData = await weatherService.GetWeatherForCityAsync(city.Key);
                    await weatherRepository.SaveWeatherDataAsync(weatherData);
                }

               
                Log.Information("Weather service completed successfully");
            }
            catch (Exception ex)
            {
                
                Log.Information($"Weather service failed: {ex.Message}");
            }
        }
    }
    
}
