namespace HSMBusiness.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public sealed record CreateServiceCategoryCommand(
        string CategoryName,
        string? CategoryDescription);
}
