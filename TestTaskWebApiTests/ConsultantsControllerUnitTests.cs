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
    public class ConsultantsControllerUnitTests
    {
        private readonly Mock<IService> mockService;//Create Moq object of Repository
        private readonly ConsultantsController consultantsController;//Create Moq object of AdminController
        private readonly ListShopsConsultants expectedListShopsConsultants;//Create list of Posts
        private readonly Mock<ILogger<ConsultantsController>> logger;//create Moq object of Serilog

        public ConsultantsControllerUnitTests()
        {
            mockService = new Mock<IService>();

            logger = new Mock<ILogger<ConsultantsController>>();

            //Setup moq controller with moq objects and local version of automapper
            consultantsController = new ConsultantsController(mockService.Object, logger.Object);
            expectedListShopsConsultants = GetExpectedListShopsConsultants();

            //Setup all methods of the moq object of Repository  
            mockService.Setup(m => m.GetShopsConsultants()).Returns(Task.FromResult(expectedListShopsConsultants));

            mockService.Setup(m => m.AddConsultant(It.IsAny<AddConsultantViewModel>()))
                .Returns((AddConsultantViewModel target) =>
                {
                    expectedListShopsConsultants.Consultants.Add(4, target.Name);
                    return Task.FromResult(true);
                });

            mockService.Setup(m => m.AppointConsultant(It.IsAny<AppointConsultantViewModel>()))
                .Returns((AppointConsultantViewModel target) =>
                {
                    string shopName;
                    var shop = expectedListShopsConsultants.Shops.
                        TryGetValue(Convert.ToInt32(target.ShopId), out shopName);
                    if (shop)
                    {
                        return Task.FromResult(true);
                    }

                    return Task.FromResult(false);
                });
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult =  consultantsController.GetShopsConsultants();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsListShopsConsultants()
        {
            // Act
            var okResult = consultantsController.GetShopsConsultants().Result as OkObjectResult;

            ListShopsConsultants models = expectedListShopsConsultants;

            // Assert
            var list = Assert.IsType<ListShopsConsultants>(okResult.Value);

            Assert.Equal(models, (okResult.Value as ListShopsConsultants));
        }

        private static ListShopsConsultants GetExpectedListShopsConsultants()
        {
            Dictionary<int, string> shops = new Dictionary<int, string>();
            Dictionary<int, string> consultants = new Dictionary<int, string>();
            shops.Add(1, "Магазин 1");
            shops.Add(2, "Магазин 2");
            shops.Add(3, "Магазин 3");
            consultants.Add(1, "Консультант 1");
            consultants.Add(2, "Консультант 2");
            consultants.Add(3, "Консультант 3");
            ListShopsConsultants list = new ListShopsConsultants()
            {
                Shops = shops,
                Consultants = consultants
            };

            return list;
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            AddConsultantViewModel surnameMissingItem = new AddConsultantViewModel
            {
                Name = "Иван",
                Surname = null  
            };

            consultantsController.ModelState.AddModelError("Surname", "Required");

            // Act
            var badResponse = consultantsController.AddConsultant(surnameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            AddConsultantViewModel consultant = new AddConsultantViewModel
            {
                Name = "Иван",
                Surname = "Михайлов"
            };

            // Act
            var createdResponse = consultantsController.AddConsultant(consultant);

            // Assert
            Assert.IsType<OkResult>(createdResponse.Result);
        }
 
        [Fact]
        public void Appoint_ValidObjectPassed_ReturnsOkResponse()
        {
            // Arrange
            AppointConsultantViewModel model = new AppointConsultantViewModel
            {
                ShopId = "1",
                ConsultantId = "1"
            };

            // Act
            var okResponse = consultantsController.AppointConsultantToShop(model);

            // Assert
            Assert.IsType<OkResult>(okResponse.Result);
        }
    }
}
