
using System.Collections.Generic;
using System.Diagnostics;
using DeconstructSamplesApp.Models;

namespace DeconstructSamplesApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void PeopleDictionaryButton_Click(object sender, EventArgs e)
    {
        foreach (var (index, person) in PersonDictionary)
        {
            Debug.WriteLine($"{index,-4}" +
                            $"{person.FirstName, -12}" +
                            $"{person.LastName, -12}" +
                            $"{person.DateOfBirth, -14:MM/dd/yyyy}" +
                            $"{person.Gender}");
        }
    }

    private static readonly Dictionary<int, Person> PersonDictionary = Persons
        .Select((p, index) => new { Key = index + 1, Value = p })
        .ToDictionary(x => x.Key, x => x.Value);

    public static List<Person> Persons =>
    [
        new Person { Id = 1, FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateOnly(1985, 6, 12), Gender = Gender.Female },
        new Person { Id = 2, FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateOnly(1990, 3, 25), Gender = Gender.Male },
        new Person { Id = 3, FirstName = "Catherine", LastName = "Brown", DateOfBirth = new DateOnly(1978, 11, 2), Gender = Gender.Female },
        new Person { Id = 4, FirstName = "David", LastName = "Wilson", DateOfBirth = new DateOnly(2000, 7, 9), Gender = Gender.Male },
        new Person { Id = 5, FirstName = "Ella", LastName = "Taylor", DateOfBirth = new DateOnly(1995, 1, 14), Gender = Gender.Female }
    ];
}
