using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database
{
    internal class Queries
    {
        private readonly ExpensesContext expensesContext;
        public Queries(ExpensesContext context) { }

        public void GetTagsSummary()
        {
            var results = expensesContext.Database.SqlQuery<int>($"SELECT COUNT(*) FROM Books");
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



