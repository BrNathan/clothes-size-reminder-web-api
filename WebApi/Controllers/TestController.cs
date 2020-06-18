using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Helpers;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        private readonly AppSettings _appsettings;

        public TestController(
            IOptions<AppSettings> appSettings)
        {
            _appsettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test()
        {
            IEnumerable<string> tests = new List<string>()
            {
                "toot",
                "rere",
                "AZE",
                "EFEJFJELF",
                _appsettings.JwtSecretKey
            };

            return Ok(tests);
        }
    }
}
