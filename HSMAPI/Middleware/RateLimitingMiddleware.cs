using System.Diagnostics;

namespace HSMAPI.Middleware
{
    //مهمته هاد الميدلوير بحدد عدد الريكويست اللي بدي ابعتها مثلا خلال 10 ثواني
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _count = 0;
        private static DateTime _LastRequestDate = DateTime.Now;
        public RateLimitingMiddleware(RequestDelegate next)
        {
            //_next transfer to another middleware
            _next = next;
            
        }
        public async Task Invoke(HttpContext context)
        {
            _count++;
            //هون معناها لو فرق الثواني اكثر من 10 معناها صفر العداد
            if (DateTime.Now.Subtract(_LastRequestDate).Seconds > 10)
            {
                _count = 1;
                _LastRequestDate = DateTime.Now;
                await _next(context);
            }
            else
            {
                if (_count > 5)
                {
                    _LastRequestDate = DateTime.Now;
                   await context.Response.WriteAsync("Date limit exceeded");
                }
                else
                {
                    _LastRequestDate = DateTime.Now;
                    await _next(context);
                }
                
            }
        }
    }
}
