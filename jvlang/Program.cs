namespace jvlang;

public class Program
{
    public static void Main(string[] args)
    {
        var cli = new Cli(args);
        var text = cli.ReadFile();
        Console.WriteLine(text);
        var lexer = new Lexer(text);
        var tokens = lexer.Tokenize();
        foreach (var token in tokens) Console.WriteLine(token);
    }
}