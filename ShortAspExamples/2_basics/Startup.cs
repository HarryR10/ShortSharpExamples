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
            //app.UseStaticFiles();
            ////указываем, что работаем со статическими файлами

            #region UseDefaultFiles
            //app.UseDefaultFiles();            
            //app.UseStaticFiles();
            ////в wwwroot будет искаться файл default.htm(.html), index.htm(.html)
            ////и будет использоваться как точка входа


            //DefaultFilesOptions defFileOptions = new DefaultFilesOptions();
            //defFileOptions.DefaultFileNames.Clear();
            //defFileOptions.DefaultFileNames.Add("another.html");
            //app.UseDefaultFiles(defFileOptions);
            //app.UseStaticFiles();
            ////"другая" страница будет выводиться при условии того, что
            ////первый app.UseDefaultFiles() закомментирован
            ////при этом, до "не основной" странице из папки wwwroot
            ////всегда можно добраться через адресную строку
            #endregion

            #region UseDirectoryBrowser
            //app.UseDirectoryBrowser();
            ////можно просматривать файлы в каталоге wwwroot


            //var fileProviderOptions = new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/internal")),
            //    RequestPath = new PathString("/files")
            //};
            //app.UseDirectoryBrowser(fileProviderOptions);
            ////здесь сопоставлена папка internal и путь localhost/files
            ////файлы видны по пути localhost/files/, но открыть их нельзя

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //var stFileOptions = new StaticFileOptions()
            //{
            //    FileProvider =
            //    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/internal")),
            //    RequestPath = new PathString("/files")
            //};
            //app.UseStaticFiles(stFileOptions);
            ////здесь сопоставлена папка internal и путь localhost/files
            ////отличие от предыдущего примера - нет браузера файлов
            ////если использовать этот код совместно с UseDirectoryBrowser
            ////файлы внутри будут открываться для просмотра
            #endregion

            #region UseFileServer
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/internal")),
                RequestPath = new PathString("/files"),
                EnableDefaultFiles = false
            });

            //совмещение функционала из блока UseDirectoryBrowser
            //при этом стартовая страница также работает
            #endregion
        }
    }
}
