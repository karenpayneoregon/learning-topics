using ConsoleConfigurationLibrary.Classes;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Serilog;

namespace MenuImageSampleApp.Classes;

public sealed class CategoryPhotoExporter
{
    /// <summary>
    /// Exports dbo.Categories.Photo for the given Id to a file named "{Name}{Ext}" in the outputDirectory.
    /// </summary>
    public static string ExportPhotoToFile(int categoryId, string outputDirectory)
    {
        if (string.IsNullOrWhiteSpace(outputDirectory))
            throw new ArgumentException("Output directory is required.", nameof(outputDirectory));

        Directory.CreateDirectory(outputDirectory);

        var dto = GetCategoryPhoto(categoryId);

        if (dto is null)
        {
            throw new InvalidOperationException($"Category Id {categoryId} not found.");
        }

        if (dto.Photo is null || dto.Photo.Length == 0)
        {
            throw new InvalidOperationException($"Category Id {categoryId} has no photo data.");
        }

        // Ext in your DB looks like ".png" (keep that convention)
        var ext = string.IsNullOrWhiteSpace(dto.Ext) ? ".bin" : dto.Ext.Trim();
        if (!ext.StartsWith(".", StringComparison.Ordinal))
        {
            ext = $".{ext}";
        }

        var fullPath = string.Empty;
        
        try
        {
            var safeName = MakeSafeFileName(dto.Name ?? $"Category_{categoryId}");
            fullPath = Path.Combine(outputDirectory, $"{safeName}{ext}");

            File.WriteAllBytes(fullPath, dto.Photo);
        }
        catch (Exception e)
        {
            Log.Error(e,"Error exporting photo for category id {CategoryId}", categoryId);
        }
        
        return fullPath;
    }

    /// <summary>
    /// Retrieves the photo data, file extension, and name for a specific category from the database.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category whose photo data is to be retrieved.</param>
    /// <returns>
    /// A <see cref="CategoryPhotoDto"/> object containing the photo data, file extension, and name of the category,
    /// or <c>null</c> if no matching category is found.
    /// </returns>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue executing the SQL query against the database.
    /// </exception>
    /// <remarks>
    /// This method queries the database to fetch the photo-related information for a category.
    /// It is used internally by the <see cref="ExportPhotoToFile"/> method to facilitate the photo export process.
    /// </remarks>
    private static CategoryPhotoDto? GetCategoryPhoto(int categoryId)
    {
        const string sql = """
                           SELECT Name, Ext, Photo
                           FROM dbo.Categories
                           WHERE Id = @Id;
                           """;

        using IDbConnection cn = new SqlConnection(AppConnections.Instance.MainConnection);
        return cn.QuerySingleOrDefault<CategoryPhotoDto>(sql, new { Id = categoryId });
    }

    /// <summary>
    /// Converts the input string into a safe file name by replacing invalid characters
    /// and trimming trailing dots or spaces.
    /// </summary>
    /// <param name="input">The original string to be sanitized for use as a file name.</param>
    /// <returns>A sanitized string that is safe to use as a file name.</returns>
    /// <remarks>
    /// This method ensures that the resulting file name does not contain characters
    /// that are invalid for file names on the current file system. Additionally, it
    /// removes trailing dots and spaces to comply with Windows file system restrictions.
    /// </remarks>
    private static string MakeSafeFileName(string input)
    {
        input = Path.GetInvalidFileNameChars().Aggregate(input, (current, c) => current.Replace(c, '_'));

        // Also avoid trailing dots/spaces (Windows)
        return input.Trim().TrimEnd('.');
    }

    /// <summary>
    /// Represents a Data Transfer Object (DTO) for category photo data retrieved from the database.
    /// </summary>
    /// <remarks>
    /// This class encapsulates the properties of a category photo, including its name, file extension, 
    /// and binary photo data, which are used during the export process.
    /// </remarks>
    private sealed class CategoryPhotoDto
    {
        public string? Name { get; init; }
        public string? Ext { get; init; }
        public byte[]? Photo { get; init; }
    }
}