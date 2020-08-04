using System;
using Microsoft.Extensions.DependencyInjection;

namespace MiddlewarePractice
{
    public class TimeService
    {
        public string Time { get; }

        public TimeService()
        {
            Time = System.DateTime.Now.ToString("hh:mm:ss");
        }
    }


    //через метод расширения мы можем добавить этот сервис
    //в Startup.ConfigureServices
    public static class ServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
            services.AddTransient<TimeService>();
        }
    }
}
