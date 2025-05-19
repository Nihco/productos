using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductosBFF.Filters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Excepcion
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            NewRelic.Api.Agent.NewRelic.NoticeError(context.Exception);
        }
    }
}
