using System;
using System.Collections.Generic;

namespace TestAppWebApi.Models
{
    public partial class Consultant
    {
        public int ConsultantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateHiring { get; set; }
        public int? ShopId { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
