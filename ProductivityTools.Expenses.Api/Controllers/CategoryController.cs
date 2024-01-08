using Microsoft.AspNetCore.Mvc;
using ProductivityTools.Expenses.Database.Objects;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Api.Requests;

namespace ProductivityTools.Expenses.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ExpenseBaseController
    {

        public CategoryController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext) : base(logger, expensesContext)
        {
        }

        [HttpPost]
        [Route("CagetoryList")]
        public List<Category> CategoryList(CategoryListRequest request)
        {
            var r = ExpensesContext.Categories.AsQueryable();
            if (request.BagId.HasValue)
            {
               r=r.Where(x=>x.BagCategories.Any(bc=>bc.BagId==request.BagId.Value));
            }
            
            return r.ToList();
        }

        [HttpGet]
        [Route("CategoryListAll")]
        public List<Category> CategoryListAll()
        {
            var r = ExpensesContext.Categories.AsQueryable();
            

            return r.ToList();
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
