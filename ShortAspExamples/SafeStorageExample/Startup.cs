using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SafeStorageExample
{
    public class Startup
    {

        private string _moviesApiKey = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //первый вариант - просто в строку
            //_moviesApiKey = Configuration["Movies:ServiceApiKey"];

            //второй вариант - если настроек много - через класс посредник
            var moviesConfig = Configuration.GetSection("Movies")
                                            .Get<MovieSettings>();
            _moviesApiKey = moviesConfig.ServiceApiKey;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async (context) =>
            {
                var result = string.IsNullOrEmpty(_moviesApiKey) ? "Null" : "Not Null";
                await context.Response.WriteAsync($"Secret is {result}\n");

                await context.Response.WriteAsync($"Secret is {_moviesApiKey}");
            });
        }
    }
}
