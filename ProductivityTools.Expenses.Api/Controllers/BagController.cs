using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BagController : ExpenseBaseController
    {
        public BagController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext) : base(logger, expensesContext)
        {
        }

        [HttpGet]
        [Route("BagList")]
        public List<Bag> BagList()
        {
            var r = ExpensesContext.Bag.ToList();
            return r;
        }

        [HttpPost]
        [Route("Save")]
        public StatusCodeResult Save(Bag bag)
        {

            if (bag.BagId.HasValue)
            {
                ExpensesContext.Update(bag);
            }
            else
            {
                ExpensesContext.Add(bag);
            }
            ExpensesContext.SaveChanges();
            return Ok();
        }
    }
}
