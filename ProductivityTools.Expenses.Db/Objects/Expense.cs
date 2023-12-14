using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class Expense
    {
        public int? ExpenseId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public decimal? ExpectedValue { get; set; }

        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public decimal? Value { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? Additions { get; set; }
        public decimal? Cost { get; set; }
        public string? Comment { get; set; }


        public int BagId { get; set; }  
        public Bag? Bag { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
