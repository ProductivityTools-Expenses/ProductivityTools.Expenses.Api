using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Expenses.Database.Objects;
using ProductivityTools.Expenses.Database;

namespace ProductivityTools.Expenses.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ExpenseBaseController
    {

        public CategoryController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext) : base(logger, expensesContext)
        {
        }

        [HttpGet]
        [Route("CagetoryList")]
        public List<Category> CategoryList()
        {
            var r = ExpensesContext.Categories.ToList();
            return r;
        }

        [HttpPost]
        [Route("CategorySave")]
        public StatusCodeResult CategorySave(Category category)
        {
            var r = ExpensesContext.Categories.Add(category);
            ExpensesContext.SaveChanges();
            return Ok();
        }

    }
}
