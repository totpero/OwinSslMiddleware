using System.Web.Http;
using Owin;

namespace mvlSite.SslMiddleware
{
    public static class SslExtension
    {
        public static IAppBuilder UseSsl(this IAppBuilder appBuilder, int sslPort)
        {
            return appBuilder.Use<SslMiddleware>(sslPort);
        }
    }
}