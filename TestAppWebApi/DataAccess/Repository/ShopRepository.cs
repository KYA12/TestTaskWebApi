using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;

namespace TestAppWebApi.DataAccess.Repository
{
    // Класс репозитория магазина
    public class ShopRepository: GenericRepository<Shop>, IShopRepository 
    {
        private readonly ShopDataBaseContext _context;
        public ShopRepository(ShopDataBaseContext context) : base(context)
        {
            _context = context;

        }
    }
}
