using System;
using System.Collections.Generic;

namespace TestAppWebApi.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Consultant = new HashSet<Consultant>();
        }

        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Consultant> Consultant { get; set; }
    }
}
