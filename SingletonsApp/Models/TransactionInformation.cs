namespace SingletonsApp.Models;

/// <summary>
/// Represents transaction-related information within the application.
/// </summary>
/// <remarks>
/// This class is designed to encapsulate the details of a transaction, such as its current value.
/// It is utilized in various parts of the application, including singleton patterns and JSON serialization/deserialization.
/// </remarks>
public sealed class TransactionInformation
{
    /// <summary>
    /// Gets or sets the current value associated with the transaction.
    /// </summary>
    /// <remarks>
    /// This property represents the primary identifier or state of the transaction.
    /// It is used throughout the application for tracking and updating transaction details.
    /// </remarks>
    public string CurrentValue { get; set; } = string.Empty;
}