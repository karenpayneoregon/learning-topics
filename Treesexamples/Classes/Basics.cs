using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace TreesExamples.Classes
{
    internal class Basics
    {
        /// <summary>
        /// Demonstrates the creation and rendering of a tree structure using the Spectre.Console library.
        /// </summary>
        /// <remarks>
        /// The method constructs a tree with a root node and several child and grandchild nodes, 
        /// representing types and collections. It also includes a footer panel. The tree is then 
        /// rendered to the console using Spectre.Console.
        /// </remarks>
        public static void Example1()
        {
            var tree = new Tree("[deeppink3 bold]root node[/]")
                .Guide(TreeGuide.Line);

            var types = tree.AddNode("[yellow]Types[/] [dim]Child 1[/]");
            types.AddNode("[green]String[/] [dim]Grandchild 1.1[/]");
            types.AddNode("[green]Int32[/] [dim]Grandchild 1.2[/]");
            types.AddNode("[green]Boolean[/] [dim]Grandchild 1.3[/]");

            var collection = tree.AddNode("[blue]Collections[/] [dim]Child 1[/]");
            collection.AddNode("[green]List<String>[/] [dim]Grandchild 1.1[/]");
            collection.AddNode("[green]Dictionary<String,Int32>[/] [dim]Grandchild 1.2[/]");

            var footer = new Panel("[bold]Root Footer[/]")
                .BorderColor(Color.Grey)
                .Padding(1, 0);
            tree.AddNode(footer);
            
            AnsiConsole.Write(tree);
        }

        /// <summary>
        /// Demonstrates the creation and rendering of a hierarchical tree structure 
        /// representing an application file system using the Spectre.Console library.
        /// </summary>
        /// <remarks>
        /// This method constructs a tree with folders as parent nodes and their respective 
        /// files as child nodes. The tree is then rendered to the console using Spectre.Console.
        /// </remarks>
        public static void Example2()
        {
            var fileSystem = new Dictionary<string, string[]>
            {
                ["Configuration"] = ["appsettings.json", "secrets.json"],
                ["Source Code"] = ["Main.cs", "Helper.cs", "Utilities.cs"],
                ["Documentation"] = ["README.md", "CHANGELOG.md"],
            };

            var tree = new Tree("[green bold]Application[/]");

            foreach (var (folder, files) in fileSystem)
            {
                
                var folderNode = tree.AddNode($"[yellow]{folder}[/]");
                
                foreach (var file in files)
                {
                    folderNode.AddNode($"[grey]{file}[/]");
                }
                
            }

            AnsiConsole.Write(tree);
        }
    }
}
