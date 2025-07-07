namespace jvlang;

public abstract class Token
{
    public override string ToString()
    {
        return $"Token {GetType().Name}";
    }
}

public abstract class ControlToken : Token;

public abstract class OperatorToken : Token;

public abstract class ValueToken(string value) : Token
{
    public override string ToString()
    {
        return $"{base.ToString()}({value})";
    }
}

public class OpenParenthesis : ControlToken;

public class CloseParenthesis : ControlToken;

public class OpenBracket : ControlToken;

public class CloseBracket : ControlToken;

public class OpenCurlyBracket : ControlToken;

public class CloseCurlyBracket : ControlToken;

public class Comma : ControlToken;

public class Semicolon : ControlToken;

public class Plus : OperatorToken;

public class Minus : OperatorToken;

public class Multiply : OperatorToken;

public class Divide : OperatorToken;

public class Exponent : OperatorToken;

public class Assign : OperatorToken;

public class Equal : OperatorToken;

public class NotEqual : OperatorToken;

public class LessThan : OperatorToken;

public class LessThanOrEqual : OperatorToken;

public class GreaterThan : OperatorToken;

public class GreaterThanOrEqual : OperatorToken;

public class Not : OperatorToken;

public class String(string value) : ValueToken(value)
{
    public static bool StartCondition(char c)
    {
        return c == '"';
    }

    public static bool EndCondition(char c)
    {
        return StartCondition(c);
    }
}

public class Number(string value) : ValueToken(value)
{
    public static bool StartCondition(char c)
    {
        return char.IsNumber(c);
    }

    public static bool EndCondition(char c)
    {
        return !StartCondition(c) && c != '.';
    }
}

public class Symbol(string value) : ValueToken(value)
{
    public static bool StartCondition(char c)
    {
        return char.IsLetter(c) || c == '_';
    }

    public static bool EndCondition(char c)
    {
        return !StartCondition(c);
    }
}