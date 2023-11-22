using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ExpensesContext ExpensesContext;

        public ExpenseController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext)
        {
            _logger = logger;
            this.ExpensesContext = expensesContext;
        }

        [HttpGet]
        [Route("Echo")]
        public string Echo(string name)
        {
            return $"Welcome request performed at {DateTime.Now} with param {name} on server {System.Environment.MachineName} to Application Expenses";
        }

        [HttpGet]
        [Route("List")]
        public List<Expense> List()
        {
            var r = ExpensesContext.Expenses
                .Include(x=>x.Bag)
                .ToList();
            return r;
        }

        [HttpGet]
        [Route("BagList")]
        public List<Bag> BagList()
        {
            var r = ExpensesContext.Bag.ToList();
            return r;
        }
    }
}