using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppWebApi.DataAccess.UnitOfWork;
using TestAppWebApi.Models;
using Microsoft.Extensions.Logging;
using TestAppWebApi.ViewModels;
using System.Collections.Concurrent;

namespace TestAppWebApi.Services
{
    public class Service : IService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
        private readonly ShopDataBaseContext shopContext;
        public  Service(IUnitOfWork uow, ILogger<Service> _loger, ShopDataBaseContext con)
        {
            unitOfWork = uow;
            logger = _loger;
            shopContext = con;
        }

        // Получение списка магазинов с назначенными консультантами 
        public async Task<ConcurrentBag<ShopViewModel>> GetListShops()
        {
            try
            {
                var shopsList = await shopContext.Shop.Include(c => c.Consultant).ToListAsync();
                ConcurrentBag<ShopViewModel> shopViewModelList = new ConcurrentBag<ShopViewModel>();
                Parallel.ForEach(shopsList, shop => {
                    shopViewModelList.Add(AddToList(shop));
                });
                return shopViewModelList;
            
            }
            catch (Exception ex)
            {
                logger.LogError("Error in Service.GetListShops(): {0}", ex.Message);
            }
            return null;
        }

        // Добавление магазина
        public async Task<bool> AddShop(AddShopViewModel model)
        {
            Shop shop = new Shop()
            {
                ShopName = model.ShopName,
                Address =  model.Address,
            };
            try
            {
                await unitOfWork.Shops.Add(shop);
                return await unitOfWork.Complete();
            }
            catch(Exception ex)
            {
                logger.LogError("Error in Consultants/AddShop(): {0}", ex.Message);
                unitOfWork.Rollback();
            }
            return false;
        }

        // Добавление консультанта
        public async Task<bool> AddConsultant(AddConsultantViewModel model)
        {
            Consultant consultant = new Consultant()
            {
                Name = model.Name,
                Surname = model.Surname,
                DateHiring = null,
                ShopId = null
            };
            try
            {
                await unitOfWork.Consultants.Add(consultant);
                return await unitOfWork.Complete();
            }
            catch(Exception ex)
            {
                logger.LogError("Error in Consultants/AddConsultant: {0}", ex.Message);
                unitOfWork.Rollback();
            }
            return false;
        }

        // Получение списка всех магазинов и косультантов
        public async Task<ShopsConsultantsViewModel> GetShopsConsultants()
        {
            try
            {
                var consultantsList = await unitOfWork.Consultants.GetAll().ToListAsync();
                var shopsList = unitOfWork.Shops.GetAll().ToList();
                Dictionary<int, string> shops = new Dictionary<int, string>();
                Dictionary<int, string> consultants = new Dictionary<int, string>();
                
                foreach (var shop in shopsList)
                {
                    shops.Add(shop.ShopId, shop.ShopName);
                }

                foreach (var consultant in consultantsList)
                {
                    consultants.Add(consultant.ConsultantId, string.Join(' ', consultant.Name, consultant.Surname));
                }

                ShopsConsultantsViewModel list = new ShopsConsultantsViewModel()
                {
                    Shops = shops,
                    Consultants = consultants
                };
                
                return list;
            }
            catch (Exception ex)
            {
                logger.LogError("Error in Consultants/GetShopsConsultants: {0}", ex.Message);
            }

            return null;
        }

    public ShopViewModel AddToList(Shop shop)
    {
        ShopViewModel model = new ShopViewModel()
        {
            ShopId = shop.ShopId,
            ShopName = shop.ShopName,
            Address = shop.Address,
            FullNameList = shop.Consultant.Select(c => c.Name + ' ' + c.Surname).ToList(),
            DateList = shop.Consultant.Select(c => c.DateHiring.ToString()).ToList()
        };
        return model;
    }

    // Назначение консультанта в магазин
    public async Task<bool> AppointConsultant(AppointConsultantViewModel model)
    {
            try
            {
                var shop = unitOfWork.Shops.GetAll().FirstOrDefault(s => s.ShopId == Convert.ToInt32(model.ShopId));
                var consultant = unitOfWork.Consultants.GetAll().FirstOrDefault(c => c.ConsultantId == Convert.ToInt32(model.ConsultantId));
                consultant.ShopId = shop.ShopId;
                consultant.Shop = shop;
                consultant.DateHiring = DateTime.Now;
                shop.Consultant.Add(consultant);
                unitOfWork.Shops.Update(shop);
                logger.LogInformation("Consultant {0} {1} was appointed to the shop {2} with address {3}", 
                    consultant.Name, consultant.Surname, shop.ShopName, shop.Address);
                return await unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                logger.LogError("Error in Consultants/AppointConsultant: {0}", ex.Message);
                unitOfWork.Rollback();
            }
            return false;
        }
    }
}
