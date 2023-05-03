using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<ExpenseTag> ExpenseTags { get; set; }
    }
}
