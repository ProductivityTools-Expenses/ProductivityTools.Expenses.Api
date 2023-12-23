using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class Bag
    {
        public int BagId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public ICollection<Expense>? Expenses { get; set; }


        //[JsonIgnore]
        //public List<Category> Categories { get; set; }

        [JsonIgnore]
        public List<BagCategory> BagCategories { get; set; } = new();
    }
}
