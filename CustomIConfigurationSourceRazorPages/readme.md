# About

This project uses two JSON files. One section uses secrets along with an in-memory collection obtained from an SQL Server database read using EF-Core.

- `MailSettings` model is setup with `set-secrets.bat` which needs path change to match your project location.
   - Use `dotnet user-secrets list` to see the secrets in a terminal window.
-  `Layout` model is read from `other.json`
- `HelpDesk` is read using a in-memory collection using EF-Core


## Setup before running

- Run `set-secrets.bat` to set the secrets.
- Create the database using using the script in the `Scripts` folder.

## Best practices

The code presented should be considered possibilities for reading information to configure an application. Best practices are going to be different depending on a developer’s environment.

Always consider the possibility that some entity will try to hack your application. In some cases, if you are in an enterprise environment, check with those who are responsible for security to ask for advice.

The author has been fortunate to have worked at enterprises that are secure enough not to be concerned about security measures, yet as stated above, if you are not as fortunate, be mindful of projecting sensitive information.
