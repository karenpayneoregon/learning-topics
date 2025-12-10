# ![Vs](instructionsAssets/vs.png) How to create a new solution and projects in Visual Studio


The following instructions will walk you through creating a home for learning C#.

The instructions will create a Visual Studio solution, which is done once and becomes a central location for creating several console projects for the free camp learning.


:white_check_mark: Avoid using spaces in solution or project names, as well as in file names. 


:bulb: [What are solutions and projects in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/ide/solutions-and-projects-in-visual-studio?view=visualstudio)?



## Set-up default project location

1. Select **Tools** > **Options** from the menu.
2. In the Options dialog, expand the **Projects and Solutions** node.
3. Select **Locations**.
4. In the **Projects location** box, enter the path where you want to store your projects.
5. Click **OK** to save your changes.


**Figure 1**
![Figure1](instructionsAssets/figure1.png)

## Create a solution

A solution is a container that organizes one or more related projects in Visual Studio. 

By creating a solution all your projects are in one place, making it easier to manage and navigate between them.

For trainng this is only needed one time.


### To create a new solution:

- Select **File** > **New Project...** from the menu.
- The following dialog will appear.
   - As shown, type in blank solution in the search box.
   - Click the **Next button**.

> **Note**
> The blured text shows recently opened solutions and projects. To open one of them, simply click on it.

![Figure2](instructionsAssets/figure2.png)

- In the next dialog (The location will be which was selected in figure 1 above):
   - Enter a name for your solution in the **Solution name** box, e.g., `TrainingSolution`
   - Click the **Create** button.

![Figure3](instructionsAssets/figure3.png)

## Create a new project in the solution

- From Visual Studio menu, select View > Solution Explorer.
- In the Solution Explorer, right-click on the solution node (e.g., `TrainingSolution`) and select **Add** > **New Project...**.
- Type in `Console App` in the search box.
- Click the **Next button**.

![Figure4](instructionsAssets/figure4.png)

- Give the project a meaningful name, e.g., `Lesson1`. Avoid spaces and special characters in the project name followed by on the next page I recommend :bulb: checking `Do not use top-level namespaces`.
- `Framework` accept the default selection.
- Click the **Create** button.

![Figure6](instructionsAssets/figure6.png)

### View from Solution Explorer

![Figure7](instructionsAssets/figure7.png)

Double-click on `Program.cs` to open the code editor.

![Figure8](instructionsAssets/figure8.png)