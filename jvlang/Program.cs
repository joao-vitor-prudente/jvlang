namespace jvlang;

public class Program
{
    public static void Main(string[] args)
    {
        var cli = new Cli(args);
        var text = cli.ReadFile();
        Console.WriteLine(text);
    }
}