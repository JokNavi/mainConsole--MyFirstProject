﻿using System.Text.RegularExpressions;

namespace ManyProjects
{
    public static class CalculatorHelper
    {
        const string SIMPLE_EXPRESSION_REGEX = "^[0-9]{1,50}[\\.,]{0,1}[0-9]{0,50}[+\\-^*/]{1}[0-9]{1,50}[\\.,]{0,1}[0-9]{0,50}$";

        static CalculatorHelper()
        {

        }
        /// <summary>
        /// This method takes in a single line expression string, removes all whitespace, and separates expressions inside of parentheses.
        /// </summary>
        /// <param name="Expression"></param> is the mathematical expression that the user input
        /// <returns>List/<String/> SubExpressions</returns> the list of sub-expressions within the expression.
        public static List<string> InterpretParentheses(string Expression)
        {
           List<string> SubExpressions = new List<string>();
            string Temp = string.Concat(Expression.Where(c => !char.IsWhiteSpace(c)));
            Temp = Temp.Replace(")", ")R").Replace("(", "R(");

            string[] tempArray1 = Temp.Split("R");

            string[] TempArray2;
            for (int i = 0; i < tempArray1.Length; i++)
            {
                TempArray2 = tempArray1[i].Split("R");
                for (int j = 0; j < TempArray2.Length; j++)
                {
                    SubExpressions.Add(TempArray2[j]);
                }

            }

            return SubExpressions;
        }


        /// <summary>
        /// Evaluates a "simple expression" with the +,-,*,/,^ operations. If the calculation goes wrong, the function will return int.MinValue.
        /// Example: "2.317^4.2" will return a value of 34.09489.
        /// </summary>
        /// <param name="Expression"></param> A simple expression as a string in the form (number)(operator)(number)
        /// <returns></returns> Returns the solution of the simple expression as a float.
        public static float EvaluateSimpleExpression(string Expression)
        {
            float Value1 = 0, Value2 = 0;
            int OperatorLocation = 0;
            string[] Operators = { "+", "-", "*", "/", "^" };
            int index = 0;

            while ((OperatorLocation == 0) && index < Operators.Length)
            {
                if (Expression.IndexOf(Operators[index]) == -1)
                    index++;
                else
                    OperatorLocation = Expression.IndexOf(Operators[index]);
            }

            Value1 = float.Parse(Expression.Substring(0, OperatorLocation));
            Value2 = float.Parse(Expression.Substring(OperatorLocation + 1));
            string Operator = Expression.Substring(OperatorLocation, 1);

            if (Operator.Equals("+"))
            {
                return Value1 + Value2;
            }
            else if (Operator.Equals("-"))
            {
                return Value1 - Value2;
            }
            else if (Operator.Equals("*"))
            {
                return Value1 * Value2;
            }
            else if (Operator.Equals("/"))
            {
                return Value1 / Value2;
            }
            else if (Operator.Equals("^"))
            {
                return (float)Math.Pow(Value1, Value2);
            }
            return float.MinValue;//If the function is used correctly, this statement should never occur.
        }

        /// <summary>
        /// Determines if a given string Expression is "Simple" or not.
        /// </summary>
        /// <param name="Expression".</param> Any string expression.
        /// <returns> true if the expression is simple. </returns>
        public static bool IsSimple(string Expression)
        {
            Regex Tester = new Regex(SIMPLE_EXPRESSION_REGEX);
            return Tester.IsMatch(Expression);
        }
        /// <summary>
        /// Determines if a given string Expression is "layered" or not.
        /// </summary>
        /// <param name="Expression".</param> Any string expression.
        /// <returns> true if the expression is layered. </returns>
        public static bool IsLayered(string Expression)
        {
            var arr = Regex.Matches(Expression, "[+^*/-]").OfType<Match>().Select(m => m.Value).ToArray();
            bool isAllEqual = arr.Distinct().Count() == 1;
            if (isAllEqual) { return true; }
            return false;
        }

        /// <summary>
        /// Determines the operator from a given string.
        /// </summary>
        /// <param name="Expression".</param> Any string expression.
        /// <returns> returns the operator from the string if IsMultipleOperators is false .</returns>
        public static string WhatOperator(string Expression, ref string OperatorS)
        {

            bool HasMultipleOperators = CalculatorHelper.IsLayered(Expression);
            if (HasMultipleOperators)
            {
                var arr = Regex.Matches(Expression, "[+-¨*/]").OfType<Match>().Select(m => m.Value).ToArray();
                OperatorS = arr[1];
                return OperatorS;
            }
            return "Error";
        }

        /// <summary>
        /// (BROKEN) Evaluates a "layered expression". A "layered" expresssion consists of a string of mathematical operations with only 1 operation type such as 5*5*5*5.
        /// </summary>
        /// <param name="Expression"></param> A string expression with one operation type.
        /// <returns></returns> returns the value of the float
        public static float EvaluateLayeredExpression(string Expression)
        {

            string temp = "";
            string simplePart = "";
            string expressionOperator = "";
            int operatorIndex = 0;
            string[] operators = { "+", "-", "*", "/", "^" };
            for (int i = 0; i < operators.Length; i++)//determine which operator the Layered Expression uses.
            {
                if (Expression.Contains(operators[i]))
                {
                    operatorIndex = i;
                    break;
                }
            }

            expressionOperator = operators[operatorIndex];

            while (!(IsSimple(Expression)))//loop until reduced to a simple expression.
            {
                temp = Expression.Substring(0, Expression.IndexOf(expressionOperator) + 1);
                Expression = Expression.Remove(0, temp.Length);
                simplePart = String.Concat(temp, Expression.Substring(0, Expression.IndexOf(expressionOperator)));
                Expression = Expression.Remove(0, Expression.IndexOf(expressionOperator));
                temp = EvaluateSimpleExpression(simplePart).ToString();
                Expression = String.Concat(temp, Expression);
            }

            return EvaluateSimpleExpression(Expression);
        }

    }
}