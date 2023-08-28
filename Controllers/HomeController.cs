using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherMVCAPP.Services;

namespace WeatherMVCAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(Weather weatherInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", weatherInfo);
            }

            var apiKey = _configuration["OpenWeatherMap:ApiKey"];
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={weatherInfo.City}&appid={apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var weatherData = await JsonSerializer.DeserializeAsync<WeatherApiResponse>(contentStream);


                weatherInfo.Temperature = $"{weatherData.Main.Temperature}°C";
                weatherInfo.Description = weatherData.Weather[0].Description;

                return View("Index", weatherInfo);
            }
            else
            {
                ModelState.AddModelError("", "Error fetching weather data.");
                return View("Index", weatherInfo);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return null;
        }

     
    }
}