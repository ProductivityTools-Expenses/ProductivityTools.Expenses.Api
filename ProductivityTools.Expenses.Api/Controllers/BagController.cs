using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Api.Requests;
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

        [HttpGet]
        [Route("Get")]
        public Bag Get(int bagId)
        {
            var r = ExpensesContext.Bag.Include(x => x.Categories).Single(x => x.BagId == bagId);
            return r;
        }

        [HttpPost]
        [Route("Save")]
        public StatusCodeResult Save(EditBagSaveRequest saveBagRequest)
        {
            if (saveBagRequest.Bag.BagId.HasValue)
            {
                
                saveBagRequest.Categories.ForEach(x =>
                {
                    if (x.BagId==null)
                    {
                        var category = ExpensesContext.Categories.Where(y => y.CategoryId == x.CategoryId).Single();
                        category.BagId = saveBagRequest.Bag.BagId;
                        ExpensesContext.Update(category);
                        Console.Write("fdsa");
                    }
                });


                ExpensesContext.Update(saveBagRequest.Bag);

            }
            else
            {
                ExpensesContext.Add(saveBagRequest.Bag);
            }

            ExpensesContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("RemoveCategoryFromBagCategory")]
        public StatusCodeResult RemoveCategoryFromBagCategory(List<int>  removeCategoryFromBagCategoryRequest)
        {

            //var bagCategories = ExpensesContext.BagCategories.Where(x => removeCategoryFromBagCategoryRequest.Contains(x.BagCategoryId.Value));
            //ExpensesContext.BagCategories.RemoveRange(bagCategories);

            ExpensesContext.SaveChanges();
            return Ok();
        }


    }
}
