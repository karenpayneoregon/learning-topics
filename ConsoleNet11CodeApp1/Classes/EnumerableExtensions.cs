namespace ConsoleNet11CodeApp1.Classes;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> sender)
    {
        public int Count => sender.Count();

        public T this[int index] => sender.ElementAt(index);
    }
}