# Weather Service Application

## Project Overview
This is a C# .NET weather service application that retrieves daily weather information for multiple cities using the OpenWeather API.

## Prerequisites
- .NET 8.0 or later SDK
- OpenWeather API Key (sign up at https://openweathermap.org/)

## Configuration
1. Create a `appsettings.json` file in the project root with the following structure:
```json
{
  "OpenWeatherApi": {
    "Key": "YOUR_API_KEY_HERE",
    "BaseURL": "https://api.openweathermap.org/data/2.5/weather"
  },
  "FilePaths": {
    "InputFilePath": "input/cities.txt",
    "OutputDirectory": "output",
    "Logs": "logs/weather_service.log"
  }
}
```

2. Replace `YOUR_API_KEY_HERE` with your actual OpenWeather API key


## Running the Application
1. Clone the repository
2. Create `appsettings.json` with your API key
3. Prepare `input/cities.txt` with city IDs
4. Restore NuGet packages: `dotnet restore`
5. Run the application: `dotnet run`

## Running Tests
Execute: `dotnet test`

## Detailed Configuration Options
- `OpenWeatherApiKey`: Your OpenWeather API authentication key
- `InputFilePath`: Path to the file containing city IDs
- `OutputDirectory`: Directory where weather data files will be saved