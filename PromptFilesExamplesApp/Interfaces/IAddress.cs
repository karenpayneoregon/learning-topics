namespace PromptFilesExamplesApp.Interfaces;

public interface IAddress
{
    string Street { get; set; }
    string City { get; set; }
    string State { get; set; }
    string PostalCode { get; set; }
}