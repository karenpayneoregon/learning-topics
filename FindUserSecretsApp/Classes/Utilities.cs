namespace FindUserSecretsApp.Classes
{
    public class Utilities
    {
        /// <summary>
        /// Gets the path to the folder where user secrets are stored.
        /// </summary>
        /// <remarks>
        /// The folder path is constructed using the application data directory, 
        /// combined with the "Microsoft\UserSecrets" subdirectory.
        /// </remarks>
        /// <returns>
        /// A <see cref="string"/> representing the full path to the user secrets folder.
        /// </returns>
        public static string SecretsFolder => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Microsoft",
            "UserSecrets"
        );

        /// <summary>
        /// Gets a value indicating whether the UserSecrets folder exists on the system.
        /// </summary>
        /// <value>
        /// <c>true</c> if the UserSecrets folder exists; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// The UserSecrets folder is typically located in the Application Data directory under "Microsoft\UserSecrets".
        /// This property checks for the existence of the folder using the <see cref="System.IO.Directory.Exists(string)"/> method.
        /// </remarks>
        public static bool SecretsFolderExists => Directory.Exists(SecretsFolder);

        /// <summary>
        /// Constructs the full path to the project folder within the user secrets directory.
        /// </summary>
        /// <param name="sender">The name or identifier of the project.</param>
        /// <returns>The full path to the project folder as a string.</returns>
        public static string ProjectFolder(string sender) => 
            Path.Combine(SecretsFolder, sender);
    }
}
