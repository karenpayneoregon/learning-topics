using DateTimeWeekNumberSample.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
// ReSharper disable ConvertConstructorToMemberInitializers

namespace DateTimeWeekNumberSample.Pages;
public class IndexModel : PageModel
{

    [BindProperty, DataType(nameof(Week))]
    public DateTime Week { get; set; }
    public void OnGet()
    {
    }

    public IndexModel() => Week = DateTime.Now;

    public void OnPost()
    {
        /*
         * Have to manipulate Week to get year/month else receiving page will not translate correctly
         */
        var week = Request.Form[nameof(Week)].First()!.Split("-W");

        Log.Information("Week array: {W}", string.Join(",", week));
        Week = ISOWeek.ToDateTime(Convert.ToInt32(week[0]), Convert.ToInt32(week[1]), DayOfWeek.Monday);

        Log.Information("Week = {W}", DateOnly.FromDateTime(Week));
        
        Log.Information("Week extension {WOY}", DateTime.Now.WeekOfYear());
    }
}
