namespace DirectoryHelperSample2;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(DirectoryHelper.ProjectFolder());
        Console.WriteLine(DirectoryHelper.ProjectName());
 
        Console.ReadLine();
    }
}
