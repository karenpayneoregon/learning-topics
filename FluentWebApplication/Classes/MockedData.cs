using FluentWebApplication.Models;

namespace FluentWebApplication.Classes;

public class MockedData
{
    public static List<Person> People =
    [
        new Person()
        {
            PersonId = 1,
            FirstName = "Karen",
            LastName = "Payne",
            EmailAddress = "payne@comcast.net"
        },
        new Person()
        {
            PersonId = 2,
            FirstName = "Bob",
            LastName = "Smith",
            EmailAddress = "BillyBob@bear.com"
        },
        new Person()
        {
            PersonId = 3,
            FirstName = "Mary",
            LastName = "Johnson",
            EmailAddress = "mary.johnson@email.com"
        },
        new Person()
        {
            PersonId = 4,
            FirstName = "James",
            LastName = "Anderson",
            EmailAddress = "james.anderson@email.com"
        },
        new Person()
        {
            PersonId = 5,
            FirstName = "Linda",
            LastName = "Martinez",
            EmailAddress = "linda.martinez@email.com"
        },
        new Person()
        {
            PersonId = 6,
            FirstName = "Michael",
            LastName = "Brown",
            EmailAddress = "michael.brown@email.com"
        },
        new Person()
        {
            PersonId = 7,
            FirstName = "Patricia",
            LastName = "Davis",
            EmailAddress = "patricia.davis@email.com"
        },
        new Person()
        {
            PersonId = 8,
            FirstName = "David",
            LastName = "Wilson",
            EmailAddress = "david.wilson@email.com"
        },
        new Person()
        {
            PersonId = 9,
            FirstName = "Jennifer",
            LastName = "Moore",
            EmailAddress = "jennifer.moore@email.com"
        },
        new Person()
        {
            PersonId = 10,
            FirstName = "Robert",
            LastName = "Taylor",
            EmailAddress = "robert.taylor@email.com"
        }
    ];
}
