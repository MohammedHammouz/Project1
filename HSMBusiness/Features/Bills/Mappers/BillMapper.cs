using HSMBusiness.Features.Bills.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.Bills.Mappers
{
    public static class BillMapper
    {
        public static BillDto ToDto(this Bill entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new BillDto
            {
                Id = entity.ID,
                PatientId = entity.PatientID,
                Status = entity.Status,
                Date = entity.Date,
                PartialPaymentAmount = entity.PartialPaymentAmount,
                DueDate = entity.DueDate,
                Invoice = entity.Invoice,
                GrossAmount = entity.GrossAmount,
                Discount = entity.Discount,
                InsuranceCoverage = entity.InsuranceCoverage,
                PatientResponsibility = entity.PatientResponsibility
            };
        }

        public static List<BillDto> ToDtos(this IEnumerable<Bill> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
