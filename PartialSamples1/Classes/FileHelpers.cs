using System.Runtime.InteropServices;

namespace PartialSamples1.Classes;

/// <summary>
/// Provides helper methods for file-related operations, such as checking file accessibility.
/// </summary>
public partial class FileHelpers
{

    const int ErrorSharingViolation = 32;
    const int ErrorLockViolation = 33;

    /// <summary>
    /// Determines whether a specified file can be read by attempting to open it with exclusive access.
    /// </summary>
    /// <param name="fileName">The full path of the file to check for readability.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains:
    /// <c>true</c> if the file can be read; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method attempts to open the file with exclusive access to determine if it is locked or accessible.
    /// If the file is locked due to sharing or locking violations, the method will return <c>false</c>.
    /// </remarks>
    public static partial bool CanReadFile(string fileName);

}
    