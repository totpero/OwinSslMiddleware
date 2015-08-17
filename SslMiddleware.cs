using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace mvlSite.SslMiddleware
{
    public class SslMiddleware : OwinMiddleware
    {
        private readonly int _sslPort;

        public SslMiddleware(OwinMiddleware next, int sslPort) : base(next)
        {
            _sslPort = sslPort;
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.Request.Uri.Scheme == Uri.UriSchemeHttps)
            {
                await Next.Invoke(context);
                return;
            }

            await RedirectToHttps(context);
        }

        private async Task RedirectToHttps(IOwinContext context)
        {
            var tmpMethod = new HttpMethod(context.Request.Method);
            var uri = new UriBuilder(context.Request.Uri)
            {
                Scheme = Uri.UriSchemeHttps,
                Port = _sslPort
            };

            if (tmpMethod == HttpMethod.Get || tmpMethod == HttpMethod.Head)
            {
                context.Response.Redirect(uri.Uri.ToString());
                if (tmpMethod == HttpMethod.Get)
                    await SetRedirectMessage(context, uri);
            }
            else
            {
                await SetRedirectMessage(context, uri);
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
        }

        private static async Task SetRedirectMessage(IOwinContext context, UriBuilder uri)
        {
            var tmpNewNody = string.Format(
                "HTTPS is required<br/>The resource can be found at <a href=\"{0}\">{0}</a>.", uri.Uri.AbsoluteUri);
            context.Response.ContentType = "text/html; charset=utf-8";
            using (var tmpStream = new MemoryStream(Encoding.UTF8.GetBytes(tmpNewNody)))
            {
                context.Response.Headers["Content-Length"] = tmpStream.Length.ToString();
                await tmpStream.CopyToAsync(context.Response.Body, 81920, context.Request.CallCancelled);
            }
        }
    }
}