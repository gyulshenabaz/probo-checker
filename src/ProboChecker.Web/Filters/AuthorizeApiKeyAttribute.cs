using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProboChecker.Services.Interfaces;

namespace ProboChecker.Web.Filters
{
    public class AuthorizeApiKeyFilter : ActionFilterAttribute
    {
        private const string ApiKeyQueryName = "apiKey";
        private IApiKeysService apiKeysService;
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            apiKeysService = (IApiKeysService)context.HttpContext.RequestServices.GetService(typeof(IApiKeysService));

            var apiKey = context.HttpContext.Request.Query[ApiKeyQueryName].ToString();

            if (string.IsNullOrEmpty(apiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "API key is required"
                };
                return;
            }

            if (!apiKeysService.IsKeyValid(apiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "The provided key is invalid"
                };

                return;
            }

            base.OnActionExecuting(context);
        }

    }
}