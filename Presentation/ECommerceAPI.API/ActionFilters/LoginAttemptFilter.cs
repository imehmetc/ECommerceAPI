using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ECommerceAPI.API.ActionFilters
{
    public class LoginAttemptFilter : ActionFilterAttribute
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Giriş denemesi başlatıldığında loglama yapabilirsiniz (isteğe bağlı)
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Controller adını almak için GetType kullanıyoruz
            var controllerName = context.Controller?.GetType().Name;

            // Eğer Controller null ise, burada bir hata var demektir. (Nadir bir durum)
            if (string.IsNullOrEmpty(controllerName))
            {
                Log.Warning("Controller bilgisi alınamadı.");
                return;
            }

            // Eğer context.Result bir RedirectToActionResult değilse başka bir sonuca gitmemiz gerekebilir.
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.StatusCode == StatusCodes.Status200OK)
                {
                    // Giriş başarılıysa loglama yapıyoruz
                    Log.Information("{DateTime} tarihinde başarılı giriş yapılmıştır.", DateTime.UtcNow);
                }// Durum kodu 401 Unauthorized ise başarısız giriş
                else if (objectResult.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    Log.Warning("{DateTime} tarihinde oturum açma girişimi başarısız oldu: {Mesaj}", DateTime.UtcNow, objectResult.Value);
                }
            }
            // BadRequest durumunu kontrol ediyoruz
            else if (context.Result is BadRequestObjectResult badRequestResult)
            {
                Log.Warning("{DateTime} tarihinde oturum açma girişimi ModelState hatası nedeniyle başarısız oldu", DateTime.UtcNow);
            }
            else
            {
                // Diğer IActionResult türleri için genel bir loglama yapıyoruz
                Log.Warning("{DateTime} tarihinde beklenmedik bir sonuç döndürüldü: {ResultType}", DateTime.UtcNow, context.Result.GetType().Name);
            }
        }
    }
}
