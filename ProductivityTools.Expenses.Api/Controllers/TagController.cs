using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database;
using ProductivityTools.Expenses.Database.Objects;
using System.Reflection.PortableExecutable;

namespace ProductivityTools.Expenses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ExpenseBaseController
    {
        IQueries Queries { get; set; }
        public TagController(ILogger<WeatherForecastController> logger, ExpensesContext expensesContext, IQueries queries) : base(logger, expensesContext)
        {
            this.Queries = queries; 
        }


        [HttpPost]
        [Route("TagList")]
        public IEnumerable<ExpenseTag> GetTags(List<int> expensesId)
        {
            var expenseTags = this.ExpensesContext.ExpenseTag.Include(x=>x.Tag).Where(x => expensesId.Contains(x.ExpenseId)).ToList();
           // var tags = expenseTags.Select(x => x.Tag);
            return expenseTags;
        }

        [HttpGet]
        [Route("GetTagsSummary")]
        public IEnumerable<TagGroupSummary> GetTagsSummary(int tagId)
        {
            var r=Queries.GetTagsSummary(tagId);
            // var tags = expenseTags.Select(x => x.Tag);
            return r;
        }


        [HttpGet]
        [Route("GetTagGroup")]
        public TagGroup GetTagGroup(int tagId)
        {
            var r = this.ExpensesContext.Tags.Include(x => x.TagGroup).Single(x => x.TagId == tagId);
            // var tags = expenseTags.Select(x => x.Tag);
            return r.TagGroup;
        }

        [HttpPost]
        [Route("RemoveTagFromExpense")]
        public bool RemoveTagFromExpense(int expenseTagId)
        {
            var expenseTag=this.ExpensesContext.ExpenseTag.Single(x => x.ExpenseTagId == expenseTagId);
            this.ExpensesContext.Remove(expenseTag);
            return true;
        }


    }
}
