using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class ExpenseTag
    {
        public int? ExpenseTagId { get; set; }
        public int ExpenseId { get; set; }
        public int TagId { get; set; }

        public Tag? Tag { get; set; }
    }
}
