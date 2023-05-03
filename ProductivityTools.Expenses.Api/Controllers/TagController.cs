using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ExpenseBaseController
    {
        public TagController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext) : base(logger, expensesContext)
        {
        }


        [HttpPost]
        [Route("GetTags")]
        public IEnumerable<ExpenseTag> GetTags(List<int> expensesId)
        {
            var expenseTags = this.ExpensesContext.ExpenseTag.Include(x=>x.Tag).Where(x => expensesId.Contains(x.ExpenseId)).ToList();
           // var tags = expenseTags.Select(x => x.Tag);
            return expenseTags;
        }
    }
}
