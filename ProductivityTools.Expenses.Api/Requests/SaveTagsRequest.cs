using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Requests
{
    public class SaveTagsRequest
    {
        public Expense Expense { get; set; }
        public List<ExpenseTag> ExpenseTags { get; set; }
    }
}
