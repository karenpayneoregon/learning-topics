namespace JsonConvertersSampleApp.Interfaces;

public interface ITaxpayer : IHuman
{
    public int Id { get; set; }
    public string SSN { get; set; }
    public string PIN { get; set; }
    public DateOnly StartDate { get; set; }
    public string EmployerIdentificationNumber { get; set; }
}