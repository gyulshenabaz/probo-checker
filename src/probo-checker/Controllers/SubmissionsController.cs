using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using probo_checker.Common;
using probo_checker.Common.CodeChecker.Models;
using probo_checker.DataAccess.Models;
using probo_checker.Filters;
using probo_checker.Models.Submissions;
using probo_checker.Services.Interfaces;
using probo_checker.Services.Models;

namespace probo_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeApiKey]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionsService submissionService;

        public SubmissionsController(ISubmissionsService submissionService)
        {
            this.submissionService = submissionService;
        }
        
        /// <summary>
        /// Compiles, debugs and checks code
        /// </summary>
        /// <param name="apiKey">Api Key</param>
        /// <param name="submissionModel">Submission</param>
        /// <remarks>
        /// </remarks>
        /// <returns>Result</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SubmissionResult), 200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] string apiKey,
            [FromBody] SubmissionFormViewModel submissionModel)
        {
            var supportedLanguages = Enum.GetValues(typeof(SupportedLanguages))
                .Cast<SupportedLanguages>()
                .Select(l => l.ToString())
                .ToList();

            if (submissionModel == null)
            {
                return BadRequest("Data cannot be empty");
            }

            if (supportedLanguages.All(l => l != submissionModel.Language))
            {
                return BadRequest("Unsupported programming language");
            }

            var serviceModel = Mapper.Map<PostedSubmission>(submissionModel);

            SubmissionResult result;
            
            try
            {
                result = submissionService.Create(serviceModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }
    }
}