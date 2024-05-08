using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database
{
    public interface IQueries
    {
        void GetTagsSummary(int tagId);
    }
    public class Queries : IQueries
    {
        private readonly ExpensesContext expensesContext;
        public Queries(ExpensesContext context)
        {
            this.expensesContext = context;

        }

        public void GetTagsSummary(int tagId)
        {
            var results = expensesContext.Database.SqlQuery<int>($"SELECT COUNT(*) FROM Books");
            Console.Write("fdsa)");
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



