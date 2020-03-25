using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAppWebApi.Services;
using TestAppWebApi.ViewModels;
using TestAppWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TestAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {

        private readonly ILogger logger;
        private readonly IService service;

        public ShopsController(IService _service, ILogger<ShopsController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        // GET
        // api/shops/
        /// <summary>
        /// Get list of shops and attached consultants
        /// </summary>
        /// <param name="get">Get model</param>
        /// <returns>A response with Ok</returns>
        /// <response code="200">Returns a response with list of shops and attached consultants</response>
        /// <response code="404">If shop is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetShops()
        {
            logger.LogInformation("Method ShopsController/GetShops is called ");
            var results = await service.GetListShops();
            if (results != null)
            {
                logger.LogInformation("Result is Ok(200)");
                return Ok(results);
            }
            logger.LogInformation("Returns result NotFound(404)");
            return NotFound();
        }

        // POST
        // api/shops/
        /// <summary>
        /// Creates a new Shop
        /// </summary>
        /// <param name="post">Post model</param>
        /// <returns>A response with new Ok</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="201">A response as creation of shop</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AddShop([FromBody] AddShopViewModel model)
        {
            logger.LogInformation("Method Home/AddShop is called ");
            
            if (await service.CheckShopName(model.ShopName))
            {
                ModelState.AddModelError("ShopName", "Некорректное название");
            }

            if (!ModelState.IsValid)
            {
                logger.LogInformation("Result is BadRequest(400): {0}", ModelState.Values.ToString());
                return BadRequest(ModelState);
            }

            if (await service.AddShop(model))
            {
                logger.LogInformation("Result is Ok(200). Added shop with name: {0} and address: {1}", model.ShopName, model.Address);
                return Ok(model);
            }

            logger.LogInformation("Result is Internal Server Error(500)");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
