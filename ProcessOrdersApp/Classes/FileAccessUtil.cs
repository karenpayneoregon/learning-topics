using System.Text;

namespace ProcessOrdersApp.Classes;

/// <summary>
/// Provides utility methods for file access operations, such as checking read permissions,
/// reading text files, and handling file access asynchronously.
/// </summary>
/// <remarks>
/// This static class is designed to simplify common file access tasks, including determining
/// whether a file can be read and reading file content with error handling. It includes both
/// synchronous and asynchronous methods for flexibility in various scenarios.
/// </remarks>
public static class FileAccessUtil
{
    /// <summary>
    /// Determines whether the current user has permission to open a specified text file for reading.
    /// </summary>
    /// <param name="path">The full path of the text file to check.</param>
    /// <returns>
    /// <c>true</c> if the file can be opened for reading; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to open the specified file in read-only mode. If the operation fails
    /// due to any exception (e.g., file not found, access denied), the method returns <c>false</c>.
    /// </remarks>
    public static bool CanOpenTextFile(string path)
    {
        try
        {
            using var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the current user can open and read text from a specified file.
    /// </summary>
    /// <param name="path">The full path of the text file to be checked.</param>
    /// <param name="encoding">
    /// The character encoding to use for reading the file. If <c>null</c>, UTF-8 encoding is used by default.
    /// </param>
    /// <returns>
    /// <c>true</c> if the file can be opened and at least one line of text can be read; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to open the specified file and read its content using the provided or default encoding.
    /// If the operation fails due to any exception (e.g., file not found, access denied, or invalid encoding),
    /// the method returns <c>false</c>.
    /// </remarks>
    public static bool CanReadTextFile(string path, Encoding? encoding = null)
    {
        try
        {
            encoding ??= Encoding.UTF8;

            using var reader = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks: true);
            reader.ReadLine(); // force actual read
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to read a text file asynchronously and returns the success status along with an error message, if any.
    /// </summary>
    /// <param name="path">The full path of the text file to be read.</param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    /// A tuple containing a boolean indicating success or failure, and a string representing the error message if the operation fails.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="path"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="path"/> is invalid.</exception>
    /// <exception cref="IOException">Thrown if an I/O error occurs while accessing the file.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if the caller does not have the required permission.</exception>
    /// <remarks>
    /// This method attempts to open and read the specified text file asynchronously. If successful, it returns <c>true</c> 
    /// and a <c>null</c> error message. If an error occurs, it returns <c>false</c> and the corresponding error message.
    /// </remarks>
    public static async Task<(bool Success, string? Error)> TryReadTextFileAsync(string path, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            using var reader = new StreamReader(stream);
            await reader.ReadLineAsync(cancellationToken);

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}