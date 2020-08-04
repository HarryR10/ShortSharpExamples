using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MiddlewarePractice
{
    public class TimerMiddleware2
    {
        private readonly RequestDelegate _next;

        public TimerMiddleware2(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TimeService timeService)
        {
            if (context.Request.Path.Value.ToLower() == "/time2")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        //как дополнительная перегрузка, этот метод не работает:

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    context.Response.ContentType = "text/html; charset=utf-8";
        //    if (context.Request.Path.Value.ToLower() == "/time2")
        //    {
        //        TimeService timeService = context.RequestServices.GetService<TimeService>();
        //        await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
        //    }
        //    else
        //    {
        //        await context.Response.WriteAsync($"Параметр неопределен");
        //    }
        //}
    }
}
