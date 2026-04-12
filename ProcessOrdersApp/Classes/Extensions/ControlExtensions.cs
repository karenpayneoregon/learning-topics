using System.ComponentModel;

namespace ProcessOrdersApp.Classes.Extensions
{
    public static class ControlExtensions
    {
        /// <summary>
        /// Determines if a control needs to be invoked to prevent a cross thread violation. This is a generic
        /// extension method
        /// </summary>
        /// <typeparam name="T">Control</typeparam>
        /// <param name="control">Control</param>
        /// <param name="action">Predicate to run</param>
        /// <example>
        /// <code title="From Form1" >
        /// private void OnTimedEvent(object source, ElapsedEventArgs e)
        /// {
        ///     ElapsedTimerLabel.InvokeIfRequired(label =>
        ///     {
        ///         label.Text = $"{e.SignalTime}";
        ///     });
        /// 
        ///     FileOperations.CheckIfNewIncomingFileIsNeeded();
        /// }
        /// </code>
        /// </example>        
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => action(control)), null);
            }
            else
            {
                action(control);
            }
        }

        /// <summary>
        /// Disable all controls on a form
        /// </summary>
        /// <param name="parentControl">Form to work with</param>
        /// <param name="exclude">list of controls to exclude</param>
        public static void DisableControls(this Control parentControl, params string[] exclude)
        {
            var controls = parentControl.Descendants<Control>().ToList();
            if (exclude.Any())
            {
                foreach (var control in controls.Where(control => !exclude.Contains(control.Name)))
                {
                    control.Enabled = false;
                }
            }
            else
            {
                foreach (var control in controls)
                {
                    control.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Enable all controls on a form
        /// </summary>
        /// <param name="parentControl"></param>
        public static void EnableControls(this Control parentControl)
        {
            parentControl.Descendants<Control>().ToList().ForEach(c => c.Enabled = true);
        }

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
}