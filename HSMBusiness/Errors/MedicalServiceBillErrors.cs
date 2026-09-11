using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class MedicalServiceBillErrors
    {
        public static Error NotFound =>
            Error.NotFound("MedicalServiceBill_NotFound", "Bill service was not found.");

        public static Error ServiceIDRequired =>
            Error.Validation("MedicalServiceBill_ServiceID_Required", "Medical service is required.");

        public static Error ServiceNotFound =>
            Error.NotFound("MedicalServiceBill_Service_NotFound", "The specified medical service does not exist.");

        public static Error BillIDRequired =>
            Error.Validation("MedicalServiceBill_BillID_Required", "Bill is required.");

        public static Error BillNotFound =>
            Error.NotFound("MedicalServiceBill_Bill_NotFound", "The specified bill does not exist.");

        public static Error InvalidQuantity =>
            Error.Validation("MedicalServiceBill_Quantity_Invalid", "Quantity must be greater than zero.");

        public static Error InvalidUnitPrice =>
            Error.Validation("MedicalServiceBill_UnitPrice_Invalid", "Unit price must be greater than or equal to zero.");
    }
}
