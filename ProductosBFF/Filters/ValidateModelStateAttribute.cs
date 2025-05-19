using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductosBFF.Models.Commons;
using System.Linq;

namespace ProductosBFF.Filters
{
    /// <summary>
    /// ValidateModelStateAttribute Class
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// ValidateModelStateAttribute => OnActionExecuting method
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                int badRequestStatusCode = 400;
                string message = "Bad Request: One or more validation failures have occurred.";

                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var responseResult = new GenericResult<object>(null, badRequestStatusCode, message, errors);

                context.Result = new JsonResult(responseResult)
                {
                    StatusCode = badRequestStatusCode
                };
            }
        }
    }
}
