using System;

namespace Publisher.Models
{
    /// <summary>
    /// Represents a NuGet package
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Name of package
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Package version
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// For debug and displaying in a ListBox
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Version.ToString();

    }
}