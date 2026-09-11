using FluentValidation;
using HSMBusiness.Common;
using HSMBusiness.Errors;
using HSMBusiness.Features.MedicalRecords.Dtos;
using HSMBusiness.Features.MedicalRecords.Mappers;
using HSMBusiness.Common.Results;
using HSMDataAccess.RepositoryServices;

namespace HSMBusiness.Features.MedicalRecords.Commands.UpdateMedicalRecord
{
    public class UpdateMedicalRecordCommandHandler(
        IValidator<UpdateMedicalRecordCommand> validator,
        MedicalRecordRepository medicalRecordRepository,
        PatientRepository patientRepository)
    {
        public async Task<Result<MedicalRecordDto>> ExecuteAsync(UpdateMedicalRecordCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors();
            }

            var medicalRecord = await medicalRecordRepository.GetByIDAsync(command.Id);
            if (medicalRecord is null)
            {
                return MedicalRecordErrors.NotFound;
            }

            var patient = await patientRepository.GetByIDAsync(command.PatientId);
            if (patient is null)
            {
                return MedicalRecordErrors.PatientNotFound;
            }

            medicalRecord.PatientID = command.PatientId;
            medicalRecord.Diagnosis = command.Diagnosis;
            medicalRecord.Treatment = command.Treatment;
            medicalRecord.Prescriptions = command.Prescriptions;
            medicalRecord.Status = command.Status;
            medicalRecord.AuditTrail = command.AuditTrail;
            medicalRecord.AccessLevel = command.AccessLevel;

            await medicalRecordRepository.UpdateAsync(medicalRecord);

            return medicalRecord.ToDto();
        }
    }
}
