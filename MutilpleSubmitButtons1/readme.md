# About

Shows how to have multiple submit buttons for a form and not showing arguments in address.


## Buttons

Are generated from reading from data in appsettings.json while in a real application you would read from a database. The reason for using json file is to keep the example simple, no need for reading from a database.

```html
<div class="d-flex flex-wrap gap-2">
    @foreach (var program in Model.Programs)
    {
        <button type="submit"
                name="action"
                value="@program.Key"
                class="btn btn-outline-dark">
            @program.Value
        </button>
    }
</div>
```

---

```json
{
  "Programs": {
    "YogaPostures": "Yoga Postures",
    "Meditation": "Kriya and Meditation",
    "RestorativeYoga": "Restorative Yoga"
  }
}
```

![Title](assets/title.png)