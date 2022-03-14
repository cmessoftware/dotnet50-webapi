using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace cmes_webapi.Filters
{
    public class ValidatorFiltersTipoOperatoria : IActionFilter
    {
        private readonly ILogger<ValidatorFiltersTipoOperatoria> _logger;
        public ValidatorFiltersTipoOperatoria(ILogger<ValidatorFiltersTipoOperatoria> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }
        }
    }
}
