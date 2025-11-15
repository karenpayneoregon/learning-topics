using ModelDeconstructApp.Models;

namespace ModelDeconstructApp.Classes;

/// <summary>
/// Provides extension methods for the <see cref="Person"/> class, enabling additional functionality 
/// such as deconstruction of <see cref="Person"/> instances into their individual components.
/// </summary>
internal static class PersonExtensions
{
    /// <summary>
    /// Deconstructs the specified <see cref="Person"/> instance into its individual components.
    /// </summary>
    /// <param name="p">The <see cref="Person"/> instance to deconstruct.</param>
    /// <param name="id">The unique identifier of the person.</param>
    /// <param name="fullName">The full name of the person, combining first and last names.</param>
    /// <param name="birthDate">The birthdate of the person.</param>
    public static void Deconstruct(this Person p, out int id, out string fullName, out DateOnly birthDate)
        => (id, fullName, birthDate) = (p.Id, p.FullName, p.BirthDate);

}