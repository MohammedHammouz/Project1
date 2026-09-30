using System.Diagnostics;

namespace HSMAPI.Middleware
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;
        public ProfilingMiddleware(RequestDelegate next,ILogger<ProfilingMiddleware> logger)
        {
            //_next transfer to another middleware
            _next = next;
            //logger help us to detect code and problems by dubugging,tracing
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await  _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request :{context.Request.Path} took:{stopwatch.ElapsedMilliseconds}ms to execute");
        }
        //Task:تمثل بروسيس شغالة في مكان ما في البروسيسر وليس في المين ثريد
        //HttpContext :معها كل معلومات تاعت الريكويس والريسبونس
        //طالما عندي ميثود بترجع وتاسك وانا رح امل لوجيك بعدها دايما استناها الا لو انا عارف اني مش محتاج استناها طبعا ببعض حالات انا بحكي
    }
}
