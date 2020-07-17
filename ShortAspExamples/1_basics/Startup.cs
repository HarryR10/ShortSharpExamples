using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _1_basics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //подключам MiddlewareExampleClass
            app.UseMiddleware<MiddlewareExampleClass>();

            //подключам MiddlewareHostInfo с помощью класса MiddlewareExtension
            app.UseHostInfo();

            //подключам MiddlewareContentLengthInfo с помощью класса 
            //MiddlewareExtension и передаем тестовую строку
            app.UseContentLengthInfo("Accept-Encoding");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("\nHello, Users!");
                });
            });
        }
    }
}
