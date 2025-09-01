using System.ComponentModel.DataAnnotations;

namespace CustomTagHelpersApp.Classes;

public enum WeekDays
{
    [Display(Name = "Sunday")]
    SUN,
    [Display(Name = "Monday")]
    MON,

    [Display(Name = "Tuesday")]
    TUE,

    [Display(Name = "Wednesday")]
    WED,

    [Display(Name = "Thursday")]
    THU,

    [Display(Name = "Friday")]
    FRI,

    [Display(Name = "Saturday")]
    SAT
}
