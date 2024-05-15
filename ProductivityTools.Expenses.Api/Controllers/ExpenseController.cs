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
        [Route("Get")]
        [Authorize]
        public Expense Get(ExpenseGetRequest expenseGetRequest)
        {

            var r = ExpensesContext.Expenses.SingleOrDefault(x => x.ExpenseId == expenseGetRequest.ExpenseId);
            var result = r;
            return result;
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
        [Route("GetExpensesByTag")]
        [Authorize]
        public List<Expense> ListByTag(int tagId)
        {

            var expensesIds = ExpensesContext.ExpenseTag.Where(x => x.TagId == tagId).Select(x => x.ExpenseId).ToList();
            var expenses = ExpensesContext.Expenses.Where(x => expensesIds.Contains(x.ExpenseId.Value));

            //    .Include(x => x.Bag).Include(x => x.Category)
            //    .Where(x=>x.tag)
            //if (listRequest.BagId.HasValue)
            //{
            //    r = r.Where(x => x.BagId == listRequest.BagId.Value).Include(x => x.Bag).Include(x => x.Category);
            //}
            //if (listRequest.CategoryId.HasValue)
            //{
            //    r = r.Where(x => x.CategoryId == listRequest.CategoryId.Value).Include(x => x.Bag).Include(x => x.Category);
            //}
            var result = expenses.ToList();
            return result;
        }



        public class s {
            public string Name { get; set; }
    }

        [HttpPost]
        [Route("Save")]
        public StatusCodeResult Save(Expense expense)
        {
            if (expense.ExpenseId.HasValue)
            {
                ExpensesContext.Update(expense);
            }
            else
            {
                ExpensesContext.Add(expense);
            }
            ExpensesContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public StatusCodeResult Delete(int expenseId)
        {
            var expense=ExpensesContext.Expenses.SingleOrDefault(x => x.ExpenseId == expenseId);
            if (expense!=null)
            {
                ExpensesContext.Remove(expense);
                ExpensesContext.SaveChanges();
            }
            
            return Ok();
        }
    }
}