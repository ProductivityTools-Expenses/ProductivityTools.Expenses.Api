using Microsoft.AspNetCore.Mvc;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    { 
        private readonly ILogger<WeatherForecastController> _logger;

        public ExpenseController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("echo")]
        public string Echo(string name)
        {
            return string.Format($"Echo returned:{name} and date: {DateTime.Now}");
        }
    }
}