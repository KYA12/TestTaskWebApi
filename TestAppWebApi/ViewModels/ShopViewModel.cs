using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppWebApi.ViewModels
{
    public class ShopViewModel
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public List<string> FullNameList { get; set; }
        public List<string> DateList { get; set; }
        public ShopViewModel() 
        {
            FullNameList = new List<string>();
            DateList = new List<string>();
        }
    }
}
