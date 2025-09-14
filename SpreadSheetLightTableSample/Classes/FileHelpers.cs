using System.Runtime.InteropServices;

namespace SpreadSheetLightTableSample.Classes;

/// <summary>
/// Provides helper methods for file-related operations, such as checking file accessibility.
/// </summary>
internal class FileHelpers
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
    public static async Task<bool> CanReadFile(string fileName)
    {
        static bool IsFileLocked(Exception exception)
        {
            var errorCode = Marshal.GetHRForException(exception) & (1 << 16) - 1;
            return errorCode is ErrorSharingViolation or ErrorLockViolation;
        }

        try
        {
            await using var fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            fileStream.Close();
        }
        catch (IOException ex)
        {
            if (IsFileLocked(ex))
            {
                return false;
            }
        }

        await Task.Delay(50);

        return true;

    }
}
