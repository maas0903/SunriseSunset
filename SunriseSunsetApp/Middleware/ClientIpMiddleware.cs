using SunriseSunsetApp.Services;

namespace SunriseSunsetApp.Middleware
{
    public class ClientIpMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientIpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ClientInfoService clientInfo)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();


            var requestDetails = $"Method: {context.Request.Method}, " +
                         $"Path: {context.Request.Path}, " +
                         $"QueryString: {context.Request.QueryString}, " +
                         $"Headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}";


            var hasSessionCookie = context.Request.Cookies.ContainsKey(".AspNetCore.Session");

            //if (context.Request.ToString() = "")
            //{

            //}

            // If behind a proxy, you might need to get the IP from headers
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = context.Request.Headers["X-Forwarded-For"];
            }

            clientInfo.ClientIp = ipAddress;

            await _next(context);
        }
    }
}
