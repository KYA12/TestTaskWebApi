using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;

namespace TestAppWebApi.ViewModels
{
    public class ListShopsViewModel
    {
        public List<ShopViewModel> Shops { get; set; }
        public ListShopsViewModel()
        {
            Shops = new List<ShopViewModel>();
        }
    }
}
