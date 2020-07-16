using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace _1_basics
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseHostInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareHostInfo>();
        }

        public static IApplicationBuilder UseContentLengthInfo(this IApplicationBuilder builder, string exampleString)
        {
            return builder.UseMiddleware<MiddlewareEncodingInfo>(exampleString);
        }
    }
}
