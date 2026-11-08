namespace EnvironmentApplication.Classes;

public class PageHelpers
{
    /// <summary>
    /// Retrieves the name of the current page based on the provided HTTP request.
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/> object representing the current HTTP request. Cannot be <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="string"/> representing the name of the current page. 
    /// Returns "Index" if the request path is the root ("/"), otherwise returns the file name without its extension.
    /// </returns>
    public static string GetCurrentPageName(HttpRequest request)
    {
        string path = request.Path;
        return path == "/" ? "Index" : Path.GetFileNameWithoutExtension(path);
    }
}