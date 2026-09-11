using HSMBusiness.Features.ServiceCategories.Commands.CreateServiceCategory;
using HSMBusiness.Features.ServiceCategories.Commands.DeleteServiceCategory;
using HSMBusiness.Features.ServiceCategories.Commands.UpdateServiceCategory;
using HSMBusiness.Features.ServiceCategories.Queries.GetServiceCategories;
using HSMBusiness.Features.ServiceCategories.Queries.GetServiceCategoryById;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/[controller]")]
    public class ServiceCategoryController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetServiceCategoriesQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetServiceCategoriesQuery());
            return result.Match(items => Ok(items), Problem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromServices] GetServiceCategoryByIdQueryHandler handler)
        {
            var result = await handler.ExecuteAsync(new GetServiceCategoryByIdQuery(id));
            return result.Match(item => Ok(item), Problem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceCategoryCommand command, [FromServices] CreateServiceCategoryCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(command);
            return result.Match(item => CreatedAtAction(nameof(GetById), new { id = item.Id }, item), Problem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateServiceCategoryCommand command, [FromServices] UpdateServiceCategoryCommandHandler handler)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await handler.ExecuteAsync(command);
            return result.Match(item => Ok(item), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] DeleteServiceCategoryCommandHandler handler)
        {
            var result = await handler.ExecuteAsync(new DeleteServiceCategoryCommand(id));
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
