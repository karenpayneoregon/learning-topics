namespace EnvironmentApplication.Classes
{
    public class StartupConfiguration
    {
        public static string Decide => DateTime.Now.IsNotWeekend() ? "_Layout" : "_LayoutWeekend";
    }
}
