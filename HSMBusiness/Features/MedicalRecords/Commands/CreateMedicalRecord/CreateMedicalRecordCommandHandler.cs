using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalRecords.Dtos;
using HSMBusiness.Features.MedicalRecords.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.Entities;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalRecords.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandHandler(
        IValidator<CreateMedicalRecordCommand> validator,
        MedicalRecordRepository medicalRecordRepository,
        PatientRepository patientRepository)
    {
        public async Task<Result<MedicalRecordDto>> ExecuteAsync(CreateMedicalRecordCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return MedicalRecordErrors.PatientNotFound;
            }

            var medicalRecord = new MedicalRecord
            {
                PatientID = command.PatientId,
                Diagnosis = command.Diagnosis,
                Treatment = command.Treatment,
                Prescriptions = command.Prescriptions,
                Status = command.Status,
                AuditTrail = command.AuditTrail,
                AccessLevel = command.AccessLevel
            };

            var created = await medicalRecordRepository.AddAsync(medicalRecord);

            return created.ToDto();
        }
    }
}
