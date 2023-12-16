using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Expenses.Database;
using System.Security.Cryptography;

namespace ProductivityTools.Expenses.Api.Controllers
{
    public abstract class ExpenseBaseController : ControllerBase
    {
        protected readonly ILogger<WeatherForecastController> _logger;
        protected readonly ExpensesContext ExpensesContext;

        public ExpenseBaseController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext)
        {
            _logger = logger;
            this.ExpensesContext = expensesContext;
        }
    }
}
