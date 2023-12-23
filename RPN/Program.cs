using System;
using System.Collections.Generic;

class RPNCalculator
{
    static void Main()
    {
        string input = Console.ReadLine();

        List<object> tokens = Tokenize(input);
        Console.WriteLine("Токены выражения:");
        PrintTokens(tokens);

        List<object> rpnTokens = ConvertToRPN(tokens);
        Console.WriteLine("Токены в обратной польской записи (ОПЗ):");
        PrintTokens(rpnTokens);

        double result = CalculateRPN(rpnTokens);
        Console.WriteLine($"Результат выражения: {result}");
    }

    static List<object> Tokenize(string expression)
    {
        List<object> tokens = new List<object>();
        char[] operators = { '+', '-', '*', '/' };

        int i = 0;
        while (i < expression.Length)
        {
            if (char.IsDigit(expression[i]))
            {
                string number = "";
                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number += expression[i];
                    i++;
                }
                tokens.Add(double.Parse(number));
            }
            else if (Array.IndexOf(operators, expression[i]) != -1)
            {
                tokens.Add(expression[i]);
                i++;
            }
            else
            {
                i++;
            }
        }

        return tokens;
    }

    static List<object> ConvertToRPN(List<object> tokens)
    {
        List<object> rpnTokens = new List<object>();
        Stack<char> operators = new Stack<char>();

        foreach (var token in tokens)
        {
            if (token is double)
            {
                rpnTokens.Add(token);
            }
            else if (token is char)
            {
                while (operators.Count > 0 && Priority((char)token) <= Priority(operators.Peek()))
                {
                    rpnTokens.Add(operators.Pop());
                }
                operators.Push((char)token);
            }
        }

        while (operators.Count > 0)
        {
            rpnTokens.Add(operators.Pop());
        }

        return rpnTokens;
    }

    static double CalculateRPN(List<object> rpnTokens)
    {
        Stack<double> stack = new Stack<double>();

        foreach (var token in rpnTokens)
        {
            if (token is double)
            {
                stack.Push((double)token);
            }
            else if (token is char)
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();

                switch ((char)token)
                {
                    case '+':
                        stack.Push(operand1 + operand2);
                        break;
                    case '-':
                        stack.Push(operand1 - operand2);
                        break;
                    case '*':
                        stack.Push(operand1 * operand2);
                        break;
                    case '/':
                        stack.Push(operand1 / operand2);
                        break;
                }
            }
        }

        return stack.Pop();
    }

    static int Priority(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            default:
                return 0;
        }
    }

    static void PrintTokens(List<object> tokens)
    {
        foreach (var token in tokens)
        {
            Console.Write($"{token} ");
        }
        Console.WriteLine();
    }
}
