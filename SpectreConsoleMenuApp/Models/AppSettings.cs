namespace SpectreConsoleMenuApp.Models;
public class AppSettings
{
    public required NextValueSettings NextValue { get; set; }
}

public class NextValueSettings
{
    public required string Pin { get; set; }
}
