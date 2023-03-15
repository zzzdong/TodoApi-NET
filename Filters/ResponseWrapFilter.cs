using Base.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApi.Filters
{
    public class ResponseWrapFilter : IActionFilter, IAsyncActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception ex)
            {
                context.Exception = null;
                context.Result = new ObjectResult(new ResponseWrapper(ex));
                return;
            }

            if (context.Result is IActionResult rst)
            {
                ObjectResult result = rst as ObjectResult;
                context.Result = new ObjectResult(new ResponseWrapper(result.Value));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // throw new NotImplementedException();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this.OnActionExecuting(context);
            var result = await next();
            this.OnActionExecuted(result);
        }
    }



    public class ResponseWrapper
    {

        public object? data { get; set; }
        public int code { get; set; }
        public string message { get; set; }

        public ResponseWrapper(object? data)
        {
            this.data = data;
            this.code = 0;
            this.message = "ok";
        }

        public ResponseWrapper(Exception ex)
        {
            if (ex is PlatformException pe)
            {
                this.code = pe.code;
                this.message = pe.message;
            }
            else
            {
                this.code = -1;
                this.message = "server failed";
            }

            this.data = new Dictionary<string, string>();
        }


    }
}