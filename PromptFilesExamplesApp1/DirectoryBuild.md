# Directory.Build.targets

## 📖 Overview
This file configures project-wide build behavior for all C# projects in the repository or just one project as done in this project.  

It specifically introduces **automatic partial class nesting** in Visual Studio, ensuring related partial class files are grouped under their primary class file in **Solution Explorer**.

This improves **readability, maintainability, and project organization**.

---

## ⚙️ Features

### ✅ Automatic Partial Class Nesting
- Groups `*.cs` partial class files together under their primary file.  
- Example:
  ```
  Person.cs
     ├── Person.Database.cs
     └── Person.Validation.cs
  ```

### 🔧 Configurable Behavior
- `EnablePartialClassAutoNesting` → Enables/disables auto-nesting (default: `true`).  
- `PartialClassAutoNestingVerbose` → Enables detailed logging for debugging (default: `false`).  

### 📂 Stable Intermediate Output
- Nesting rules are written to:  
  ```
  $(BaseIntermediateOutputPath)FileNesting.DependentUpon.generated.props
  ```
- Keeps builds **deterministic and consistent** across environments.

### 🔄 Automatic Re-Evaluation
- Whenever nesting rules change, Visual Studio re-evaluates the project automatically — **no manual reload required**.

### 🛠 Custom Build Task
- Uses a custom MSBuild task `WritePartialNestingProps` (via `CodeTaskFactory`) to generate props.  
- Ensures new partial files are immediately recognized and nested.

---

## 🚀 How It Works (Step by Step)
1. **MSBuild loads** `Directory.Build.targets` at build time.  
2. If auto-nesting is enabled:
   - The `WritePartialNestingProps` task scans all source files.  
   - It generates a `.props` file with `DependentUpon` relationships.  
3. The generated `.props` file is **imported into the project**.  
4. Visual Studio **refreshes Solution Explorer** to reflect nesting.  

---

## 🔧 Usage

### Enabling / Disabling
Inside `Directory.Build.targets`:
```xml
<PropertyGroup>
  <EnablePartialClassAutoNesting>true</EnablePartialClassAutoNesting>
  <PartialClassAutoNestingVerbose>false</PartialClassAutoNestingVerbose>
</PropertyGroup>
```

- Set `EnablePartialClassAutoNesting` → `false` to disable globally.  
- Set `PartialClassAutoNestingVerbose` → `true` for detailed logs.

---

## 🐞 Troubleshooting
- **Nesting not showing?**
  - Verify `EnablePartialClassAutoNesting` is set to `true`.  
  - Reload the solution in Visual Studio.  

- **Still not working?**
  - Set `PartialClassAutoNestingVerbose` to `true`.  
  - Inspect MSBuild output for errors.  

---

## 📌 Notes for Developers
- This affects **all projects** in the repository (unless overridden in a project-specific `.csproj`).  
- The generated `.props` file is **intermediate build output** and should not be committed to source control.  
- Works best with **Visual Studio 2019+** (tested with VS2022).  

---

## 🗂 Example in Action
Files:
```
Order.cs
Order.Repository.cs
Order.Validation.cs
```

Solution Explorer:
```
Order.cs
   ├── Order.Repository.cs
   └── Order.Validation.cs
```
