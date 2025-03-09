@echo off

:: Alter the path to match your project path
set projectPath="C:\OED\DotnetLand\VS2022\HowToSeriesSolution\CustomIConfigurationSourceRazorPages\CustomIConfigurationSourceRazorPages.csproj"

dotnet user-secrets set "MailSettings:FromAddress" "your-email@example.com" --project %projectPath%
dotnet user-secrets set "MailSettings:Host" "smtp.example.com" --project %projectPath%
dotnet user-secrets set "MailSettings:Port" "587" --project %projectPath%
dotnet user-secrets set "MailSettings:TimeOut" "30" --project %projectPath%
dotnet user-secrets set "MailSettings:PickupFolder" "MailDrop" --project %projectPath%

echo Secrets set successfully!
