using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;
using TestAppWebApi.Controllers;
using TestAppWebApi.DataAccess.UnitOfWork;
using TestAppWebApi.Models;
using TestAppWebApi.ViewModels;
using System.Collections.Generic;
using TestAppWebApi.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskWebApiTests
{
    public class ShopsControllerTests
    {
        private readonly Mock<IService> mockService;//Create Moq object of Repository
        private readonly ShopsController shopsController;//Create Moq object of AdminController
        private readonly List<ShopViewModel> expectedShopViewModels;//Create list of Posts
        private readonly Mock<ILogger<ShopsController>> logger;//create Moq object of Serilog

        public ShopsControllerTests()
        {
            mockService = new Mock<IService>();

            logger = new Mock<ILogger<ShopsController>>();

            //Setup moq controller with moq objects and local version of automapper
            shopsController = new ShopsController(mockService.Object, logger.Object);
            expectedShopViewModels = GetExpectedShopViewModels();

            //Setup all methods of the moq object of Repository  
            mockService.Setup(m => m.GetListShops()).Returns(Task.FromResult(expectedShopViewModels));

            mockService.Setup(m => m.AddShop(It.IsAny<AddShopViewModel>()))
                .Returns((AddShopViewModel target) =>
                {
                    ShopViewModel model = new ShopViewModel()
                    {
                        Id = 2,
                        ShopName = target.ShopName,
                        Address = target.Address,
                        FullNames = null,
                        Dates = null
                    };

                    expectedShopViewModels.Add(model);
                    return Task.FromResult(true);
                });
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = shopsController.GetShops();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsListShopViewModels()
        {
            // Arrange 
            List<ShopViewModel> models = expectedShopViewModels;

            // Act
            var okResult = shopsController.GetShops().Result as OkObjectResult;

            // Assert
            Assert.IsType<List<ShopViewModel>>(okResult.Value);

            Assert.Equal(models.Count, (okResult.Value as List<ShopViewModel>).Count);
        }

        private static List<ShopViewModel> GetExpectedShopViewModels()
        {
            List<ShopViewModel> list = new List<ShopViewModel>();
            ShopViewModel shopViewModel = new ShopViewModel
            {
                Id = 1,
                ShopName = "Магазин 1",
                Address = "Адрес 1",
                FullNames = null,
                Dates = null,
            };
            List<string> fullNames = new List<string>();
            fullNames.Add("Кошкин Дмитрий");
            shopViewModel.FullNames = fullNames;
            List<string> dates = new List<string>();
            dates.Add(DateTime.Now.ToString());
            list.Add(shopViewModel);
            return list;
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            AddShopViewModel addressMissingItem = new AddShopViewModel
            {
                ShopName = "Магазин 2",
                Address = null,
            };

            shopsController.ModelState.AddModelError("Address", "Required");
         
            // Act
            var badResponse = shopsController.AddShop(addressMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            AddShopViewModel shop = new AddShopViewModel
            {
                ShopName = "Магазин 2",
                Address = "Адрес 2"
            };

            // Act
            var createdResponse = shopsController.AddShop(shop);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedObject()
        {
            // Arrange
            AddShopViewModel shop = new AddShopViewModel
            {
                ShopName = "Магазин 2",
                Address = "Адрес 2"
            };

            // Act
            var okValue = shopsController.AddShop(shop).Result as OkObjectResult;

            // Assert
            Assert.Equal(shop, okValue.Value as AddShopViewModel);
        }

    }
}
