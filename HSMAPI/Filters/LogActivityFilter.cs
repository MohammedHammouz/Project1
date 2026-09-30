using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace HSMAPI.Filters
{
    public class LogActivityFilter:IActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;
        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // هذا الكود ينفذ قبل الـ Action Method
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // هذا الكود ينفذ بعد انتهاء الـ Action Method
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} finished on controller {context.Controller}");
        }
    }
    //هاد فلتر بسيط لما اجي انادي اي 
    //API
    //يقلي انه هاي ال 
    //API
    //تنفذت في الوقت الفلاني
    //ورجعت 
    //Result
    //نوعها كذا
    //وال
    //parameters 
    //اللي كانت داخلتلها كانت شكلها كذا
    //انا بدي اعمل 
    //log
    //لل
    //Action method  ,controller name,parameters
}
