using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class TagGroupCategory
    {
        public int TagGroupCategoryId { get; set; }
        public int TagGroupId { get; set; }
        public int CategoryId { get; set; }
        public TagGroup TagGroup { get; set; }
    }
}
