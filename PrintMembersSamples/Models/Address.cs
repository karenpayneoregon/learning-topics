#nullable enable
namespace PrintMembersSamples.Models;

public record Address(string Line1, string? Line2, string City, string Country, string PostCode)
{
    public override string ToString() =>
        $"{Line1}, {(Line2 is not null ? Line2 + ", " : string.Empty)}{City}, {Country}, {PostCode}";
}