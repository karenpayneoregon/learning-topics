using PromptFilesExamplesApp.Interfaces;

namespace PromptFilesExamplesApp.Models;

public class Address : IAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}