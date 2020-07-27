using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppWebApi.ViewModels
{
    public class ShopsConsultantsViewModel
    {
        public Dictionary<int,string> Shops { get; set; }
        public Dictionary<int,string> Consultants { get; set; }
    }
}
