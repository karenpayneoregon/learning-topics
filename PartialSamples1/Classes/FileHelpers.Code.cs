using System.Runtime.InteropServices;

namespace PartialSamples1.Classes;
public partial class FileHelpers
{
    public static partial bool CanReadFile(string fileName)
    {
        try
        {
            using var fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            fileStream?.Close();
        }
        catch (IOException ex)
        {
            if (IsFileLocked(ex))
            {
                return false;
            }
        }

        return true;

        static bool IsFileLocked(Exception exception)
        {
            var errorCode = Marshal.GetHRForException(exception) & (1 << 16) - 1;
            return errorCode is ErrorSharingViolation or ErrorLockViolation;
        }
    }
}
