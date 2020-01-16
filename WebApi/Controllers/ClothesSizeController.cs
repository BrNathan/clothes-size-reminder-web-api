using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebApi.Controllers
{
    [Route("api/clothesSize")]
    [ApiController]
    public class ClothesSizeController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IClothesSizeService _clothesSizeService;

        public ClothesSizeController(IClothesSizeService clothesSizeService)
        {
            _clothesSizeService = clothesSizeService;
        }

        [HttpGet]
        public IActionResult GetAllClothesSizes()
        {
            try
            {
                IEnumerable<ClothesSize> clothesSizes = _clothesSizeService.GetAllClothesSizes();
                return Ok(clothesSizes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesSize/GetAllClothesSizes");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ClothesSizeById")]
        public IActionResult GetClothesSizeById(int id)
        {
            try
            {
                ClothesSize clothesSize = _clothesSizeService.GetClothesSizeById(id);
                if (clothesSize == null)
                {
                    _logger.Error($"ClothesSize with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(clothesSize);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesSize/GetClothesSizeById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateClothesSize([FromBody]ClothesSize clothesSize)
        {
            try
            {
                if (clothesSize.IsEntityNull())
                {
                    return BadRequest("ClothesSize object is null");
                }

                if (!clothesSize.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _clothesSizeService.CreateClothesSize(clothesSize);
                _clothesSizeService.Save();

                return CreatedAtRoute("ClothesSizeById", new { id = clothesSize.Id.Value }, clothesSize);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesSize/CreateClothesSize", clothesSize);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClothesSize(int id, [FromBody]ClothesSize clothesSize)
        {
            try
            {
                if (clothesSize.IsEntityNull())
                {
                    return BadRequest("ClothesSize object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                ClothesSize dbClothesSize = _clothesSizeService.GetClothesSizeById(id);
                if (dbClothesSize.IsEntityNull())
                {
                    _logger.Error($"ClothesSize with id: {id} not found in db");
                    return NotFound();
                }

                _clothesSizeService.UpdateClothesSize(dbClothesSize, clothesSize);
                _clothesSizeService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesSize/UpdateClothesSize/" + id, clothesSize);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClothesSize(int id)
        {
            try
            {
                ClothesSize clothesSize = _clothesSizeService.GetClothesSizeById(id);
                if (clothesSize.IsEntityNull())
                {
                    _logger.Error($"ClothesSize with id: {id} not found in db");
                    return NotFound();
                }

                _clothesSizeService.DeleteClothesSize(clothesSize);
                _clothesSizeService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesSize/DeleteClothesSize/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}