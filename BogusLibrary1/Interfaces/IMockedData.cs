using BogusLibrary1.Models;

namespace BogusLibrary1.Interfaces;

/// <summary>
/// Represents an interface for generating mocked data.
/// </summary>
public interface IMockedData
{
    /// <summary>
    /// Generates a list of customers with random time values.
    /// </summary>
    /// <param name="count">The number of customers to generate.</param>
    /// <returns>A list of customers with random time values.</returns>
    List<Customer> GetCustomersTimeOnlyRandom(int count);

    /// <summary>
    /// Generates a list of customers with recent date values and time values set to midnight.
    /// </summary>
    /// <param name="count">The number of customers to generate.</param>
    /// <returns>A list of customers with recent date values and time values set to midnight.</returns>
    List<Customer> GetCustomersDateOnlyTimeOnlyRecent(int count);

    /// <summary>
    /// Generates a list of customers with time values set to a future time.
    /// </summary>
    /// <param name="count">The number of customers to generate.</param>
    /// <returns>A list of customers with time values set to a future time.</returns>
    List<Customer> GetCustomersTimeOnlySoon(int count);

    /// <summary>
    /// Generates a list of customers with time values set to a future time within the specified date range.
    /// </summary>
    /// <param name="count">The number of customers to generate.</param>
    /// <param name="minDate">The minimum date for the time values.</param>
    /// <param name="maxDate">The maximum date for the time values.</param>
    /// <returns>A list of customers with time values set to a future time within the specified date range.</returns>
    List<Customer> GetCustomersTimeOnlySoon(int count, DateOnly minDate, DateOnly maxDate);

    /// <summary>
    /// Generates a list of customers with recent time values.
    /// </summary>
    /// <param name="count">The number of customers to generate.</param>
    /// <returns>A list of customers with recent time values.</returns>
    List<Customer> GetCustomersBetweenTimeOnlyAndDateOnly(int count);
}