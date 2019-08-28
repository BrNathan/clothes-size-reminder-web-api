using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
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
    }
}