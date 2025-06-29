using PromptFilesExamplesApp.Models;

namespace PromptFilesExamplesApp.Classes;

/// <summary>
/// Represents a grouping of customers by their gender, including the gender, 
/// the count of customers in the group, and the list of customers.
/// </summary>
/// <param name="Gender">
/// The gender of the customers in the group. Can be <see langword="null"/> if gender is unspecified.
/// </param>
/// <param name="Count">
/// The total number of customers in the group.
/// </param>
/// <param name="List">
/// The list of customers belonging to the group.
/// </param>
public record CustomerGenderGroup(Gender? Gender, int Count, List<Customer> List);