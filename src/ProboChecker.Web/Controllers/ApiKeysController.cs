using Microsoft.AspNetCore.Mvc;
using ProboChecker.Services.Interfaces;

namespace ProboChecker.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class ApiKeysController : ControllerBase
    {
        private readonly IApiKeysService apiKeysService;

        public ApiKeysController(IApiKeysService apiKeysService)
        {
            this.apiKeysService = apiKeysService;
        }

        /// <summary>
        /// Returns a new API key
        /// </summary>
        /// <returns>Returns API key as a string</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Generate()
        {
            var apiKey = this.apiKeysService.Generate();

            return this.Ok(apiKey);
        }
    }
}