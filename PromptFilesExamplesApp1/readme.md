# Article

[Create C# nested files in Visual Studio](https://dev.to/karenpayneoregon/create-c-nested-files-in-visual-studio-3j20)

.filenesting.json

```json
{
  "help": "https://go.microsoft.com/fwlink/?linkid=866610",
  "root": true,
  "dependentFileProviders": {
    "add": {
      "fileToFile": {
        "add": {
          "Person.Notification.cs": [
            "Person.cs"
          ],
          "Person.Sets.cs": [
            "Person.cs"
          ]
        }
      }
    }
  }
}
```