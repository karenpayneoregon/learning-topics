﻿#nullable disable

using FieldKeywordSample.Classes;
using FieldKeywordSample.Interfaces;

namespace FieldKeywordSample.Models;

/// <summary>
/// Represents a person entity with properties for identification, name, and birthdate.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IPerson"/> interface and utilizes the `field` keyword 
/// to provide custom logic for property setters, such as capitalizing the first letter of names.
/// </remarks>
public class Person : IPerson
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    /// <value>
    /// The unique identifier for the customer.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    /// <remarks>
    /// The setter automatically capitalizes the first letter of the FirstName using the
    /// <see cref="Classes.StringExtensions.CapitalizeFirstLetter"/> method.
    /// </remarks>
    public required string FirstName
    {
        get;
        set => field = value.CapitalizeFirstLetter();
    }
    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    /// <remarks>
    /// The setter automatically capitalizes the first letter of the LastName using the
    /// <see cref="Classes.StringExtensions.CapitalizeFirstLetter"/> method.
    /// </remarks>
    public required string LastName
    {
        get;
        set => field = value.CapitalizeFirstLetter();
    }
    public required DateOnly BirthDate { get; set; }
}