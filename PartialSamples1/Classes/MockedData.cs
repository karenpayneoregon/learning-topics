using PartialSamples1.Interfaces;
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

    /// <summary>
    /// Generates a randomized list of people with unique IDs, first names, last names, and genders.
    /// </summary>
    /// <returns>A list of <see cref="IPerson"/> objects with randomized data.</returns>
    /// <remarks>
    /// The method alternates between male and female names to create a balanced dataset.
    /// </remarks>
    public static List<IPerson> RandomizePeople()
    {
        string[] firstNamesMale = ["james", "john", "robert", "michael", "david"];
        string[] firstNamesFemale = ["mary", "patricia", "jennifer", "linda", "elizabeth"];
        string[] lastNames = ["smith", "johnson", "williams", "brown", "jones"];

        List<IPerson> people = [];

        for (int index = 1; index <= 10; index++)
        {
            bool isMale = index % 2 == 1;
            string firstName = isMale
                ? firstNamesMale[(index / 2) % firstNamesMale.Length]
                : firstNamesFemale[(index / 2 - 1) % firstNamesFemale.Length];

            string lastName = lastNames[(index - 1) % lastNames.Length];

            people.Add(new Person
            {
                Id = index,
                FirstName = firstName,
                LastName = lastName,
                Gender = isMale ? Gender.Male : Gender.Female
            });
        }

        return people;
    }

    /// <summary>
    /// Generates a list of randomized Social Security Numbers (SSNs) in a standard format.
    /// </summary>
    /// <returns>A list of strings representing Social Security Numbers in the format "XXX-XX-XXXX".</returns>
    /// <remarks>
    /// Each SSN is generated using random numbers to ensure uniqueness within the list.
    /// This method is useful for testing scenarios where valid but non-real SSNs are required.
    /// </remarks>
    public static List<string> GeneratedSocialSecurityNumbers()
    {
        var random = new Random();
        var ssnList = new List<string>();

        for (int index = 0; index < 5; index++)
        {
            string ssn = $"{random.Next(100, 1000):000}-{random.Next(10, 100):00}-{random.Next(1000, 10000):0000}";
            ssnList.Add(ssn);
        }

        return ssnList;
    }
}


