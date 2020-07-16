using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _1_basics
{
    public class MiddlewareHostInfo
    {
        private RequestDelegate _next;

        public MiddlewareHostInfo(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //выводим данные о Host'е
            var host = context.Request.Headers["Host"];
            await context.Response.WriteAsync(host.ToString() + "\n");

            await _next.Invoke(context);
        }
    }
}
