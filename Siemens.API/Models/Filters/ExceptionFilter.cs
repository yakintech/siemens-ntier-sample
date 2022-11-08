using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Siemens.API.Models.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            if (context.Exception is DataNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }


            context.HttpContext.Response.ContentType =  "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;


            context.Result = new JsonResult(new
            {
                errors = new[] { context.Exception.Message },
                statusCode = (int)statusCode
            });


        }
    }
}
