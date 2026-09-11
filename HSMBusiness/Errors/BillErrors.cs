using HSMBusiness.Common.Results;

namespace HSMBusiness.Errors
{
    public static class BillErrors
    {
        public static Error NotFound =>
            Error.NotFound("Bill_NotFound", "Bill was not found.");

        public static Error PatientIDRequired =>
            Error.Validation("Bill_PatientID_Required", "Patient is required.");

        public static Error PatientNotFound =>
            Error.NotFound("Bill_Patient_NotFound", "The specified patient does not exist.");

        public static Error StatusRequired =>
            Error.Validation("Bill_Status_Required", "Bill status is required.");

        public static Error InvoiceRequired =>
            Error.Validation("Bill_Invoice_Required", "Invoice number is required.");

        public static Error InvalidGrossAmount =>
            Error.Validation("Bill_GrossAmount_Invalid", "Gross amount must be greater than or equal to zero.");

        public static Error NoServicesAdded =>
            Error.Validation("Bill_Services_Required", "At least one service must be added to the bill.");

        public static Error AlreadyPaid =>
            Error.Conflict("Bill_AlreadyPaid", "Bill is already paid and cannot be modified.");
    }
}
