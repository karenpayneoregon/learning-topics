namespace SingletonsApp.Classes.SingletonSamples;

public sealed class Basic1
{
    private static readonly Lazy<Basic1> Lazy = new(() => new Basic1());
    public static Basic1 Instance => Lazy.Value;
}