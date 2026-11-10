namespace dz_4.Models;

public class CreateCategoryModel
{
    public Guid? ParentCategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}