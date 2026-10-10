using HSMBusiness.Erorrs;
using HSMBusiness.Pattern;
using HSMBusiness.Services;
using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HSMAPI.Controllers
{
    [Route("api/ServiceCategoryController")]
    [ApiController]
    public class ServiceCategoryController(ServicesCategoryService servicesCategoryService) : ControllerBase
    {
        [HttpGet]
        [Route("{Id}", Name = "GetServiceCategoryByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceCategoryByIdAsync(string Id)
        {
            var appointment = await servicesCategoryService.GetByID(Id);
            if (Id == "")
            {
                return BadRequest($"{appointment.Status},{appointment.Response},{appointment.IsSuccess}");
            }
            if (appointment.servicesCategoryDTO == null)
            {
                return NotFound($"{appointment.Response},{appointment.Response}");
            }

            return Ok($"{appointment.Response},{appointment.Response}");
        }
        [HttpGet]
        [Route("All", Name = "GetAllServiceCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllServiceCategories()
        {
            var serviceCategoriesList = await servicesCategoryService.GetAll();
            if (serviceCategoriesList.Item4 == null)
            {
                return NotFound($"{serviceCategoriesList.Response},{serviceCategoriesList.Response}");
            }
            return Ok(new
            {
                status = serviceCategoriesList.Status,
                response = serviceCategoriesList.Response,
                serviceCategories = serviceCategoriesList.ListOfServiceCategories
            });
        }
        [HttpPost]
        [Route("Create", Name = "CreateServiceCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateServiceCategory(ServicesCategoryDTO serviceCategory)
        {
            if (string.IsNullOrWhiteSpace(serviceCategory.CategoryName))
            {
                    var response = await new ResultPatern().GiveResponse(400, null);
                    return BadRequest(new
                    {
                        status = response.Status,
                        response = response.Response,
                        isSuccess = response.IsSuccess,
                        errors = response.Errors
                    });
                
            }

            servicesCategoryService._mode = ServicesCategoryService.enMode.Add;
            var result = await servicesCategoryService.Save(serviceCategory, serviceCategory.ID);

            if (!result.IsSuccess)
            {
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            }
            return Ok(CreatedAtRoute("GetServiceCategoryByIdAsync", new { id = serviceCategory.ID }, serviceCategory));

        }
        [HttpPut]
        [Route("{ID}", Name = "UpdateServiceCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateServiceCategory(string ID, ServicesCategoryDTO serviceCategory)
        {
            if (string.IsNullOrEmpty(ID))
            {
               
                var response = await new ResultPatern().GiveResponse(400, null);
                return BadRequest(new
                {
                    status = response.Status,
                    response = response.Response,
                    isSuccess = response.IsSuccess
                });
            }
            var CurrServiceCategory = await servicesCategoryService.GetByID(ID);
            if (CurrServiceCategory.servicesCategoryDTO== null)
            {

                return NotFound(new
                {
                    status = CurrServiceCategory.Status,
                    response = CurrServiceCategory.Response,
                    isSuccess = CurrServiceCategory.IsSuccess
                });
            }
            if (string.IsNullOrWhiteSpace(serviceCategory.CategoryName))
            {
                
                    var response = await new ResultPatern().GiveResponse(400, null);
                    return BadRequest(new
                    {
                        status = response.Status,
                        response = response.Response,
                        isSuccess = response.IsSuccess,
                        
                    });
                
            }

            servicesCategoryService._mode = ServicesCategoryService.enMode.Update;
            var result = await servicesCategoryService.Save(serviceCategory, ID);

            if (!result.IsSuccess)
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });
            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess
            });
        }

        [HttpDelete]
        [Route("Delete/{id}", Name = "DeleteServiceCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteServiceCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID is empty or not found");

            var result = await servicesCategoryService.Delete(id);

            if (!result.IsSuccess)
                return StatusCode(result.Status, new
                {
                    status = result.Status,
                    response = result.Response,
                    isSuccess = result.IsSuccess
                });

            return Ok(new
            {
                status = result.Status,
                response = result.Response,
                isSuccess = result.IsSuccess
            });
        }
    }
}
