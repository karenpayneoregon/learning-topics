using IOptionsMonitorAzureSettingsBasicApp.Models;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;

namespace IOptionsMonitorAzureSettingsBasicApp.Pages;

public class IndexModel : PageModel
{
    /// <summary>
    /// The <see cref="IOptionsMonitor{TOptions}"/> instance for monitoring changes to the <see cref="AzureSettings"/> configuration.
    /// </summary>
    private readonly IOptionsMonitor<AzureSettings> _azureSettings;

    /// <summary>
    /// The <see cref="AzureSettings"/> instance containing the current configuration values.
    /// </summary>
    private AzureSettings _azureSettingsIOptionsMonitor;

    /// <summary>
    /// The <see cref="CompareLogic"/> instance used to compare configuration values.
    /// </summary>
    private CompareLogic _compareLogic;

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="azureSettings">
    /// An instance of <see cref="IOptionsMonitor{TOptions}"/> for monitoring changes to the <see cref="AzureSettings"/> configuration.
    /// </param>
    /// <param name="compareLogic">
    /// An instance of <see cref="CompareLogic"/> used to compare configuration values.
    /// </param>
    /// <remarks>
    /// This constructor sets up monitoring for changes to the <see cref="AzureSettings"/> configuration
    /// and initializes the internal state with the current configuration values.
    /// </remarks>
    public IndexModel(IOptionsMonitor<AzureSettings> azureSettings, CompareLogic compareLogic)
    {
        _compareLogic = compareLogic;
        _azureSettings = azureSettings;
        _azureSettingsIOptionsMonitor = _azureSettings.CurrentValue;

        _azureSettings.OnChange(OnAzureSettingsValueChange);
    }

    /// <summary>
    /// Handles changes to the <see cref="AzureSettings"/> configuration values.
    /// </summary>
    /// <param name="azureSettings">
    /// The updated <see cref="AzureSettings"/> instance containing the new configuration values.
    /// </param>
    /// <remarks>
    /// This method is triggered whenever the configuration values for <see cref="AzureSettings"/> are updated.
    /// It compares the new configuration values with the current ones and logs any detected changes.
    /// Specifically, it logs changes to the <c>TenantName</c> and <c>TenantId</c> properties.
    /// Additionally, the internal state is updated to reflect the new configuration values.
    /// 
    /// Note: This method is typically invoked four times even if there is only one change in the configuration.
    /// </remarks>
    private void OnAzureSettingsValueChange(AzureSettings azureSettings)
    {
        CompareAndLogDifferences(azureSettings);
        //LogAndUpdateAzureSettings(azureSettings);
    }

    private void LogAndUpdateAzureSettings(AzureSettings azureSettings)
    {
        if (_azureSettingsIOptionsMonitor.TenantName != azureSettings.TenantName)
        {
            Log.Information("TenantName changed from {P1} to {P2}",
                _azureSettingsIOptionsMonitor.TenantName,
                azureSettings.TenantName);

            _azureSettingsIOptionsMonitor.TenantName = azureSettings.TenantName;
        }
        else if (_azureSettingsIOptionsMonitor.TenantId != azureSettings.TenantId)
        {
            Log.Information("TenantId changed from {P1} to {P2}",
                _azureSettingsIOptionsMonitor.TenantId,
                azureSettings.TenantId);

            _azureSettingsIOptionsMonitor.TenantId = azureSettings.TenantId;
        }
    }

    private bool CompareAndLogDifferences(AzureSettings azureSettings)
    {

        var result = _compareLogic.Compare(_azureSettingsIOptionsMonitor, azureSettings);
        if (result.AreEqual)
        {
            return true;
        }
        else
        {
            result.Differences.ForEach(difference =>
            {
                Log.Information("{P1} changed from {P2} to {P3}",
                    difference.PropertyName,
                    difference.Object1Value,
                    difference.Object2Value);
            });
        }

        return false;
    }

    public void OnGet()
    {

    }
}
