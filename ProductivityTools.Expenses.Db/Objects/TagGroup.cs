using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class TagGroup
    {
        public int TagGroupId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Tag> Tags { get; set; }

        [JsonIgnore]
        public ICollection<TagGroupCategory> TagGroupCategories { get; set; }

    }
}
