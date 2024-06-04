using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database
{
    public interface IQueries
    {
        IEnumerable<TagGroupSummary> GetTagsSummary(int tagId);
        IEnumerable<Category> GetCategoriesForTagGroup(int tagId);
    }
    public class Queries : IQueries
    {
        private readonly ExpensesContext expensesContext;
        public Queries(ExpensesContext context)
        {
            this.expensesContext = context;

        }

        public IEnumerable<TagGroupSummary> GetTagsSummary(int tagId)
        {
            var results = expensesContext.Database.SqlQuery<TagGroupSummary>($@"with query as
                (
                select t.TagId, t.Name as TagName, Value,cost from [me].Expense e
                    inner join me.ExpenseTag et on e.ExpenseId=et.ExpenseId
                    inner join me.Tag t on et.TagID=t.TagID
                    where t.TagGroupID in (
                        select TagGroupID from me.Tag where TagID={tagId})
                )
                select TagId, TagName,Sum(Value) as ValueSum, sum (cost) as CostSum
            from query group by TagId, TagName");
            var x = results.ToList();
            return x;
        }

        public List<Category> GetCategoriesForTagGroup(int tagId)
        {
            var results = expensesContext.Database.SqlQuery<Category>($@"
  select distinct c.Name,c.CategoryID,c.BagId from me.tag t
  inner join me.TagGroup tg on t.TagGroupID=tg.TagGroupID
  inner join me.tagGroupCategory tgc on tg.TagGroupId=tgc.tagGroupid
  inner join me.Category c on tgc.CategoryID=c.CategoryID
  where tg.TagGroupID=${tagId}
");
            var x = results.ToList();
            return x;
        }
    }

}


//with query as
//(
//  select t.Name as TagName, Value from [me].Expense e
//inner join me.ExpenseTag et on e.ExpenseId=et.ExpenseId
//inner join me.Tag t on et.TagID=t.TagID
//where t.TagGroupID in (
//   select TagGroupID from me.Tag where TagID=1)
//)
//select TagName,Sum(Value)
//from query group by TagName



//Doble category
//with ex1 as 
// (select e.ExpenseID, t.TagId, t.Name as TagName, Value, cost from [me].Expense e
//        inner join me.ExpenseTag et on e.ExpenseId=et.ExpenseId
//        inner join me.Tag t on et.TagID=t.TagID
//        where t.TagGroupID in (
//            select TagGroupID from me.Tag where TagID=20)
//			)
//			select  ExpenseID,sum(1) from ex1
//			group by ExpenseID
//			order by sum(1) desc


//			 with ex1 as 
// (select e.ExpenseID, t.TagId, t.Name as TagName, Value, cost from [me].Expense e
//        inner join me.ExpenseTag et on e.ExpenseId=et.ExpenseId
//        inner join me.Tag t on et.TagID=t.TagID
//        where t.TagGroupID in (
//            select TagGroupID from me.Tag where TagID=20)
//			)
//			select  * from ex1
//		where ExpenseID=732

