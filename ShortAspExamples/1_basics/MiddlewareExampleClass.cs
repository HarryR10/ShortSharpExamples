using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace _1_basics
{
    public class MiddlewareExampleClass
    {
        private readonly RequestDelegate _next;

        public MiddlewareExampleClass(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //выводим данные о User-Agent'е
            var userAgent = context.Request.Headers["User-Agent"];
            await context.Response.WriteAsync(userAgent.ToString() + "\n");

            //_next - служит для вызова следующего делегата
            await _next.Invoke(context);
        }
    }
}
