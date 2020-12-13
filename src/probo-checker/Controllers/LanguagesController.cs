using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using probo_checker.Common;

namespace probo_checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LanguagesController : ControllerBase
    {
        /// <summary>
        /// Get all supported languages
        /// </summary>
        /// <returns>All supported languages</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public virtual ActionResult Get()
        {
            var supportedLanguages = Enum.GetValues(typeof(SupportedLanguages))
                .Cast<SupportedLanguages>()
                .Select(l => l.ToString())
                .ToList();

            if (supportedLanguages.Count == 0)
            {
                return NoContent();
            }

            return Ok(supportedLanguages);
        }
    }
}