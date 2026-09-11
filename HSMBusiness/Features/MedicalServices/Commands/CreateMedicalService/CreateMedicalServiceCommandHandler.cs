using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServices.Dtos;
using HSMBusiness.Features.MedicalServices.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServices.Commands.CreateMedicalService
{
    public class CreateMedicalServiceCommandHandler(
        IValidator<CreateMedicalServiceCommand> validator,
        MedicalServiceRepository medicalServiceRepository,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<MedicalServiceDto>> ExecuteAsync(CreateMedicalServiceCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var category = await serviceCategoryRepository.GetByIDAsync(command.CategoryId);
            if (category is null)
            {
                return MedicalServiceErrors.CategoryNotFound;
            }

            var medicalService = new MedicalService
            {
                ServiceName = command.ServiceName,
                CategoryID = command.CategoryId,
                DefaultPrice = command.DefaultPrice,
                Description = command.Description,
                DurationMinutes = command.DurationMinutes
            };

            var created = await medicalServiceRepository.AddAsync(medicalService);

            return created.ToDto();
        }
    }
}
