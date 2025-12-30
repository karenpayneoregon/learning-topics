using System.Numerics;

namespace IncrementDecrementApp.Classes;
internal static class GenericExtensions
{   
    /// <summary>
    /// Increments the specified numeric value by 1.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the numeric value. Must implement <see cref="INumber{T}"/>.
    /// </typeparam>
    /// <param name="sender">
    /// The numeric value to increment.
    /// </param>
    /// <returns>
    /// The result of incrementing <paramref name="sender"/> by 1.
    /// </returns>
    public static T Increment<T>(this T sender) where T : INumber<T>
        => sender + T.One;

    /// <summary>
    /// Increments the specified numeric value by the given amount.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the numeric value. Must implement <see cref="INumber{T}"/>.
    /// </typeparam>
    /// <param name="sender">
    /// The numeric value to increment.
    /// </param>
    /// <param name="value">
    /// The amount to increment the <paramref name="sender"/> by.
    /// </param>
    /// <returns>
    /// The result of incrementing <paramref name="sender"/> by <paramref name="value"/>.
    /// </returns>
    public static T Increment<T>(this T sender, T value) where T : INumber<T>
        => sender + value;

    /// <summary>
    /// Decrements the specified numeric value by 1.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the numeric value. Must implement <see cref="INumber{T}"/>.
    /// </typeparam>
    /// <param name="sender">
    /// The numeric value to decrement.
    /// </param>
    /// <returns>
    /// The result of decrementing <paramref name="sender"/> by 1.
    /// </returns>
    public static T Decrement<T>(this T sender) where T : INumber<T>
        => sender - T.One;

    /// <summary>
    /// Decrements the specified numeric value by the given amount.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the numeric value. Must implement <see cref="INumber{T}"/>.
    /// </typeparam>
    /// <param name="sender">
    /// The numeric value to decrement.
    /// </param>
    /// <param name="value">
    /// The amount to decrement the <paramref name="sender"/> by.
    /// </param>
    /// <returns>
    /// The result of decrementing <paramref name="sender"/> by <paramref name="value"/>.
    /// </returns>
    public static T Decrement<T>(this T sender, T value) where T : INumber<T>
        => sender - value;
}