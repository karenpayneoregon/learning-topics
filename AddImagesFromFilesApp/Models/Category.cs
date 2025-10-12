namespace AddImagesFromFilesApp.Models;
public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[]? Photo { get; set; }
    public string FileName { get; set; } = string.Empty;
}