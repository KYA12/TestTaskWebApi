using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestAppWebApi.Models
{
    public partial class Consultant
    {
        public int ConsultantId { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
        public DateTime? DateHiring { get; set; }
        public int? ShopId { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
