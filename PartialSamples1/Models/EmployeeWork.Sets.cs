using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PartialSamples1.Models;
public partial class EmployeeWork
{
    public partial int Id { get; set => SetField(ref field, value); }

    public partial decimal HourlyWage
    {
        get; 
        set => SetField(ref field, value);
    }

    public partial int HoursWorked
    {
        get;
        set => SetField(ref field, value);
    }

    public partial decimal OvertimeRate
    {
        get;
        set => SetField(ref field, value);
    }

    public partial int OvertimeThreshold
    {
        get;
        set => SetField(ref field, value);
    }

    public partial decimal Salary
    {
        get;
        private set => SetField(ref field, value);
    }


    public partial void PerformWork()
    {
        // TODO
    }
}
