using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewarePractice
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TimeService>();
            //не смотря на то, что мы указываем AddTransient
            //время по адресу https://localhost:5001/time не обновляется
            //AddTransient создает новые экземпляры для сервисов, но не middleware

            //вариант добавления через метод расширения
            //services.AddTimeService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TimeService timeService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<TimerMiddleware>();

            app.UseMiddleware<TimerMiddleware2>();
            //проблему можно исправить, передавая аргумент не в конструктор а в метод
            //InvokeAsync (см. класс TimerMiddleware2)

            //либо передавая контекст


            //сервис мы можем передать как аргумент метода Configure
            //а также следующим образом:
            var aServise = app.ApplicationServices.GetService<TimeService>();


            app.Run(async (context) =>
            {
                //еще два варианта получения сервиса
                //var aService = context.RequestServices.GetService<TimeService>();
                ////возвращает null, если для данного сервиса не установлена зависимость

                //var aService = context.RequestServices.GetRequiredService<TimeService>();
                ////генерирует исключение, если для данного сервиса не установлена зависимость

                //в коде ниже мы работаем с сервисом напрямую
                //в данном случае, время также не обновляется
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($"Текущее время: {timeService?.Time}");
            });
        }
    }
}
