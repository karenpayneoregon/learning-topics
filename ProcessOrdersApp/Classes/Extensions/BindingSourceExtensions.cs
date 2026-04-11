using ProcessOrdersApp.Models;

namespace ProcessOrdersApp.Classes.Extensions;
internal static class BindingSourceExtensions
{
    /// <summary>
    /// Retrieves the current <see cref="OrdersResults"/> object from the specified <see cref="BindingSource"/>.
    /// </summary>
    /// <param name="source">
    /// The <see cref="BindingSource"/> from which to retrieve the current item.
    /// </param>
    /// <returns>
    /// The current <see cref="OrdersResults"/> object in the binding source, or throws an exception if the current item is null.
    /// </returns>
    /// <remarks>
    /// This method assumes that the current item in the <see cref="BindingSource"/> is of type <see cref="OrdersResults"/>.
    /// If the current item is not of this type, an exception will be thrown.
    /// </remarks>
    /// <exception cref="InvalidCastException">
    /// Thrown if the current item in the <see cref="BindingSource"/> cannot be cast to <see cref="OrdersResults"/>.
    /// </exception>
    public static OrdersResults GetCurrentOrder(this BindingSource source) 
        => (source.Current as OrdersResults)!;
}
