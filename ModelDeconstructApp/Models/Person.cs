namespace ModelDeconstructApp.Models;

public record Person()
{
    public int Id { get; init; }
    public required string Firstname { get; init; }
    public required string Lastname { get; init; }
    public required DateOnly BirthDate { get; set; }
    public string FullName => $"{Firstname} {Lastname}";
}

