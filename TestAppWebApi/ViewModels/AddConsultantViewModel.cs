using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppWebApi.ViewModels
{
    public class AddConsultantViewModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }
    }
}
