using ModelDeconstructApp.Models;

namespace ModelDeconstructApp.Classes;

internal static class PersonExtensions
{
    public static void Deconstruct(this Person p, out int id, out string fullName, out DateOnly birthDate)
        => (id, fullName, birthDate) = (p.Id, p.FullName, p.BirthDate);

}