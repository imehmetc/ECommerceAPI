using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Diagnostics;

namespace ECommerceAPI.API.ActionFilters
{
    public class PerformanceLoggingFilter : ActionFilterAttribute
    {
        public Stopwatch _stopwatch { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _stopwatch.Stop();

            var controllerName = context.Controller.GetType().Name;

            var actionName = context.ActionDescriptor.DisplayName;
            var methodName = actionName.Split('.').Reverse().Skip(1).First();
            var methodName2 = methodName.Split(' ').Reverse().Skip(1).First();

            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
            var statusCode = context.HttpContext.Response.StatusCode;
            var exception = context.Exception;

            // Loglama yap
            if (context.Exception != null || statusCode != 200)
            {
                // Hata durumunda loglama
                Log.Error(exception, $"{controllerName} içindeki {methodName2} eyleminde bir hata oluştu. Süre: {elapsedMilliseconds} ms. Durum Kodu: {statusCode}");
                context.ExceptionHandled = false; // Hatanın işlem görmesini sağlayarak client'a iletilmesini sağlar.               
            }
            else
            {
                // Başarı durumunda loglama
                Log.Information($"{controllerName} içindeki {methodName2} eylemi {elapsedMilliseconds} ms içinde tamamlandı. Durum Kodu: {statusCode}");
            }
        }
    }
}
