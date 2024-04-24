using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class Category
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Expense>? Expenses { get; set; }

        public int BagId { get; set; }
        [JsonIgnore]
        public Bag? Bag { get; set; }

        //[JsonIgnore]
        //public List<BagCategory> BagCategories { get; set; } = new();
    }
}
