using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Patients.Dtos;
using HSMBusiness.Features.Patients.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler(
        IValidator<UpdatePatientCommand> validator,
        PatientRepository patientRepository,
        PersonRepository personRepository)
    {
        public async Task<Result<PatientDto>> ExecuteAsync(UpdatePatientCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var patient = await patientRepository.GetByIDAsync(command.Id);
            if (patient is null)
            {
                return PatientErrors.NotFound;
            }

            var person = await personRepository.GetByIDAsync(command.PersonId);
            if (person is null)
            {
                return PatientErrors.PersonNotFound;
            }

            patient.MedicalHistory = command.MedicalHistory;
            patient.Status = command.Status;
            patient.PersonID = command.PersonId;

            await patientRepository.UpdateAsync(patient);

            return patient.ToDto();
        }
    }
}
