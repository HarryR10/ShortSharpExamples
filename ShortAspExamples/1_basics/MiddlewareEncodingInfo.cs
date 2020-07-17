using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _1_basics
{
    public class MiddlewareEncodingInfo
    {
        private RequestDelegate _next;
        string _exampleString;

        public MiddlewareEncodingInfo(RequestDelegate next, string exampleString)
        {
            _next = next;
            _exampleString = exampleString;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //выводим данные о Accept-Encoding
            var Encoding = context.Request.Headers["Accept-Encoding"];
            context.Request.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"{_exampleString} - {Encoding.ToString()}\n");

            await _next.Invoke(context);
        }
    }
}
