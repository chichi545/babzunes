using System;
using System.Collections.Generic;

public class Token
{
    public string Value { get; set; }

    public Token(string value)
    {
        Value = value;
    }
}

public class Parenthesis : Token
{
    public Parenthesis(string value) : base(value)
    {
    }
}

public class Number : Token
{
    public int NumValue { get; set; }

    public Number(string value) : base(value)
    {
        if (int.TryParse(value, out int numValue))
        {
            NumValue = numValue;
        }
        else
        {
            throw new ArgumentException("Invalid numeric value");
        }
    }
}

public class Operation : Token
{
    public char Symbol { get; set; }
    public int Priority { get; set; }

    public Operation(string value, char symbol, int priority) : base(value)
    {
        Symbol = symbol;
        Priority = priority;
    }

    public static bool operator ==(Operation operation1, Operation operation2)
    {
        return operation1.Symbol == operation2.Symbol && operation1.Priority == operation2.Priority;
    }

    public static bool operator !=(Operation operation1, Operation operation2)
    {
        return !(operation1 == operation2);
    }

    public static bool operator >(Operation operation1, Operation operation2)
    {
        return operation1.Priority > operation2.Priority;
    }

    public static bool operator <(Operation operation1, Operation operation2)
    {
        return operation1.Priority < operation2.Priority;
    }
}

class Program
{
    static void Main()
    {
        List<Token> tokens = new List<Token>
        {
            new Number("123"),
            new Operation("+", '+', 1),
            new Parenthesis("("),
            new Operation("*", '*', 2),
            new Number("456"),
            new Parenthesis(")"),
            new Operation("-", '-', 1),
            new Number("789")
        };

        foreach (var token in tokens)
        {
            Console.WriteLine(token.Value);
        }
    }
}
