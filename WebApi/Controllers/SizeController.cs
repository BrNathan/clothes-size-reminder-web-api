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
    [Route("api/size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public IActionResult GetAllSizes()
        {
            try
            {
                IEnumerable<Size> sizes = _sizeService.GetAllSizes();
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/size/GetAllSizes");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "SizeById")]
        public IActionResult GetSizeById(int id)
        {
            try
            {
                Size size = _sizeService.GetSizeById(id);
                if (size == null)
                {
                    _logger.Error($"Size with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(size);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/size/GetSizeById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateSize([FromBody] Size size)
        {
            try
            {
                if (size.IsEntityNull())
                {
                    return BadRequest("Size object is null");
                }

                if (!size.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _sizeService.CreateSize(size);
                _sizeService.Save();

                return CreatedAtRoute("SizeById", new { id = size.Id.Value }, size);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/size/CreateSize", size);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSize(int id, [FromBody] Size size)
        {
            try
            {
                if (size.IsEntityNull())
                {
                    return BadRequest("Size object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Size dbSize = _sizeService.GetSizeById(id);
                if (dbSize.IsEntityNull())
                {
                    _logger.Error($"Size with id: {id} not found in db");
                    return NotFound();
                }

                _sizeService.UpdateSize(dbSize, size);
                _sizeService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/size/UpdateSize/" + id, size);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSize(int id)
        {
            try
            {
                Size size = _sizeService.GetSizeById(id);
                if (size.IsEntityNull())
                {
                    _logger.Error($"Size with id: {id} not found in db");
                    return NotFound();
                }

                _sizeService.DeleteSize(size);
                _sizeService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/size/DeleteSize/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}