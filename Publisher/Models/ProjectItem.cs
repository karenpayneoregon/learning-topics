using System.Collections.Generic;

namespace Publisher.Models
{
    public class ProjectItem
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Path to project <see cref="Name"/>
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// NuGet packages
        /// </summary>
        public List<Package> PackageList { get; set; }
        /// <summary>
        /// For debugging and for displaying in a ListBox
        /// </summary>
        /// <returns></returns>
        public override string ToString() => System.IO.Path.GetFileName(Name);

        public ProjectItem()
        {
            PackageList = new List<Package>();
        }

    }
}
