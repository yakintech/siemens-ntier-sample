using Microsoft.AspNetCore.Mvc.Filters;

namespace Siemens.API.Models.Filters
{
    public class ResponseHeaderFilter : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderFilter(string name, string value)
        {
            _name = name;
            _value = value;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _value);
            context.HttpContext.Response.Headers.Add("userId", "1");

            base.OnActionExecuting(context);
        }
    }
}
