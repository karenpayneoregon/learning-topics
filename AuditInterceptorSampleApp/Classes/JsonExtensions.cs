using Newtonsoft.Json;

namespace AuditInterceptorSampleApp.Classes;
public static class JsonExtensions
{
    public static string ToJson<T>(this List<T> source) => JsonConvert.SerializeObject(source, Formatting.Indented);
}