using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspException
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Production";
            //явная установка EnvironmentName в "Production" позволяет игнорировать
            //следущую инструкцию:

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            #region HTTPExceptions
            //обработка ошибок HTTP
            //так переход по несуществующему пути вызовет ошибку 404
            //app.UseStatusCodePages();

            ////вариант с переадресацией
            ////"может быть неудобно, особенно с точки зрения поисковой индексации"
            //app.UseStatusCodePagesWithRedirects("/error?code={0}");

            ////"Формально мы получим тот же ответ, так как так же будет идти перенаправление на путь "/error?code=404".
            ////Но теперь браузер получит оригинальный статусный код 404"
            //app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
            //app.Map("/error", ap => ap.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"Err: {context.Request.Query["code"]}");
            //}));
            #endregion

            #region UseExceptionHandler
            ////В релизных версиях должна осуществляться обработка ошибок
            //if (env.IsProduction())
            //{
            //    app.UseExceptionHandler("/error");
            //}
            //app.Map("/error", ap => ap.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Деление на ноль!");
            //}));


            //В другой перегрузке UseExceptionHandler принимается делегат
            //доработанный пример с сайта Microsoft
            if (env.IsProduction())
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        //для корректного отображения русского языка необходимо
                        //указать ближе к началу следующий ContentType:
                        context.Response.ContentType = "text/html; charset=utf-8"; 

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is DivideByZeroException)
                        {
                            await context.Response.WriteAsync("Ошибка деления (деление на ноль)!<br><br>\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
            }
            #endregion

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    int a = 1;
                    int b = 0;
                    await context.Response.WriteAsync((a / b).ToString());
                    await context.Response.WriteAsync("It's a page...");
                    //throw new Exception("ошибка");
                    ////попытка вызвать Exception не дает провести запуск приложения
                });
            });
        }
    }
}
