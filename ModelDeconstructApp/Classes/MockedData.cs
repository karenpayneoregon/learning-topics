using ModelDeconstructApp.Models;

namespace ModelDeconstructApp.Classes;
public static class MockedData
{
    public static List<Person> People()
       =>
        [
            new Person
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Doe",
                BirthDate = new DateOnly(1985, 4, 12)
            },

            new Person
            {
                Id = 2,
                Firstname = "Jane",
                Lastname = "Smith",
                BirthDate = new DateOnly(1990, 9, 3)
            },

            new Person
            {
                Id = 3,
                Firstname = "David",
                Lastname = "Brown",
                BirthDate = new DateOnly(1978, 1, 22)
            },

            new Person
            {
                Id = 4,
                Firstname = "Emily",
                Lastname = "Clark",
                BirthDate = new DateOnly(1995, 7, 18)
            },

            new Person
            {
                Id = 5,
                Firstname = "Mark",
                Lastname = "Wilson",
                BirthDate = new DateOnly(1982, 12, 5)
            }
        ];

}

