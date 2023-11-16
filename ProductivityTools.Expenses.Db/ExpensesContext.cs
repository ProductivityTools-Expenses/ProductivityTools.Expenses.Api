using Microsoft.EntityFrameworkCore;
using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Database
{
    public class ExpensesContext
    {
        public DbSet<Expense> Expenses { get; set; }

    }
}