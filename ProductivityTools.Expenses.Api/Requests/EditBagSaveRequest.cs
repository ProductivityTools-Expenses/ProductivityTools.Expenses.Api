using ProductivityTools.Expenses.Database.Objects;

namespace ProductivityTools.Expenses.Api.Requests
{
    public class EditBagSaveRequest
    {
        public Bag Bag { get; set; }
        public List<EditBagSaveRequestCategories> Categories { get; set; }  
    }
}
