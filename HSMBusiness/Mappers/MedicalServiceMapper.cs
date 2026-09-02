using HSMDataAccess.DTOs;
using HSMDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class MedicalServiceMapper
    {
        public enum enMode { Add, Update }

        public MedicalService ToEntity(
            MedicalServiceDTO medicalServiceDTO,
            enMode mode = enMode.Add)
        {
            if (mode == enMode.Add)
            {
                return new MedicalService
                {
                    ID = medicalServiceDTO.ID,
                    ServiceName = medicalServiceDTO.ServiceName,
                    CategoryID = medicalServiceDTO.CategoryID,
                    DefaultPrice = medicalServiceDTO.DefaultPrice,
                    Description = medicalServiceDTO.Description,
                    DurationMinutes = medicalServiceDTO.DurationMinutes
                };
            }
            else
            {
                return new MedicalService
                {
                    ID = medicalServiceDTO.ID,
                    ServiceName = medicalServiceDTO.ServiceName,
                    CategoryID = medicalServiceDTO.CategoryID,
                    DefaultPrice = medicalServiceDTO.DefaultPrice,
                    Description = medicalServiceDTO.Description,
                    DurationMinutes = medicalServiceDTO.DurationMinutes
                };
            }
        }

        public MedicalServiceDTO ToDTO(MedicalService medicalService)
        {
            return new MedicalServiceDTO
            (
                medicalService.ID,
                medicalService.ServiceName,
                medicalService.CategoryID,
                medicalService.DefaultPrice,
                medicalService.Description,
                medicalService.DurationMinutes
            );
        }
    }
}
