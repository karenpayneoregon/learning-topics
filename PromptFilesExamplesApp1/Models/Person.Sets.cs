
using PromptFilesExamplesApp1.Classes;

namespace PromptFilesExamplesApp1.Models;
#nullable disable

internal partial class Person
{
    public int Id
    {
        get;
        set => SetField(ref field, value, nameof(Id));
    }

    public partial string Remarks
    {
        get;
        set => field = value.UpToFirstPeriodOrThreeWords();
    }
}