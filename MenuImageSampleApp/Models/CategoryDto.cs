namespace MenuImageSampleApp.Models;

/// <summary>
/// Represents a data transfer object (DTO) for a category.
/// </summary>
/// <remarks>
/// This class is used to encapsulate category data retrieved from the database, 
/// including the category's identifier and name.
/// </remarks>
internal sealed class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}