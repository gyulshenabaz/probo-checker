using Microsoft.AspNetCore.Mvc;
using probo_checker.Services.Interfaces;

namespace probo_checker.Controllers
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
        /// Returns a new generated API key
        /// </summary>
        /// <returns>Returns the API key as a string</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Generate()
        {
            var apiKey = this.apiKeysService.Generate();

            return this.Ok(apiKey);
        }
    }
}
