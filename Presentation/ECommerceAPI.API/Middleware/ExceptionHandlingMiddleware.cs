using Serilog;

namespace ECommerceAPI.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "İstek işlenirken işlenmeyen bir özel durum oluştu.");
                throw;
            }
        }
    }
}
