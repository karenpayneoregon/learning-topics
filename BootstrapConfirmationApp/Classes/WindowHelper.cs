using System.Diagnostics;
using System.Runtime.InteropServices;

// ReSharper disable SuggestVarOrType_Elsewhere
#pragma warning disable CA1806

namespace BootstrapConfirmationApp.Classes;

public static class WindowHelper
{

    /// <summary>
    /// Brings the specified process's main window to the foreground.
    /// </summary>
    /// <param name="process">
    /// The <see cref="Process"/> instance representing the process whose main window should be brought to the front.
    /// </param>
    /// <remarks>
    /// If the window is minimized, it will be restored before being brought to the foreground.
    /// </remarks>
    public static void BringProcessToFront(Process process)
    {
        IntPtr handle = process.MainWindowHandle;
        if (IsIconic(handle))
        {
            ShowWindow(handle, SwRestore);
        }

        SetForegroundWindow(handle);
    }

    /// <summary>
    /// <para>Locates the console window for a web application started with Visual Studio.</para>
    /// <para>If not in production environment bring the window to the front.</para>
    /// </summary>
    /// <param name="app">The application</param>
    /// <param name="title">Optional title for console window</param>
    public static void ShowConsoleWindow(this WebApplication app, string title = "")
    {
        if (!app.Environment.IsDevelopment()) return;


        Process[] processes = Process.GetProcesses();

        var consoleWindowTitle = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Concat(AppDomain.CurrentDomain.FriendlyName, ".exe"));

        foreach (var process in processes)
        {
            if (string.IsNullOrWhiteSpace(process.MainWindowTitle)) continue;
            if (process.MainWindowTitle == consoleWindowTitle)
            {
                BringProcessToFront(process);

                if (!string.IsNullOrWhiteSpace(title))
                {
                    SetWindowText(process.MainWindowHandle, title);
                }
                
                var hwnd = process.MainWindowHandle;
                // Move to top-left of the monitor, keep current size and z-order.
                SetWindowPos(
                    hwnd,
                    IntPtr.Zero,
                    0,
                    0,
                    0,
                    0,
                    SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
            }

        }
    }

    /// <summary>
    /// Sets the title of the console window for the specified web application.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance representing the application.</param>
    /// <param name="title">The desired title for the console window.</param>
    /// <remarks>
    /// This method updates the console window title only in development environments.
    /// It locates the console window associated with the application and sets its title to the specified value.
    /// </remarks>
    public static void SetConsoleWindowTitle(WebApplication app, string title)
    {
        if (!app.Environment.IsDevelopment()) return;

        Process[] processes = Process.GetProcesses();

        var consoleWindowTitle = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            string.Concat(AppDomain.CurrentDomain.FriendlyName, ".exe"));
        var p = processes.FirstOrDefault(x => x.MainWindowTitle == consoleWindowTitle);
        SetWindowText(p.MainWindowHandle, title);

    }

    /// <summary>
    /// Sets the console window title specifically for Windows 11 environments.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    /// <param name="title">The desired title for the console window.</param>
    /// <remarks>
    /// This method updates the console title for both classic console hosts and VT-aware hosts
    /// (e.g., Windows Terminal). It also handles scenarios where no console is attached, such as
    /// when running as a service or under IIS Express.
    /// </remarks>
    /// <exception cref="IOException">
    /// Thrown when no console is attached or output is redirected. The exception is caught and handled internally.
    /// </exception>
    public static void SetConsoleWindowTitleWindows11(this WebApplication app, string title)
    {
        if (!app.Environment.IsDevelopment()) return;

        try
        {
            // Works on classic conhost and Windows Terminal
            Console.Title = title;

            // Belt-and-suspenders for VT-aware hosts (Windows Terminal)
            Console.Write($"\e]0;{title}\a");
        }
        catch (IOException)
        {
            // No console is attached (e.g., service, IIS Express, or output redirected)
            // Swallow or log as needed.
        }
    }


    const int SwRestore = 9;

    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

    /// <summary>
    /// Brings the window with the specified handle to the foreground.
    /// </summary>
    /// <param name="handle">
    /// A pointer to the window handle (<see cref="IntPtr"/>) that should be brought to the foreground.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the window was successfully brought to the foreground; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method is a platform invocation (P/Invoke) of the native `SetForegroundWindow` function in the User32.dll.
    /// It is used to set the specified window as the foreground window.
    /// </remarks>
    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr handle);
    /// <summary>
    /// Displays or hides the specified window based on the given command.
    /// </summary>
    /// <param name="handle">A handle to the window.</param>
    /// <param name="nCmdShow">
    /// The command that specifies how the window is to be shown. 
    /// For example, use <c>9</c> to restore a minimized window.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the operation succeeds; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method is a P/Invoke declaration for the native <c>ShowWindow</c> function in the User32.dll library.
    /// It is used to control the visibility and state of a window.
    /// </remarks>
    [DllImport("User32.dll")]
    private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
    /// <summary>
    /// Determines whether the specified window is minimized (iconic).
    /// </summary>
    /// <param name="handle">A handle to the window.</param>
    /// <returns>
    /// <c>true</c> if the window is minimized; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method is a P/Invoke declaration for the native <c>IsIconic</c> function in User32.dll.
    /// </remarks>
    [DllImport("User32.dll")]
    private static extern bool IsIconic(IntPtr handle);
    /// <summary>
    /// Sets the text of the specified window's title bar.
    /// </summary>
    /// <param name="hWnd">
    /// A handle to the window or control whose text is to be changed.
    /// </param>
    /// <param name="text">
    /// The new title or control text to be set.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.
    /// </returns>
    /// <remarks>
    /// This method is a platform invocation (P/Invoke) of the native `SetWindowText` function 
    /// from the Windows API, which modifies the title of a window or control.
    /// </remarks>
    [DllImport("user32.dll")]
    static extern int SetWindowText(IntPtr hWnd, string text);
}
