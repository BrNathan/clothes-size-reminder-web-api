using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebApi.Controllers
{
    [Route("api/clothes")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private IRepositoryWrapper _repository;
        private readonly IClothesService _clothesService;


        public ClothesController(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [HttpGet]
        public IActionResult GetAllClothes()
        {
            try
            {
                IEnumerable<Clothes> clothes = _clothesService.GetAllClothes();
                return Ok(clothes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/GetAllClothes");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ClothesById")]
        public IActionResult GetClothesById(int id)
        {
            try
            {
                Clothes clothes = _clothesService.GetClothesById(id);
                if (clothes.IsEntityNull())
                {
                    _logger.Error($"Clothes with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(clothes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/GetClothesById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}/details")]
        public IActionResult GetClothesWithDetails(int id)
        {
            try
            {
                ClothesExtended clothesExtended = _clothesService.GetClothesWithDetails(id);
                if (clothesExtended == null)
                {
                    _logger.Error($"Clothes with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(clothesExtended);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/GetClothesWithDetails/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateClothes([FromBody]Clothes clothes)
        {
            try
            {
                if (clothes.IsEntityNull())
                {
                    return BadRequest("Clothes object is null");
                }

                if (!clothes.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _clothesService.CreateClothes(clothes);
                _clothesService.Save();

                return CreatedAtRoute("ClothesById", new { id = clothes.Id.Value }, clothes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/CreateClothes", clothes);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClothes(int id, [FromBody]Clothes clothes)
        {
            try
            {
                if (clothes.IsEntityNull())
                {
                    return BadRequest("Clothes object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Clothes dbClothes = _clothesService.GetClothesById(id);
                if (dbClothes.IsEntityNull())
                {
                    _logger.Error($"Clothes with id: {id} not found in db");
                    return NotFound();
                }

                _clothesService.UpdateClothes(dbClothes, clothes);
                _clothesService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/UpdateClothes/" + id, clothes);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClothes(int id)
        {
            try
            {
                Clothes clothes = _clothesService.GetClothesById(id);
                if (clothes.IsEntityNull())
                {
                    _logger.Error($"Clothes with id: {id} not found in db");
                    return NotFound();
                }

                _clothesService.DeleteClothes(clothes);
                _clothesService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothes/DeleteClothes/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}