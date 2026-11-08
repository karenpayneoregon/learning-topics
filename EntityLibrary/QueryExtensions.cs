using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace EntityLibrary;

/// <summary>
/// Provides extension methods for enhancing and debugging Entity Framework Core queries.
/// </summary>
public static class QueryExtensions
{
    /// <summary>
    /// Adds a debug tag to the query, providing information about the calling method, file, and line number.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="IQueryable{T}"/>.</typeparam>
    /// <param name="query">The query to which the debug tag will be added.</param>
    /// <param name="message">An optional custom message to include in the debug tag.</param>
    /// <param name="memberName">The name of the calling method. This is automatically populated by the compiler.</param>
    /// <param name="filePath">The file path of the calling code. This is automatically populated by the compiler.</param>
    /// <param name="lineNumber">The line number in the calling file. This is automatically populated by the compiler.</param>
    /// <returns>
    /// The original query with a debug tag added in debug builds, or the original query unchanged in release builds.
    /// </returns>
    public static IQueryable<T> TagWithDebugInfo<T>(this IQueryable<T> query,
        string message = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
#if DEBUG
        var path = Path.GetFileName(filePath);
        return query.TagWith(string.IsNullOrWhiteSpace(message)
            ? $"Executing method {memberName} in {path} at line {lineNumber}"
            : $"Executing method {memberName} in {path} at line {lineNumber} message {message}");
#else
        return query;
#endif

    }

}