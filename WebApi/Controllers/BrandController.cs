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
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private IRepositoryWrapper _repository;

        public BrandController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllBrands()
        {
            try
            {
                IEnumerable<Brand> brands = _repository.Brand.GetAllBrands();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/brand/GetAllBrands");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            try
            {
                Brand brand = _repository.Brand.GetBrandById(id);
                if (brand == null)
                {
                    _logger.Error($"Brand with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(brand);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/brand/GetBrandById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateBrand([FromBody]Brand brand)
        {
            try
            {
                if (brand.IsEntityNull())
                {
                    return BadRequest("Gender object is null");
                }

                if (!brand.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.Brand.Create(brand);
                _repository.Save();

                return CreatedAtRoute("BrandById", new { id = brand.Id.Value }, brand);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/brand/CreateBrand", brand);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateBrand(int id, [FromBody]Brand brand)
        {
            try
            {
                if (brand.IsEntityNull())
                {
                    return BadRequest("Brand object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Brand dbBrand = _repository.Brand.GetBrandById(id);
                if (dbBrand.IsEntityNull())
                {
                    _logger.Error($"Brand with id: {id} not found in db");
                    return NotFound();
                }

                _repository.Brand.UpdateBrand(dbBrand, brand);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/brand/UpdateBrand/" + id, brand);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {
            try
            {
                Brand brand = _repository.Brand.GetBrandById(id);
                if (brand.IsEntityNull())
                {
                    _logger.Error($"Brand with id: {id} not found in db");
                    return NotFound();
                }

                _repository.Brand.DeleteBrand(brand);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/brand/DeleteBrand/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}