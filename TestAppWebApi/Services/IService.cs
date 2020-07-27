using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.Models;
using TestAppWebApi.ViewModels;

namespace TestAppWebApi.Services
{
    public interface IService
    {
        Task<ConcurrentBag<ShopViewModel>> GetListShops();
        Task<bool> AddShop(AddShopViewModel model);
        Task<bool> AddConsultant(AddConsultantViewModel model);
        Task<bool> AppointConsultant(AppointConsultantViewModel model);
        Task<ShopsConsultantsViewModel> GetShopsConsultants();
        ShopViewModel AddToList(Shop shop);
    }
}
