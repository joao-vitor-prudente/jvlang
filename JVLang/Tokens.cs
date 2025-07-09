namespace JVLang;

public abstract class Token
{
    public override string ToString()
    {
        return $"Token {GetType().Name}";
    }

    public override bool Equals(object? obj)
    {
        return GetType() == obj?.GetType();
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }
}

public abstract class ControlToken : Token;

public abstract class OperatorToken : Token;

public abstract class ValueToken(string value) : Token
{
    private string Value { get; } = value;

    public override string ToString()
    {
        return $"{base.ToString()}({Value})";
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueToken token && GetType() == obj?.GetType() && token.Value == Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Value);
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

public class And : OperatorToken;

public class Or : OperatorToken;

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

public abstract class Symbol(string value) : ValueToken(value)
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

public class Identifier(string value) : Symbol(value);

public class Keyword(string value) : Symbol(value)
{
    private static readonly string[] Keywords = ["while", "if", "else", "int", "str", "bool"];

    public static bool IsKeyword(string value)
    {
        return Keywords.Contains(value);
    }
}