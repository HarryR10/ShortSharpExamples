using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MVCRazorExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            //подключение маршрутизации с использованием RouterMiddleware
            services.AddMvc(MvcOptions => MvcOptions.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "my",
                    template: "my/{controller=Test}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                //controller=Home - указывает, что здесь первым должно идти имя класса контроллера.
                //Home - по умолчанию
                //action=Index - параметр action позволяет указать,
                //какой метод класса контроллера нужно вызвать для обработки запроса
                //Index - по умолчанию
                //{id?} – это параметр метода.Он не обязательный - об этом говорит знак вопроса
            });
            //результат работы можно увидеть запустив в браузере https://localhost:5001/home/index/ + строка
        }
    }
}
