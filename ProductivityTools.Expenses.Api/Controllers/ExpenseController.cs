using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Api.Requests;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ExpenseBaseController
    {

        public ExpenseController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext):base(logger,expensesContext)
        {
        }

        [HttpGet]
        [Route("Echo")]
        public string Echo(string name)
        {
            return $"Welcome request performed at {DateTime.Now} with param {name} on server {System.Environment.MachineName} to Application Expenses";
        }

        [HttpPost]
        [Route("List")]
        [Authorize]
        public List<Expense> List(ListRequest listRequest)
        {

            var r = ExpensesContext.Expenses
                .Include(x => x.Bag).Include(x => x.Category);
            if (listRequest.BagId.HasValue)
            {
                r = r.Where(x => x.BagId == listRequest.BagId.Value).Include(x => x.Bag).Include(x => x.Category);
            }
            if (listRequest.CategoryId.HasValue)
            {
                r = r.Where(x => x.CategoryId == listRequest.CategoryId.Value).Include(x => x.Bag).Include(x => x.Category);
            }
            var result = r.ToList();
            return result;
        }

        [HttpGet]
        [Route("BagList")]
        public List<Bag> BagList()
        {
            var r = ExpensesContext.Bag.ToList();
            return r;
        }

       

        public class s {
            public string Name { get; set; }
    }

        [HttpPost]
        [Route("Save")]
        public StatusCodeResult Save(Expense expense)
        {
            ExpensesContext.Add(expense);
            ExpensesContext.SaveChanges();
            return Ok();
        }
    }
}