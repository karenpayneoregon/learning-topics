using Serilog;
using Spectre.Console;

namespace TreesExamples.Classes;

public static class DirectoryTree
{
    /// <summary>
    /// Displays a directory tree structure in the console.
    /// </summary>
    /// <param name="path">The path of the directory to display.</param>
    /// <remarks>
    /// If the specified directory does not exist, an error message is displayed.
    /// The method uses Spectre.Console to render the directory tree in a visually appealing format.
    /// </remarks>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when access to a directory is denied. This is handled internally and logged.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while accessing a directory. This is handled internally and logged.
    /// </exception>
    public static void Display(string path)
    {
        if (!Directory.Exists(path))
        {
            AnsiConsole.MarkupLine("[red]Directory does not exist.[/]");
            return;
        }

        DirectoryInfo root = new(path);

        Tree tree = new($"[bold yellow]{Markup.Escape(root.FullName)}[/]");

        AddDirectory(root, tree);

        AnsiConsole.Write(tree);
    }

    /// <summary>
    /// Recursively adds directories and files to a tree structure for display.
    /// </summary>
    /// <param name="directory">The directory to process and add to the tree.</param>
    /// <param name="parent">The parent tree node to which the directory and its contents will be added.</param>
    /// <remarks>
    /// This method processes the specified directory, adding its subdirectories and files to the tree structure.
    /// Directories named "bin" or "obj" are collapsed by default.
    /// Unauthorized access and I/O exceptions are handled internally and logged using Serilog.
    /// </remarks>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when access to a directory is denied. This exception is caught and logged internally.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while accessing a directory. This exception is caught and logged internally.
    /// </exception>
    private static void AddDirectory(DirectoryInfo directory, IHasTreeNodes parent)
    {
        bool isBinObj = directory.Name.Equals("obj", StringComparison.OrdinalIgnoreCase) || 
                     directory.Name.Equals("bin", StringComparison.OrdinalIgnoreCase);
        try
        {
            foreach (DirectoryInfo subdirectory in directory.GetDirectories().OrderBy(d => d.Name))
            {
                var node = parent.AddNode($"[blue]📁 {Markup.Escape(subdirectory.Name)}[/]");

                AddDirectory(subdirectory, node);
                if (isBinObj)
                {
                    node.Collapse();
                }
            }

            if (!isBinObj)
            {
                foreach (FileInfo file in directory.GetFiles().OrderBy(f => f.Name))
                {
                    parent.AddNode($"[green]📄 {Markup.Escape(file.Name)}[/]");
                }

            }


        }
        catch (UnauthorizedAccessException uae)
        {
            parent.AddNode("[red]Access denied[/]");
            Log.Error(uae, "Access denied to directory: {Directory}", directory.FullName);
        }
        catch (IOException ex)
        {
            parent.AddNode($"[red]{Markup.Escape(ex.Message)}[/]");
            Log.Error(ex, "IO exception occurred while accessing directory: {Directory}", directory.FullName);
        }
    }
}