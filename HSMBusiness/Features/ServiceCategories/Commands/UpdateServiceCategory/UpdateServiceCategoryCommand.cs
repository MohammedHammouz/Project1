namespace HSMBusiness.Features.ServiceCategories.Commands.UpdateServiceCategory
{
    public sealed record UpdateServiceCategoryCommand(
        string Id,
        string CategoryName,
        string? CategoryDescription);
}
