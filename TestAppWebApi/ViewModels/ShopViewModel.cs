using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;

namespace TestAppWebApi.ViewModels
{
    public class ShopViewModel
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public List<string> FullNames { get; set; }
        public List<string> Dates { get; set; }
        public ShopViewModel()
        {
            FullNames = new List<string>();
            Dates = new List<string>();
        }
    }
}
