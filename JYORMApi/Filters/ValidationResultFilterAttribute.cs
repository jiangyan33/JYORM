using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace JYORMApi.Filters
{
    public class ValidationResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not BadRequestObjectResult objectResult) return;

            if (objectResult.Value is not ValidationProblemDetails validationResults) return;

            context.Result = new ObjectResult(new
            {
                Code = 10004,
                Result = 0,
                Message = "参数序列化失败",
                Data = validationResults.Errors.Select(x => new
                {
                    field = x.Key,
                    message = x.Value
                })
            });
        }
    }
}
