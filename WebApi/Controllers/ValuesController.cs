using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();
        private IRepositoryWrapper _repositoryWrapper;

        public ValuesController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            this._logger.Debug("api/values");
            var brand = _repositoryWrapper.Brand.FindAll();
            var category = _repositoryWrapper.ClothesCategory.FindAll();
            var clothes = _repositoryWrapper.Clothes.FindByCondition(c => c.Code == "BASKET");
            var clothesSizes = _repositoryWrapper.ClothesSize.FindAll();
            var gender = _repositoryWrapper.Gender.FindAll();
            var reminder = _repositoryWrapper.Reminder.FindAll();
            var role = _repositoryWrapper.Role.FindAll();
            var size = _repositoryWrapper.Size.FindAll();
            var user = _repositoryWrapper.User.FindAll();
            var userRole = _repositoryWrapper.UserRole.FindAll();
            return new JsonResult("ok");
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
