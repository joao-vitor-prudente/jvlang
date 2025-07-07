namespace JVLang.Tests;

public class LexerTests
{
    private Lexer _lexer;

    [SetUp]
    public void Setup()
    {
        _lexer = new Lexer("""
                           int x = 4.5;
                           str y = "salve";

                           if (x >= 4 and y == "salve") {
                               print("salve");
                           }
                           """.ToCharArray());
    }

    [Test]
    public void Tokenize_SplitsInputIntoTokens()
    {
        Assert.That(_lexer.Tokenize(), Is.EquivalentTo(new Token[]
        {
            new Symbol("int"),
            new Symbol("x"),
            new Assign(),
            new Number("4.5"),
            new Semicolon(),
            new Symbol("str"),
            new Symbol("y"),
            new Assign(),
            new String("salve"),
            new Semicolon(),
            new Symbol("if"),
            new OpenParenthesis(),
            new Symbol("x"),
            new GreaterThanOrEqual(),
            new Number("4"),
            new Symbol("and"),
            new Symbol("y"),
            new Equal(),
            new String("salve"),
            new CloseParenthesis(),
            new OpenCurlyBracket(),
            new Symbol("print"),
            new OpenParenthesis(),
            new String("salve"),
            new CloseParenthesis(),
            new Semicolon(),
            new CloseCurlyBracket()
        }));
    }
}