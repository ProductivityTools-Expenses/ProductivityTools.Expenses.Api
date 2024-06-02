using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class TagGroupSummary
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public decimal ValueSum { get; set; }

        public decimal CostSum { get; set; }
    }
}
