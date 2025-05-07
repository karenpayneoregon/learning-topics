using FieldKeywordSample.Interfaces;
using FieldKeywordSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldKeywordSample.Classes;
internal class MockedData
{
    public static List<IPerson> People()
    {
        List<IPerson> people =
        [
            new Customer
            {
                Id = 1,
                CustomerId = 100,
                FirstName = "mary",
                LastName = "smith",
                BirthDate = new DateOnly(1960,12,2)
            },

            new Customer
            {
                Id = 2,
                CustomerId = 200,
                FirstName = "john",
                LastName = "doe",
                BirthDate = new DateOnly(2000,1,7),
                Addresses =
                [
                    new Address
                    {
                        CustomerId = 2,
                        Street = "123 Main St",
                        City = "Any town",
                        State = "Ca",
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "555-555-5555"
                    },
                    new Address
                    {
                        CustomerId = 2,
                        Street = "456 Main St",
                        City = "Some town",
                        State = "Ca",
                        ZipCode = "12345",
                        Country = "USA",
                        Phone = "555-555-5566"
                    }
                ]
            },

            new Person
            {
                Id = 3,
                FirstName = "jane",
                LastName = "doe",
                BirthDate = new DateOnly(1978,8,15)
            }
        ];
        return people;
    }
}
