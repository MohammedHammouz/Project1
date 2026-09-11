namespace HSMBusiness.Features.ServiceCategories.Dtos
{
    public class ServiceCategoryDto
    {
        public string Id { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
    }
}
