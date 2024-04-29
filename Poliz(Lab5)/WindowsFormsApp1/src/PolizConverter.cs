using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1.src;

namespace WindowsFormsApp1.src
{
    internal class PolizConverter
    {
        static int GetPriority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    return 0;
            }
        }
        public static string InfixToPostfix(string infix)
        {
            string postfix = "";
            Stack<char> operatorStack = new Stack<char>();
            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];
                if (char.IsDigit(c))
                {
                    // Считываем двузначное число
                    string number = c.ToString();
                    while (i + 1 < infix.Length && char.IsDigit(infix[i + 1]))
                    {
                        number += infix[i + 1];
                        i++;
                    }
                    postfix += number + " ";
                }
                else if (c == '(')
                {
                    char ch = infix[i + 1];
                    if (ch == ')')
                    {
                        MessageBox.Show("Встретились пустые скобки !!!");
                        return null;
                    }
                    operatorStack.Push(c);
                }
                else if (c == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        postfix += operatorStack.Pop() + " ";
                    }
                    operatorStack.Pop(); // Извлекаем открывающую скобку из стека
                }
                else if (IsOperator(c))
                {
                    while (operatorStack.Count > 0 && GetPriority(operatorStack.Peek()) >= GetPriority(c))
                    {
                        postfix += operatorStack.Pop() + " ";
                    }
                    operatorStack.Push(c);
                }
                else
                {
                    MessageBox.Show("Встретился недопустимый символ " +"\'"+ c +"\'" + " !!!");
                    return null;
                }

            }
            while (operatorStack.Count > 0)
            {
                postfix += operatorStack.Pop() + " ";
            }


            PolizItem polizItem = new PolizItem();
            polizItem.Result = PolizSolver.EvaluatePostfix(postfix.Trim());

            //return postfix.Trim();
            MessageBox.Show( postfix.Trim() +  " = " + polizItem.Result.ToString());
            return polizItem.Result.ToString();
        }
        static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
        }
    }
}
