namespace PrintMembersSamples.Interfaces;

public interface ITaxpayer
{
    /// <summary>
    /// Gets the Social Security Number (SSN) of the taxpayer.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the taxpayer's SSN.
    /// </value>
    public string SSN { get; init; }
}