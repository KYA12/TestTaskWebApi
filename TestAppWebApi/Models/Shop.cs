using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestAppWebApi.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Consultant = new HashSet<Consultant>();
        }

        public int ShopId { get; set; }
        [Required(ErrorMessage = "Не указано название")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }

        public virtual ICollection<Consultant> Consultant { get; set; }
    }
}
