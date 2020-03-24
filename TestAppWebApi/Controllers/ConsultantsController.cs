using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppWebApi.Services;
using Microsoft.Extensions.Logging;
using TestAppWebApi.ViewModels;
using Microsoft.AspNetCore.Http;

namespace TestAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultantsController : ControllerBase
    {

        private readonly ILogger logger;
        private readonly IService service;
        public ConsultantsController(IService _service, ILogger<ConsultantsController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        // POST
        // api/consultants/addconsultant
        /// <summary>
        /// Creates a new Consultant
        /// </summary>
        /// <param name="post">Post model</param>
        /// <returns>A response with new Ok</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="201">A response as creation of consultant</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AddConsultant([FromBody] AddConsultantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogInformation("Method Home/AddConsultant() called ");
                return BadRequest(ModelState);
            }

            if (await service.AddConsultant(model))
            {
                logger.LogInformation("Result is Ok()");
                return Ok();
            }
            logger.LogInformation("Result is BadRequest()");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        // GET
        // api/shops/
        /// <summary>
        /// Get avaible shops and consultants
        /// </summary>
        /// <param name="get">Get model</param>
        /// <returns>A response with Ok</returns>
        /// <response code="200">Returns response with avaible shops and consultants</response>
        /// <response code="404">If shop is not exists</response>
        /// <response code="500">If there was an internal server error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult> GetShopsConsultants()
        {
            logger.LogInformation("Method Home/GetAttachConsultant called ");
            var list = await service.GetShopsConsultants();
            if (list != null)
            {
                logger.LogInformation("Result is Ok()");
                return Ok(list);
            }

            logger.LogInformation("Result is NotFound(404)");
            return NotFound();
        }

        // POST
        // api/consultants/
        /// <summary>
        /// Appoint consultant to the shop
        /// </summary>
        /// <param name="post">Post model</param>
        /// <returns>A response with new Ok</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="201">A response as creation an appointment of the consultant to the shop</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AppointConsultantToShop([FromBody] AppointConsultantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogInformation("Method Home/AttachConsultant() is called ");
                return BadRequest(ModelState);
            }

            if (await service.AppointConsultant(model))
            {
                logger.LogInformation("Result OK()");
                return Ok();
            }
            logger.LogInformation("Result BadRequest");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        }

    }
}
