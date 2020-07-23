using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SafeStorageExample
{
    //справка и примеры кода:
    //https://docs.microsoft.com/ru-ru/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=linux
    //https://github.com/dotnet/AspNetCore.Docs/tree/master/aspnetcore/security/app-secrets/samples/3.x/UserSecrets
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
