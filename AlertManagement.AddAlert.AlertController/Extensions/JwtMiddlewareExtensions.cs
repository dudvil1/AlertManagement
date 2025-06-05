using AlertManagement.AddAlert.AlertController.Middleware;

namespace AlertManagement.AddAlert.AlertController.Extensions
{
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtValidation(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtValidationMiddleware>();
        }
    }
}
