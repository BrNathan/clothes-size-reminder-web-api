using BLL.Interfaces;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/clothes-category")]
    [ApiController]
    public class ClothesCategoryController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IClothesCategoryService _clothesCategoryService;

        public ClothesCategoryController(IClothesCategoryService clothesCategoryService)
        {
            this._clothesCategoryService = clothesCategoryService;
        }

        [HttpGet]
        public IActionResult GetAllClothesCategories()
        {
            try
            {
                IEnumerable<ClothesCategory> clothesCategories = _clothesCategoryService.GetAllClothesCategories();
                return Ok(clothesCategories);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesCategory/GetAllClothesCategories");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ClothesCategoryById")]
        public IActionResult GetClothesCategoryById(int id)
        {
            try
            {
                ClothesCategory clothesCategory = _clothesCategoryService.GetClothesCategoryById(id);
                if (clothesCategory == null)
                {
                    _logger.Error($"ClothesCategory with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(clothesCategory);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesCategory/GetClothesCategoryById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateClothesCategory([FromBody] ClothesCategory clothesCategory)
        {
            try
            {
                if (clothesCategory.IsEntityNull())
                {
                    return BadRequest("ClothesCategory object is null");
                }

                if (!clothesCategory.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _clothesCategoryService.CreateClothesCategory(clothesCategory);
                _clothesCategoryService.Save();

                return CreatedAtRoute("ClothesCategoryById", new { id = clothesCategory.Id.Value }, clothesCategory);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesCategory/CreateClothesCategory", clothesCategory);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClothesCategory(int id, [FromBody] ClothesCategory clothesCategory)
        {
            try
            {
                if (clothesCategory.IsEntityNull())
                {
                    return BadRequest("ClothesCategory object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                ClothesCategory dbClothesCategory = _clothesCategoryService.GetClothesCategoryById(id);
                if (dbClothesCategory.IsEntityNull())
                {
                    _logger.Error($"ClothesCategory with id: {id} not found in db");
                    return NotFound();
                }

                _clothesCategoryService.UpdateClothesCategory(dbClothesCategory, clothesCategory);
                _clothesCategoryService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesCategory/UpdateClothesCategory/" + id, clothesCategory);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClothesCategory(int id)
        {
            try
            {
                ClothesCategory clothesCategory = _clothesCategoryService.GetClothesCategoryById(id);
                if (clothesCategory.IsEntityNull())
                {
                    _logger.Error($"ClothesCategory with id: {id} not found in db");
                    return NotFound();
                }

                _clothesCategoryService.DeleteClothesCategory(clothesCategory);
                _clothesCategoryService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/clothesCategory/DeleteClothesCategory/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}