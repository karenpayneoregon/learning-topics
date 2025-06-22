using PromptFilesExamplesApp.Interfaces;

namespace PromptFilesExamplesApp.Models;

public class Person : IPerson
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
}