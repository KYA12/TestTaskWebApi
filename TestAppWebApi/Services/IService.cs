using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;
using TestAppWebApi.ViewModels;

namespace TestAppWebApi.Services
{
    public interface IService
    {
        Task<List<ShopViewModel>> GetListShops();
        Task<bool> AddShop(AddShopViewModel model);
        Task<bool> AddConsultant(AddConsultantViewModel model);
        Task<ListShopsConsultants> GetShopsConsultants();
        Task<bool> AppointConsultant(AppointConsultantViewModel model);
        Task<bool> CheckShopName(string shopName);

    }
}
