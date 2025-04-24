using AzureSettingsOptionsMonitorSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
#pragma warning disable MVC1002

namespace AzureSettingsOptionsMonitorSample.Pages;
public class IndexModel : PageModel
{
    private readonly IOptionsMonitor<AzureSettings> _azureSettings;

    private AzureSettings _azureSettingsIOptionsMonitor;

    [BindProperty]
    public required string TenantName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class with the specified Azure settings monitor.
    /// </summary>
    /// <param name="azureSettings">
    /// An <see cref="IOptionsMonitor{TOptions}"/> instance for monitoring changes to the <see cref="AzureSettings"/> configuration.
    /// </param>
    public IndexModel(IOptionsMonitor<AzureSettings> azureSettings)
    {
        _azureSettings = azureSettings;
        _azureSettingsIOptionsMonitor = _azureSettings.CurrentValue;

        _azureSettings.OnChange(config =>
        {
            if (_azureSettingsIOptionsMonitor.TenantName != config.TenantName)
            {
                OnTenantNameChanged(config);
            }
            else if (_azureSettingsIOptionsMonitor.ConnectionString != config.ConnectionString)
            {
                OnConnectionStringChanged(config);
            }
        });
    }

    /// <summary>
    /// Handles changes to the Azure settings values by updating the current configuration
    /// and synchronizing the <see cref="TenantName"/> property.
    /// </summary>
    /// <param name="azureSettings">The updated Azure settings containing the new configuration values.</param>
    private void OnTenantNameChanged(AzureSettings azureSettings)
    {
        _azureSettingsIOptionsMonitor.TenantName = azureSettings.TenantName;
        TenantName = azureSettings.TenantName;
    }

    /// <summary>
    /// Handles changes to the connection string in the Azure settings configuration.
    /// Updates the current configuration with the new connection string value.
    /// </summary>
    /// <param name="azureSettings">
    /// The updated Azure settings containing the new connection string value.
    /// </param>
    private void OnConnectionStringChanged(AzureSettings azureSettings)
    {
        _azureSettingsIOptionsMonitor.ConnectionString = azureSettings.ConnectionString;
    }
    public void OnGet()
    {
        TenantName = _azureSettingsIOptionsMonitor.TenantName;
    }

    /// <summary>
    /// Retrieves the tenant name from the current Azure settings configuration.
    /// See fetch in frontend JavaScript.
    /// </summary>
    /// <returns>
    /// A <see cref="JsonResult"/> containing the tenant name as specified in the current
    /// <see cref="AzureSettings"/> configuration.
    /// </returns>
    [HttpGet]
    public IActionResult OnGetTenantName()
    {
        return new JsonResult(_azureSettings.CurrentValue.TenantName);
    }

}
