using SingletonWebApp.Models;

namespace SingletonWebApp.Interfaces;

public interface IHelpers
{
    List<MonthItem> MonthList { get; }
}