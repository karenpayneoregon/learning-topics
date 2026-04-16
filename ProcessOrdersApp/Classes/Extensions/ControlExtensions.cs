// ReSharper disable InvertIf
namespace ProcessOrdersApp.Classes.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Control"/> objects, 
/// enabling operations such as retrieving descendant controls or specific types of controls 
/// within a container like a form, panel, or group box.
/// </summary>
public static class ControlExtensions
{

    /// <summary>
    /// Base method for obtaining controls on a form or within a container like a panel or group box
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <returns></returns>
    public static IEnumerable<T> Descendants<T>(this Control control) where T : class
    {
        foreach (Control child in control.Controls)
        {
            if (child is T thisControl)
            {
                yield return (T)thisControl;
            }

            if (child.HasChildren)
            {
                foreach (T descendant in Descendants<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }


    /// <summary>
    /// Get all Button controls from specified control
    /// </summary>
    /// <param name="control">Desired control which can be a form or a control like a panel or GroupBox on a form</param>
    /// <returns>List of Button or an empty list if no Button on control</returns>
    public static List<Button> ButtonList(this Control control) 
        => control.Descendants<Button>().ToList();

}