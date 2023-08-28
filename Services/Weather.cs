using System.Text.Json.Serialization;

namespace WeatherMVCAPP.Services
{
    public class Weather
    {
        public string? City { get; set; }
        public string? Temperature { get; set; }
        public string? Description { get; set; }
    }

    public class WeatherApiResponse
    {
        [JsonPropertyName("main")]
        public MainWeatherData Main { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherData> Weather { get; set; }
    }

    public class MainWeatherData
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        // Other properties like humidity, pressure, etc.
    }

    public class WeatherData
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
