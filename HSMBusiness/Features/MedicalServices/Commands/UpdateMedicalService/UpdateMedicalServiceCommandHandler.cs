using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalServices.Dtos;
using HSMBusiness.Features.MedicalServices.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalServices.Commands.UpdateMedicalService
{
    public class UpdateMedicalServiceCommandHandler(
        IValidator<UpdateMedicalServiceCommand> validator,
        MedicalServiceRepository medicalServiceRepository,
        ServicesCategoryRepository serviceCategoryRepository)
    {
        public async Task<Result<MedicalServiceDto>> ExecuteAsync(UpdateMedicalServiceCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalService = await medicalServiceRepository.GetByIDAsync(command.Id);
            if (medicalService is null)
            {
                return MedicalServiceErrors.NotFound;
            }

            var category = await serviceCategoryRepository.GetByIDAsync(command.CategoryId);
            if (category is null)
            {
                return MedicalServiceErrors.CategoryNotFound;
            }

            medicalService.ServiceName = command.ServiceName;
            medicalService.CategoryID = command.CategoryId;
            medicalService.DefaultPrice = command.DefaultPrice;
            medicalService.Description = command.Description;
            medicalService.DurationMinutes = command.DurationMinutes;

            await medicalServiceRepository.UpdateAsync(medicalService);

            return medicalService.ToDto();
        }
    }
}
