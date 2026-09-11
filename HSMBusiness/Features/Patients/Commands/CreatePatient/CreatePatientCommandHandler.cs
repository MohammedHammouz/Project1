using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.Patients.Dtos;
using HSMBusiness.Features.Patients.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommandHandler(
        IValidator<CreatePatientCommand> validator,
        PatientRepository patientRepository,
        PersonRepository personRepository)
    {
        public async Task<Result<PatientDto>> ExecuteAsync(CreatePatientCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var person = await personRepository.GetByIDAsync(command.PersonId);
            if (person is null)
            {
                return PatientErrors.PersonNotFound;
            }

            var patient = new Patient
            {
                MedicalHistory = command.MedicalHistory,
                Status = command.Status,
                PersonID = command.PersonId
            };

            var created = await patientRepository.AddAsync(patient);

            return created.ToDto();
        }
    }
}
