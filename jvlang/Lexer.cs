namespace jvlang;

public class Lexer(char[] input)
{
    private readonly List<Token> _tokens = [];
    private int _currentIndex = -1;

    private char? CurrentChar => _currentIndex < 0 ? '\0' : _currentIndex >= input.Length ? null : input[_currentIndex];
    private char? NextChar => _currentIndex + 1 >= input.Length ? null: input[_currentIndex + 1];

    private void HandleValue(Func<string, ValueToken> constructToken, Predicate<char> stopCondition)
    {
        var value = "";
        while (CurrentChar is not null && !stopCondition(CurrentChar.Value))
        {
            value += CurrentChar;
            _currentIndex++;
        }

        if (CurrentChar != '"') _currentIndex--;

        _tokens.Add(constructToken(value));
    }


    private void HandleMultiCharOperator(char secondChar, OperatorToken bothOp, OperatorToken onlyOneOp)
    {
        if (NextChar != secondChar)
        {
            _tokens.Add(onlyOneOp);
            return;
        }

        _tokens.Add(bothOp);
        _currentIndex++;
    }

    public Token[] Tokenize()
    {
        while (CurrentChar is not null)
        {
            _currentIndex++;

            switch (CurrentChar)
            {
                case char c when String.StartCondition(c):
                    _currentIndex++;
                    HandleValue(s => new String(s), String.EndCondition);
                    break;
                case char c when Number.StartCondition(c):
                    HandleValue(s => new Number(s), Number.EndCondition);
                    break;
                case char c when Symbol.StartCondition(c):
                    HandleValue(s => new Symbol(s), Symbol.EndCondition);
                    break;
                case '=':
                    HandleMultiCharOperator('=', new Equal(), new Assign());
                    break;
                case '>':
                    HandleMultiCharOperator('=', new GreaterThanOrEqual(), new GreaterThan());
                    break;
                case '<':
                    HandleMultiCharOperator('=', new LessThanOrEqual(), new LessThan());
                    break;
                case '!':
                    HandleMultiCharOperator('=', new NotEqual(), new Not());
                    break;
                case ',':
                    _tokens.Add(new Comma());
                    break;
                case ';':
                    _tokens.Add(new Semicolon());
                    break;
                case '(':
                    _tokens.Add(new OpenParenthesis());
                    break;
                case ')':
                    _tokens.Add(new CloseParenthesis());
                    break;
                case '{':
                    _tokens.Add(new OpenCurlyBracket());
                    break;
                case '}':
                    _tokens.Add(new CloseCurlyBracket());
                    break;
                case '[':
                    _tokens.Add(new OpenBracket());
                    break;
                case ']':
                    _tokens.Add(new CloseBracket());
                    break;
                case '-':
                    _tokens.Add(new Minus());
                    break;
                case '+':
                    _tokens.Add(new Plus());
                    break;
                case '*':
                    _tokens.Add(new Multiply());
                    break;
                case '/':
                    _tokens.Add(new Divide());
                    break;
                case '^':
                    _tokens.Add(new Exponent());
                    break;
            }
        }

        return _tokens.ToArray();
    }
}