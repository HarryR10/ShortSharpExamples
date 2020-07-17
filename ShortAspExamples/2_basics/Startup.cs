using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Basics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseDefaultFiles();
            //в wwwroot будет искаться файл default.htm(.html), index.htm(.html)
            //и будет использоваться как точка входа


            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("another.html");
            //app.UseDefaultFiles(options);
            //"другая" страница будет выводиться при условии того, что
            //первый app.UseDefaultFiles() закомментирован
            //при этом, до "не основной" странице из папки wwwroot
            //всегда можно добраться через адресную строку


            //app.UseDirectoryBrowser();
            //можно просматривать файлы в каталоге wwwroot


            var fileProviderOptions = new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/internal")),
                RequestPath = new PathString("/files")
            };

            app.UseDirectoryBrowser(fileProviderOptions);
            //здесь сопоставлена папка internal и путь localhost/files
            //файлы видны, но открыть их нельзя


            app.UseStaticFiles();


        }
    }
}
