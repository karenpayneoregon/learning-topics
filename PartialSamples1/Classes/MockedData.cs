using PartialSamples1.Models;

namespace PartialSamples1.Classes;

/// <summary>
/// Provides methods for generating and managing mocked data for testing purposes.
/// </summary>
internal class MockedData
{

    /// <summary>
    /// Generates a randomized list of clients with unique IDs, first names, last names, and <see cref="Gender"/>.
    /// </summary>
    /// <returns>A list of <see cref="Client"/> objects with randomized data.</returns>
    /// <remarks>
    /// Using Bogus or a similar library could are other options.
    /// </remarks>
    public static List<Client> RandomizeClients()
    {
        var random = new Random();

        List<string> maleFirstNames = ["liam", "noah", "ethan", "mason", "James", "Logan", "Lucas"];
        List<string> femaleFirstNames = ["Emma", "Olivia", "Sophia", "ava", "Isabella", "mia", "Charlotte"];
        List<string> lastNames = ["Smith", "Johnson", "williams", "Brown", "jones", "Garcia", "martinez", "Davis"];

        var clients = new List<Client>();
        int id = 1;

        List<T> Shuffle<T>(List<T> list) => list.OrderBy(x => random.Next()).ToList();

        var femalePool = Shuffle(femaleFirstNames);
        var lastPool1 = Shuffle(lastNames);

        for (int index = 0; index < 5; index++)
        {
            clients.Add(new()
            {
                Id = id++,
                FirstName = femalePool[index % femalePool.Count],
                LastName = lastPool1[index % lastPool1.Count],
                Gender = Gender.Female
            });
        }

        var malePool = Shuffle(maleFirstNames);
        var lastPool2 = Shuffle(lastNames);

        for (int index = 0; index < 5; index++)
        {
            clients.Add(new()
            {
                Id = id++,
                FirstName = malePool[index % malePool.Count],
                LastName = lastPool2[index % lastPool2.Count],
                Gender = Gender.Male
            });
        }
        
        return clients;
        
    }
}


