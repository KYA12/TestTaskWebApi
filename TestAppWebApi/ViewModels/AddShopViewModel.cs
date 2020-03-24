using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppWebApi.ViewModels
{ 
    public class AddShopViewModel
    {

        [Required(ErrorMessage = "Не указано название")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "Не указан адрес")]
        public string Address { get; set; }
    }
}
