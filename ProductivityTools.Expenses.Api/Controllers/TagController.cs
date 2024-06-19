using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Api.Requests;
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
            var expenseTags = this.ExpensesContext.ExpenseTag.Include(x => x.Tag).Where(x => expensesId.Contains(x.ExpenseId)).ToList();
            // var tags = expenseTags.Select(x => x.Tag);
            return expenseTags;
        }



       
        [HttpGet]
        [Route("GetAllTags")]
        public IEnumerable<Tag> GetAllTags()
        {
            var tags = this.ExpensesContext.Tags.ToList();
            // var tags = expenseTags.Select(x => x.Tag);
            return tags;
        }

        [HttpPost]
        [Route("GetTagsByTagGroupId")]
        public IEnumerable<Tag> GetTagsByTagGroupId(int tagGroupId)
        {
            var tags = this.ExpensesContext.Tags.Where(x => x.TagGroupId == tagGroupId).ToList();
            return tags;
        }

        [HttpGet]
        [Route("GetCategoriesForTagGroup")]
        public IEnumerable<Category> GetCategoriesForTagGroup(int tagGroupId)
        {
            var result = this.Queries.GetCategoriesForTagGroup(tagGroupId);
            return result;
        }
        /*
         *   select * from me.tag t
  inner join me.TagGroup tg on t.TagGroupID=tg.TagGroupID
  inner join me.tagGroupCategory tgc on tg.TagGroupId=tgc.tagGroupid
  inner join me.Category c on tgc.CategoryID=c.CategoryID
  where tg.name	='Estymacje'
        */

        [HttpGet]
        [Route("GetTagsSummary")]
        public IEnumerable<TagGroupSummary> GetTagsSummary(int tagId)
        {
            var r = Queries.GetTagsSummary(tagId);
            // var tags = expenseTags.Select(x => x.Tag);
            return r;
        }

        [HttpGet]
        [Route("GetAllTagGroups")]
        public List<TagGroup> GetTagGroup()
        {
            var r = this.ExpensesContext.TagGroups.ToList();
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
            var expenseTag = this.ExpensesContext.ExpenseTag.Single(x => x.ExpenseTagId == expenseTagId);
            this.ExpensesContext.Remove(expenseTag);
            this.ExpensesContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("GetTagGroupForCategory")]
        public List<TagGroup> GetTagGroupForCategory(int categoryId)
        {
            var tagGroups = this.ExpensesContext.TagGroupCategory.Where(x => x.CategoryId == categoryId).Select(x => x.TagGroup).ToList();
            return tagGroups;
        }

        [HttpPost]
        [Route("addTagGroupToCategory")]
        public bool AddTagGroupForCategory(int tagGroupId, int categoryId)
        {
            TagGroupCategory tgc=new TagGroupCategory();
            tgc.CategoryId = categoryId;
            tgc.TagGroupId = tagGroupId;
            this.ExpensesContext.TagGroupCategory.Add(tgc);
            this.ExpensesContext.SaveChanges();
            return true;
        }

        [HttpPost]
        [Route("SaveTags")]
        public IActionResult SaveTags(SaveTagsRequest saveTagsRequest)
        {
            var expenseTagsInDb = this.ExpensesContext.ExpenseTag.Where(x => x.ExpenseId == saveTagsRequest.Expense.ExpenseId).ToList();

            foreach (var expenseTag in saveTagsRequest.ExpenseTags)
            {
                if (expenseTag.ExpenseTagId != null)
                {
                    expenseTagsInDb.Remove(expenseTagsInDb.First(x => x.ExpenseTagId == expenseTag.ExpenseTagId));
                }
                else
                {
                    this.ExpensesContext.Add(expenseTag);
                }
            }
            foreach(var expenseTagInDb in expenseTagsInDb)
            {
                this.ExpensesContext.Remove(expenseTagInDb);
            }
            ExpensesContext.SaveChanges();
            return Ok();
        }

        /*
  select * from me.tag t
  inner join me.TagGroup tg on t.TagGroupID=tg.TagGroupID
  inner join me.tagGroupCategory tgc on tg.TagGroupId=tgc.tagGroupid
  inner join me.Category c on tgc.CategoryID=c.CategoryID
        */
    }
}
