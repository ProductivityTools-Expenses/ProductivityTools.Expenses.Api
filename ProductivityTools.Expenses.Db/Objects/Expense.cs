using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }
    }
}
