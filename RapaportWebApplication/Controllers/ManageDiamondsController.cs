using Microsoft.AspNetCore.Mvc;
using RapaportWebApplication.Models;
using System.Net;
using RapaportWebApplication.Interfaces;
using System.Collections.Generic;
using System.Text.Json;

namespace RapaportWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ManageDiamondsController : ControllerBase
    {

        private readonly ILogger<ManageDiamondsController> _logger;
        private readonly IDiamondsCsvService _diamondsCsvService;

        public ManageDiamondsController(ILogger<ManageDiamondsController> logger, IDiamondsCsvService diamondsCsvService)
        {
            _logger = logger;
            _diamondsCsvService = diamondsCsvService;
        }

        [Route("PostNewDiamond")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public IActionResult PostNewDiamond(DiamondObject diamondObject)
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            _logger.LogInformation("PostNewDiamond try to post new diamond");

            try
            {
                if (!string.IsNullOrEmpty(diamondObject.clarity) && diamondObject.size > 0 && diamondObject.list_price > 0 &&
                    diamondObject.price > 0 && !string.IsNullOrEmpty(diamondObject.color) && !string.IsNullOrEmpty(diamondObject.shape))
                {
                    _diamondsCsvService.AddDiamondToCsvFile(diamondObject);
                    httpResponseModel.ErrorDesc = "good request";
                    httpResponseModel.ErrorCode = "200";
                    return Ok(httpResponseModel);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostNewDiamond faild with message: {ex.Message}");
            }



            httpResponseModel.ErrorDesc = "bad request";
            httpResponseModel.ErrorCode = "400";
            return BadRequest(httpResponseModel);

        }

        [Route("GetAllDiamonds")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public IActionResult GetAllDiamonds()
        {
            HttpResponseModel httpResponseModel = new HttpResponseModel();
            _logger.LogInformation("request for all diamond data");
            try
            {
                //throw new NotImplementedException();
                List<DiamondObject> diamonds = new List<DiamondObject>();
                diamonds = _diamondsCsvService.GetDiamondListFromCsvFile();
                _logger.LogInformation("GetAllDiamonds from csv file successed");
                return Ok(diamonds);

            }
            catch (Exception ex)
            { 
                _logger.LogError($"main prosess of get all diamond faild message:{ex.Message}");
                httpResponseModel.ErrorDesc = "internal error";
                httpResponseModel.ErrorCode = "500";
                return Ok(httpResponseModel);
            }

        }
    }
}