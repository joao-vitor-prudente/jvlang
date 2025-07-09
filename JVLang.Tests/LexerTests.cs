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

                           if (x >= 4 && y == "salve") {
                               print("salve");
                           }
                           """.ToCharArray());
    }

    [Test]
    public void Tokenize_SplitsInputIntoTokens()
    {
        Assert.That(_lexer.Tokenize(), Is.EquivalentTo(new Token[]
        {
            new Keyword("int"),
            new Identifier("x"),
            new Assign(),
            new Number("4.5"),
            new Semicolon(),
            new Keyword("str"),
            new Identifier("y"),
            new Assign(),
            new String("salve"),
            new Semicolon(),
            new Keyword("if"),
            new OpenParenthesis(),
            new Identifier("x"),
            new GreaterThanOrEqual(),
            new Number("4"),
            new And(),
            new Identifier("y"),
            new Equal(),
            new String("salve"),
            new CloseParenthesis(),
            new OpenCurlyBracket(),
            new Identifier("print"),
            new OpenParenthesis(),
            new String("salve"),
            new CloseParenthesis(),
            new Semicolon(),
            new CloseCurlyBracket()
        }));
    }
}