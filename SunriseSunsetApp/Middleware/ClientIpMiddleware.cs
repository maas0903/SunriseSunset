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
