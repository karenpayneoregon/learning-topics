using ConsoleNet11CodeApp1.Classes.Core;
using ConsoleNet11CodeApp1.Models;
using Serilog;
using Spectre.Console;

namespace ConsoleNet11CodeApp1;

internal partial class Program
{
    static void Main(string[] args)
    {
        EmployeeOrManager employee = new Employee("John", "Doe");
        EmployeeOrManager manager = new Manager("Jane", "Smith", "Sales");
        
        AnsiConsole.MarkupLine($"[green bold]Employee:[/] {employee.FirstName} {employee.LastName}");
        AnsiConsole.MarkupLine($"[green bold]Manager:[/] {manager.FirstName} {manager.LastName} ({((Manager)manager).Department})");
        ClosedHierarchyProcessSensorsAndReports();
        DisplayWithIndex();
        UnionHandleProductLookup();
        ManualUnionExample();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static void ClosedHierarchyProcessSensorsAndReports()
    {

        SpectreConsoleHelpers.PrintPink();
        
        // Closed hierarchy with an exhaustive switch.
        Sensor[] sensors =
        [
            new Temperature(21.4),
            new Humidity(55),
            new Contact(Open: true),
        ];
        foreach (Sensor sensor in sensors)
        {
            AnsiConsole.MarkupLine($"[green bold]Sensor:[/] {Describe(sensor)}");
        }

        // A DoorContact from another assembly still matches the Contact case.
        Sensor frontDoor = new DoorContact(Open: false, Door: "Front");
        AnsiConsole.MarkupLine($"[green bold]Sensor:[/] {Describe(frontDoor)}");

        // Generic closed hierarchy.
        Report<string> report = new Group<string>(new Single<string>("Kitchen"),
            new Group<string>(new Single<string>("Garage"), new Single<string>("Attic")));
        
        AnsiConsole.MarkupLine($"[green bold]Report leaves:[/] {Count(report)}");
    }

    /// <summary>
    /// Handles the lookup of a product and displays the result in the console.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the use of a union type <see cref="ProductLookup"/> to handle
    /// different outcomes of a product lookup operation. It uses pattern matching to determine
    /// whether a product was found or not and displays an appropriate message using Spectre.Console.
    /// </remarks>
    private static void UnionHandleProductLookup()
    {
        
        SpectreConsoleHelpers.PrintPink();
        
        ProductLookup result = new Product(42, "Mechanical keyboard");

        var message = result switch
        {
            Product product => $"Found {product.Name}",
            ProductNotFound missing => $"No product with ID {missing.Id}",
        };

        AnsiConsole.MarkupLine($"[green bold]{message}[/]");
    }

    /// <summary>
    /// Demonstrates the use of a manual union type to calculate the area of a shape
    /// using pattern matching and a closed hierarchy of shape types.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="Shape"/> struct, which represents a union of 
    /// different shape types, such as <see cref="Circle"/> and <see cref="Rectangle"/>.
    /// The area of the shape is calculated based on its specific type using a <c>switch</c> expression.
    /// The result is displayed in the console using Spectre.Console.
    /// </remarks>
    /// <example>
    /// Example output:
    /// <code>
    /// 78.54
    /// </code>
    /// </example>
    private static void ManualUnionExample()
    {

        SpectreConsoleHelpers.PrintPink();
        
        Shape shape = new(new Circle(5.0));

        var area = shape switch
        {
            Circle c => Math.PI * c.Radius * c.Radius,
            Rectangle r => r.Width * r.Height,
        };

        AnsiConsole.MarkupLine($"[green bold]{area:F2}[/]"); // output: 78.54
        
    }
    /// <summary>
    /// Displays a list of tags along with their corresponding indices in the console.
    /// </summary>
    /// <remarks>
    /// The method uses a <see cref="HashSet{T}"/> to store tags with case-insensitive comparison
    /// and iterates through the tags using their indices. The output is formatted with colors
    /// using the Spectre.Console library.
    /// </remarks>
    private static void DisplayWithIndex()
    {

        SpectreConsoleHelpers.PrintPink();

        //the comparer controls equality, so "Alice" and "ALICE" are one entry.
        HashSet<string> names = [with(StringComparer.OrdinalIgnoreCase), "Alice", "ALICE", "Bob"];
        
        HashSet<string> tags = [with(StringComparer.OrdinalIgnoreCase), "one", "two", "three"];

        foreach (var (index, tag) in tags.Index())
        {
            AnsiConsole.MarkupLine($"[green]{index}[/]: [cyan]{tag}[/]");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine($"[green]Names: [/][cyan]{string.Join(", ", names)}[/]");


    }

    public static int Count<T>(Report<T> report) => report switch
    {
        Single<T> => 1,
        Group<T> group => Count(group.Left) + Count(group.Right),
        // Exhaustive: Report<T> is closed and both subtypes are handled.
    };

    public static string Describe(Sensor sensor) => sensor switch
    {
        Temperature temperature => $"{temperature.Celsius:F1}°C",
        Humidity humidity => $"{humidity.Percent:F0}% RH",
        Contact contact => contact.Open ? "open" : "closed",
        // No default arm is needed. Sensor is closed, so these cases are exhaustive.
    };
}


public sealed record Product(int Id, string Name);
public sealed record ProductNotFound(int Id);

public union ProductLookup(Product, ProductNotFound);

// This type lives in a different assembly than the closed Sensor hierarchy.
// It can't derive from Sensor directly, but it can extend the unsealed Contact
// leaf. A DoorContact still matches the 'Contact' case, so switches over Sensor
// stay exhaustive.
public sealed record class DoorContact(bool Open, string Door) : Contact(Open);

// A closed class restricts its direct subtypes to this assembly. A 'closed'
// class is implicitly abstract.
public closed record class Sensor;

// Seal the cases whose shape is final.
public sealed record class Temperature(double Celsius) : Sensor;
public sealed record class Humidity(double Percent) : Sensor;

// Leave a case unsealed as an "escape hatch" so other assemblies can specialize it.
public record class Contact(bool Open) : Sensor;

// A generic closed hierarchy. Every type parameter of a derived type must appear
// in the base type, so a single derived construction exhausts each Report<T>.
public closed record class Report<T>;
public sealed record class Single<T>(T Value) : Report<T>;
public sealed record class Group<T>(Report<T> Left, Report<T> Right) : Report<T>;



public class Employee(string firstName, string lastName)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public bool IsManager { get; set; } = false;
}
public  class Manager(string firstName, string lastName, string department)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Department { get; set; } = department;
    public bool IsManager { get; set; } = true;
}

public union EmployeeOrManager(Employee, Manager)
{
    public bool IsManager => this switch
    {
        Employee employee => employee.IsManager,
        Manager manager => manager.IsManager,
        null => false,
    };
}
