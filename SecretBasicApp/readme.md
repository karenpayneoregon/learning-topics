# About

This project demonstrates how to store `sensitive information` that should not be pushed to a `repository`.

## Important

- When moving to a new computer, make sure to back up secrets.
    - Karen has a program to help with this.
- If working in a team, you need to share secrets as they are not stored in the repository.


## Required NuGet packages

- Install-Package Microsoft.Extensions.Configuration
- Install-Package Microsoft.Extensions.Hosting
- Install-Package Microsoft.Extensions.Configuration.UserSecrets

## On disk

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Enter anything here"
  },
  "MailSettings": {
    "FromAddress": "",
    "Host": "",
    "Port": 0,
    "TimeOut": 0,
    "PickupFolder": ""
  },
  "Login": {
    "UserName": "",
    "Password": ""
  } 
}
```

## In memory

```json
{
  "ConnectionStrings:DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EF.NotesDatabase;Integrated Security=True",
  "MailSettings:FromAddress": "Payne@gmail.com",
  "MailSettings:Host": "SomeHost",
  "MailSettings:Port": "25",
  "MailSettings:TimeOut": "3000",
  "MailSettings:PickupFolder": "MailDrop",
  "Login:UserName": "payne$1",
  "Login:Password": "your_password_here"
}
``` 