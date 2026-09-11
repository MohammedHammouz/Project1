using HSMBusiness.Features.Notifications.Commands.CreateNotification;
using HSMBusiness.Features.Notifications.Commands.DeleteNotification;
using HSMBusiness.Features.Notifications.Commands.UpdateNotification;
using HSMBusiness.Features.Notifications.Queries.GetNotificationById;
using HSMBusiness.Features.Notifications.Queries.GetNotifications;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetNotificationsQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetNotificationsQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetNotificationByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetNotificationByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationCommand command, [FromServices] CreateNotificationCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateNotificationCommand command, [FromServices] UpdateNotificationCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteNotificationCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteNotificationCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
