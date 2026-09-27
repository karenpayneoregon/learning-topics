using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingletonLibrary;

namespace SingletonLibrary.Tests;

/// <summary>
/// Contains unit tests for the <see cref="EnvironmentSettings"/> class, ensuring its functionality and behavior
/// align with expected outcomes.
/// </summary>
/// <remarks>
/// This test class verifies the singleton behavior, configuration loading, and key properties of the
/// <see cref="EnvironmentSettings"/> class.
/// </remarks>
[TestClass]
[TestSubject(typeof(EnvironmentSettings))]
public class EnvironmentSettingsTest
{

    [TestMethod]
    public void MainConnectionValidation()
    {
        // Arrange
        var environmentSettings = EnvironmentSettings.Instance;

        // Act
        var mainConnection = environmentSettings.MainConnection;

        // Assert
        Assert.IsNotNull(mainConnection, "MainConnection should not be null.");
        Assert.IsFalse(string.IsNullOrWhiteSpace(mainConnection), "MainConnection should not be empty or whitespace.");
    }
    
    [TestMethod]
    public void ConfigurationIsNotNull()
    {
        // Arrange
        var environmentSettings = EnvironmentSettings.Instance;

        // Act
        var configuration = EnvironmentSettings.Configuration;

        // Assert
        Assert.IsNotNull(configuration, "Configuration should not be null.");
    }
    
    [TestMethod]
    public void ConfigurationLoadSuccessful()
    {
        // Arrange
        // The Configuration is loaded in the constructor of EnvironmentSettings, which is called when accessing Instance.
        // So, we just need to ensure it's not null.

        // Act
        var configuration = EnvironmentSettings.Configuration;

        // Assert
        Assert.IsNotNull(configuration, "Configuration should have been loaded successfully.");
        // Optionally, you can add more specific checks if you know expected keys or values
        // For example, checking if a specific setting exists:
        // Assert.IsNotNull(configuration["SomeSetting"], "Expected setting 'SomeSetting' not found.");
    }

    [TestMethod]
    public void SingletonInstanceIsConsistent()
    {
        // Arrange
        var instance1 = EnvironmentSettings.Instance;
        var instance2 = EnvironmentSettings.Instance;

        // Act
        // No specific action needed, just comparing instances.

        // Assert
        Assert.AreSame(instance1, instance2, "EnvironmentSettings.Instance should always return the same instance.");
    }
    
}