using AddImagesFromFilesApp.Models;

namespace AddImagesFromFilesApp.Classes;

public static class CategoryHelper
{
    /// <summary>
    /// Loads categories from image files located in the specified folder path.
    /// </summary>
    /// <param name="folderPath">
    /// The path to the folder containing image files to be processed.
    /// </param>
    /// <returns>
    /// A list of <see cref="Category"/> objects, each representing a category derived from an image file.
    /// </returns>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the specified folder path does not exist.
    /// </exception>
    public static List<Category> LoadCategoriesFromImages(string folderPath)
    {
        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

        var categories = new List<Category>();
        var imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

        foreach (var filePath in imageFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext is ".png" or ".jpg" or ".jpeg")
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                var fileName = Path.GetFileName(filePath);
                var categoryName = Path.GetFileNameWithoutExtension(filePath);

                categories.Add(new Category
                {
                    CategoryName = categoryName,
                    Description = $"Image for {categoryName}",
                    Photo = imageBytes,
                    FileName = fileName
                });
            }
        }

        return categories;
    }
}