﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Expenses.Database.Objects
{
    public class BagCategory
    {
        public int? BagCategoryId { get; set; }
        public int BagId { get; set; }
        public int CategoryId { get; set; }
        public Bag Bag { get; set; } = null;
        public Category Category { get; set; } = null;
    }
}
