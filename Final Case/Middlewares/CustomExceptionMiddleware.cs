using System;
using System.Diagnostics;
using System.Net;
using Final.Data.LoggerService;
using Newtonsoft.Json;

namespace Final_Case.Middlewares
{
	public class CustomExceptionMiddleware
	{
        private readonly RequestDelegate next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            this.next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            //ne kadar sürede çalıştığını göremek için timer yaptık.
            var watch = Stopwatch.StartNew();

            try
            {
                //Request
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message, context.Response.StatusCode);

                //request bitti ve buraya düştü. yani işlem bittikten sonra response kısmı bundan sonra çalışır.
                await next(context);

                //timerı durdurduk.
                watch.Stop();

                //Response
                //biz burada ayrıca birde ne kadar sürede requesten response geldi bunda görmek istiyoruz.
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message, context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                //timerı durdurduk.
                watch.Stop();

                //Hatalarımızı tek bir yerden kontrol etmek istiyoruz.
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            //burada biz istediğimiz gibi hata türüne bakarak yönetibiliriz.
            context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  //biz sadece 500 döndük.

            string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds;
            _loggerService.Write(message, context.Response.StatusCode);

            //Burada json türünde biz geri dönüş yapacağımız için =>"Newtonsoft.Json" kütüphanesini indirdik.
            //yani jsona çevirdik.
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            //contexte yazıp geri döndürdük.
            return context.Response.WriteAsync(result);

            //şimdi direk biz diğer taraftan throw(hata) gönderirsek direk üst taraf yakalacak ve buraya yönlendirip bu işlemleri yapacak.
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}

