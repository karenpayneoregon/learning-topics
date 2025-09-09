using System.ComponentModel;

namespace PartialSamples1.Models;

/// <summary>
/// Represents the work details of an employee, including hourly wage, hours worked, 
/// overtime rate, and salary calculations. This class supports property change notifications.
/// </summary>
/// <remarks>
/// Part of an answer for Microsoft I replied to.
/// </remarks>
public partial class EmployeeWork : PropertySetters, INotifyPropertyChanged
{
    public partial int Id { get; set; }
    public partial decimal HourlyWage { get; set; }
    public partial int HoursWorked { get; set; }
    public partial decimal OvertimeRate { get; set; }       
    public partial int OvertimeThreshold { get; set; }
    /// <summary>
    /// For computed salary based on hours worked, hourly wage, overtime rate, and overtime threshold.
    /// See DataScripts\EmployeeWork.sql
    /// </summary>
    public partial decimal Salary { get; private set; }

    public partial void PerformWork();
}