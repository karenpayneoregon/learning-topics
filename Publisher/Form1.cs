using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Publisher.Classes;
using Publisher.Models;
using Serilog;

namespace Publisher
{
    public partial class Form1 : Form
    {
        private readonly BindingSource _bindingSource = new ();
        public Form1()
        {
            InitializeComponent();
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            var source = DirectoryHelper.ProjectItems(SolutionHelper.ProjectNames(DirectoryHelper.SolutionName())).OrderBy(x => x.Name);
            _bindingSource.DataSource = source;
            ProjectListBox.DataSource = _bindingSource;
            
            _bindingSource.PositionChanged += BindingSourceOnPositionChanged;
            PositionChanged();
            
        }

        private void BindingSourceOnPositionChanged(object sender, EventArgs e)
        {
            PositionChanged();
        }

        /// <summary>
        /// Updates the data source of the <see cref="VersionsListBox"/> to reflect the package list
        /// of the currently selected project in the <see cref="ProjectListBox"/>.
        /// </summary>
        /// <remarks>
        /// This method is triggered whenever the position of the <see cref="_bindingSource"/> changes,
        /// ensuring that the <see cref="VersionsListBox"/> displays the correct package list
        /// corresponding to the currently selected project.
        /// </remarks>
        private void PositionChanged()
        {
            VersionsListBox.DataSource = ((ProjectItem) _bindingSource.Current)!.PackageList.OrderByDescending(x => x.Version).ToList();
        }

        /// <summary>
        /// Handles the click event of the <see cref="PublishButton"/>.
        /// </summary>
        /// <param name="sender">The source of the event, typically the <see cref="PublishButton"/>.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// <list type="bullet">
        /// <item>Retrieves the currently selected project and package.</item>
        /// <item>Prompts the user for confirmation before publishing.</item>
        /// <item>Copies the selected package asynchronously to the target location.</item>
        /// <item>Displays a success or error message based on the outcome.</item>
        /// </list>
        /// </remarks>
        private async void PublishButton_Click(object sender, EventArgs e)
        {

            try
            {
                var currentProject = (ProjectItem) _bindingSource.Current;
                var currentPackage = (Package) VersionsListBox.SelectedItem;

                if (!Dialogs.Question(this, "Question", $"Are you sure you want to publish {Path.GetFileNameWithoutExtension(currentPackage.Name)}?"))
                    return;
                
                await PackHelpers.CopyPackageAsync(Path.Combine(currentProject.Path, currentPackage.Name!));

                Log.Information("Package {P} copied successfully", currentPackage);
                Dialogs.Information(this, "Done");
                
            }
            catch (Exception exception)
            {
                
                Log.Error(exception,"On copy package");
                Dialogs.ErrorBox(exception);
            }
        }

        private void OpenLocalFeedFolderButton_Click(object sender, EventArgs e)
        {
            PackHelpers.OpenFeedFolder();
        }
    }
}
