using FieldKeywordSample.Interfaces;
using FieldKeywordSample.Models;

namespace FieldKeywordSample.Classes;
internal class MockedData
{

    /// <summary>
    /// Retrieves a list of people as a view.
    /// </summary>
    /// <returns>A <see cref="List{T}"/> of <see cref="IPerson"/> representing the people view.</returns>
    public static List<IPerson> People()
    {
        List<IPerson> people =
        [
            new Customer
            {
                Id = 1,
                CustomerId = 200,
                FirstName = "john",
                LastName = "Doe",
                BirthDate = new DateOnly(2000, 1, 7),
                Addresses =
                [
                    new Address
                    {
                        CustomerId = 200,
                        Street = "123 Main St",
                        City = "Springfield",
                        State = "CA",
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "555-555-5555"
                    },
                    new Address
                    {
                        CustomerId = 200,
                        Street = "456 Elm St",
                        City = "Shelbyville",
                        State = "CA",
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "555-555-5566"
                    }
                ]
            },
            new Person
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "smith",
                BirthDate = new DateOnly(1978, 8, 15)
            },
            new Person
            {
                Id = 3,
                FirstName = "Robert",
                LastName = "Johnson",
                BirthDate = new DateOnly(1985, 3, 22)
            },
            new Customer
            {
                Id = 4,
                CustomerId = 204,
                FirstName = "Emily",
                LastName = "Davis",
                BirthDate = new DateOnly(1982, 4, 12),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 204, 
                            Street = "789 Oak St", 
                            City = "Portland", 
                            State = "OR", 
                            ZipCode = "97001", 
                            Country = "USA", 
                            Phone = "555-111-0004"
                        }
                    ]
            },
            new Person
            {
                Id = 5,
                FirstName = "Michael",
                LastName = "Brown",

                BirthDate = new DateOnly(1990, 5, 9)
            },
            new Customer
            {
                Id = 6,
                CustomerId = 206,
                FirstName = "Linda",
                LastName = "Taylor",
                BirthDate = new DateOnly(1983, 6, 25),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 206, 
                            Street = "321 Birch St", 
                            City = "Salem", 
                            State = "OR", 
                            ZipCode = "97002", 
                            Country = "USA", 
                            Phone = "555-111-0006"
                        }
                    ]
            },
            new Person
            {
                Id = 7,
                FirstName = "William",
                LastName = "Anderson",
                BirthDate = new DateOnly(1987, 7, 18)
            },
            new Customer
            {
                Id = 8,
                CustomerId = 208,
                FirstName = "Barbara",
                LastName = "Thomas",
                BirthDate = new DateOnly(1981, 8, 10),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 208, 
                            Street = "654 Maple Ave", 
                            City = "Eugene", 
                            State = "OR", 
                            ZipCode = "97003", 
                            Country = "USA", 
                            Phone = "555-111-0008"
                        }
                    ]
            },
            new Person
            {
                Id = 9,
                FirstName = "David",
                LastName = "Jackson",
                BirthDate = new DateOnly(1989, 9, 3)
            },
            new Customer
            {
                Id = 10,
                CustomerId = 210,
                FirstName = "Susan",
                LastName = "White",
                BirthDate = new DateOnly(1975, 10, 14),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 210, 
                            Street = "987 Cedar Ln", 
                            City = "Medford", 
                            State = "OR", 
                            ZipCode = "97004", 
                            Country = "USA", 
                            Phone = "555-111-0010"
                        }
                    ]
            },
            new Person
            {
                Id = 11,
                FirstName = "Joseph",
                LastName = "Harris",
                BirthDate = new DateOnly(1986, 11, 27)
            },
            new Customer
            {
                Id = 12,
                CustomerId = 212,
                FirstName = "jessica",
                LastName = "martin",
                BirthDate = new DateOnly(1979, 12, 6),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 212, 
                            Street = "222 Walnut Dr", 
                            City = "corvallis", 
                            State = "OR", 
                            ZipCode = "97005", 
                            Country = "USA", 
                            Phone = "555-111-0012"
                        }
                    ]
            },
            new Person
            {
                Id = 13,
                FirstName = "Thomas",
                LastName = "Thompson",
                BirthDate = new DateOnly(1992, 1, 5)
            },
            new Customer
            {
                Id = 14,
                CustomerId = 214,
                FirstName = "Patricia",
                LastName = "Garcia",
                BirthDate = new DateOnly(1980, 2, 22),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 214, 
                            Street = "301 Fir Ct", 
                            City = "bend", 
                            State = "OR", 
                            ZipCode = "97006", 
                            Country = "USA", 
                            Phone = "555-111-0014"
                        }
                    ]
            },
            new Person
            {
                Id = 15,
                FirstName = "Daniel",
                LastName = "Martinez",
                BirthDate = new DateOnly(1988, 3, 15)
            },
            new Customer
            {
                Id = 16, 
                CustomerId = 216, 
                FirstName = "Nancy", 
                LastName = "Robinson",
                BirthDate = new DateOnly(1977, 4, 8),
                Addresses = 
                    [ 
                        new Address
                        {
                            CustomerId = 216, 
                            Street = "444 Spruce Blvd", 
                            City = "gresham", 
                            State = "OR", 
                            ZipCode = "97007", 
                            Country = "USA", 
                            Phone = "555-111-0016"
                        } 
                    ]
            },
            new Person
            {
                Id = 17, 
                FirstName = "Matthew", 
                LastName = "Clark", 
                BirthDate = new DateOnly(1991, 5, 28)
            },
            new Customer
            {
                Id = 18,
                CustomerId = 218,
                FirstName = "Karen",
                LastName = "Rodriguez",
                BirthDate = new DateOnly(1976, 6, 20),
                Addresses =
                    [
                        new Address
                        {
                            CustomerId = 218, 
                            Street = "118 Poplar St",
                            City = "Hillsboro", 
                            State = "OR", 
                            ZipCode = "97008", 
                            Country = "USA",
                            Phone = "555-111-0018"
                        }
                    ]
            },
            new Person
            {
                Id = 19, 
                FirstName = "Anthony",
                LastName = "Lewis", 
                BirthDate = new DateOnly(1984, 7, 30)
            },
            new Customer
            {
                Id = 20, 
                CustomerId = 220, 
                FirstName = "Donna",
                LastName = "lee", 
                BirthDate = new DateOnly(1982, 8, 13), 
                Addresses = 
                    [ 
                        new Address
                        {
                            CustomerId = 220, 
                            Street = "550 Willow Way", 
                            City = "Albany",
                            State = "OR", 
                            ZipCode = "97009",
                            Country = "USA", 
                            Phone = "555-111-0020"
                        } 
                    ]
            },
            new Person
            {
                Id = 21, 
                FirstName = "christopher", 
                LastName = "Walker", 
                
                BirthDate = new DateOnly(1993, 9, 12)
            },
            new Customer
            {
                Id = 22, 
                CustomerId = 222,
                FirstName = "Sandra",
                LastName = "Hall", 
                BirthDate = new DateOnly(1973, 10, 16), 
                Addresses = 
                    [ 
                        new Address
                        {
                            CustomerId = 222, 
                            Street = "911 Redwood Ave", 
                            City = "beaverton", 
                            State = "OR", 
                            ZipCode = "97010", 
                            Country = "USA", 
                            Phone = "555-111-0022"
                        } ,
                        new Address
                        {
                            CustomerId = 222, 
                            Street = "91 Deep wood Ave", 
                            City = "beaverton", 
                            State = "OR", 
                            ZipCode = "97010", 
                            Country = "USA", 
                            Phone = "555-111-0033"
                        } 
                    ]
            },
            new Person
            {
                Id = 23, 
                FirstName = "Joshua", 
                LastName = "Allen", 
                BirthDate = new DateOnly(1985, 11, 11)
            },
            new Customer
            {
                Id = 24, 
                CustomerId = 224, 
                FirstName = "Carol", 
                LastName = "Young", 
                BirthDate = new DateOnly(1974, 12, 4), 
                Addresses = 
                    [ 
                        new Address
                        {
                            CustomerId = 224, 
                            Street = "888 Chestnut Pl", 
                            City = "Lake Oswego", State = "OR", 
                            ZipCode = "97011", 
                            Country = "USA", 
                            Phone = "555-111-0024"
                        } 
                    ]
            },
            new Person 
                {
                    Id = 25, 
                    FirstName = "andrew", 
                    LastName = "king", 
                    BirthDate = new DateOnly(1994, 1, 21) 
                }
        ];

        return people;
    }
}
