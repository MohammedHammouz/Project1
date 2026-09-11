using HSMBusiness.Features.MedicalServiceBills.Dtos;
using HSMDataAccess.Entities;

namespace HSMBusiness.Features.MedicalServiceBills.Mappers
{
    public static class MedicalServiceBillMapper
    {
        public static MedicalServiceBillDto ToDto(this MedicalServiceBill entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new MedicalServiceBillDto
            {
                ServiceId = entity.ServiceID,
                BillId = entity.BillID,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                TotalPrice = entity.TotalPrice
            };
        }

        public static List<MedicalServiceBillDto> ToDtos(this IEnumerable<MedicalServiceBill> entities)
            => entities.Select(e => e.ToDto()).ToList();
    }
}
