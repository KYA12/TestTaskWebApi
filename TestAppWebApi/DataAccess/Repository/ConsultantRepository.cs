using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;

namespace TestAppWebApi.DataAccess.Repository
{
    // Класс репозитория консультантов
    public class ConsultantRepository: GenericRepository<Consultant>, IConsultantRepository
    {
        private readonly ShopDataBaseContext _context;
        public ConsultantRepository(ShopDataBaseContext context) : base(context)
        { 
            _context = context;

        }
    }
}
