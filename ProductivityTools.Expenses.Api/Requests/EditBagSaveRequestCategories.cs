namespace ProductivityTools.Expenses.Api.Requests
{
    public class EditBagSaveRequestCategories
    {
        public int CategoryId { get; set; }

        public int? BagId { get; set; }  
        public int? BagCategoryId { get; set; }
    }
}
