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
        public List<Tag> GetTags(List<int> expensesId)
        {
            var tags = this.ExpensesContext.ExpenseTag.Include(x=>x.Tag).Where(x => expensesId.Contains(x.ExpenseId)).ToList(); ;
            return new List<Tag>();
        }
    }
}
