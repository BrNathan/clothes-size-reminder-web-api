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
    [Route("api/gender")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public IActionResult GetAllGenders()
        {
            try
            {
                IEnumerable<Gender> genders = _genderService.GetAllGenders();
                return Ok(genders);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/gender/GetAllGenders");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGenderById(int id)
        {
            try
            {
                Gender gender = _genderService.GetGenderById(id);
                if (gender == null)
                {
                    _logger.Error($"Gender with id: {id} not found in db");
                    return NotFound();
                }

                return Ok(gender);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/gender/GetGenderById/" + id.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateGender([FromBody]Gender gender)
        {
            try
            {
                if (gender.IsEntityNull())
                {
                    return BadRequest("Gender object is null");
                }

                if (!gender.IsEntityEmpty())
                {
                    return BadRequest("For create, the Id must be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _genderService.CreateGender(gender);
                _genderService.Save();

                return CreatedAtRoute("GenderById", new { id = gender.Id.Value }, gender);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/gender/CreateGender", gender);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateGender(int id, [FromBody]Gender gender)
        {
            try
            {
                if (gender.IsEntityNull())
                {
                    return BadRequest("Gender object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                Gender dbGender = _genderService.GetGenderById(id);
                if (dbGender.IsEntityNull())
                {
                    _logger.Error($"Gender with id: {id} not found in db");
                    return NotFound();
                }

                _genderService.UpdateGender(dbGender, gender);
                _genderService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/gender/UpdateGender/" + id, gender);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGender(int id)
        {
            try
            {
                Gender gender = _genderService.GetGenderById(id);
                if (gender.IsEntityNull())
                {
                    _logger.Error($"Gender with id: {id} not found in db");
                    return NotFound();
                }

                _genderService.DeleteGender(gender);
                _genderService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in call : api/gender/DeleteGender/" + id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}