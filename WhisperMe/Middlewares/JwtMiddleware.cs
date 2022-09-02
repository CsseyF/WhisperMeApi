using Microsoft.AspNetCore.Authorization;
using WhisperMe.Helpers;
using WhisperMe.Services.Interfaces;

namespace WhisperMe.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            try
            {
                var endpoint = context.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object || context.Request.Method.ToLower().Equals("options") || context.Request.Path.ToString().Contains("swagger"))
                {
                    await _next(context); return;
                }

                var token = context.Request.Cookies.FirstOrDefault(c => c.Key == "token").Value;
                var claims = jwtUtils.ValidateJwtToken(token);
                if (claims == null)
                {
                    throw new Exception("Invalid Token");
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
