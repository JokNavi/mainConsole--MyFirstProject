﻿using System.Text.RegularExpressions;

namespace ManyProjects
{
    public static class CalculatorHelper
    {
        const string SIMPLE_EXPRESSION_REGEX = "^[0-9]{1,50}[+-^*/]{1}[0-9]{50}$";
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

        //will add summary later
        //takes inputs and assigns hierarchy value based on brackets
        public static List<string> InterpretDoThisFirst(string Expression)
        {


            bool ContainsParentheses = false;
            bool ContainsExponents = false;
            bool ContainsMultiplications = false;
            bool ContainsAddition = false;
            bool ContainsSubtraction = false;
            bool ContainsSomething = false;
            int s = 0;

            Console.WriteLine("Expression: {0}", Expression);
            List<string> SubExpressions = new List<string>();
            

            SubExpressions = CalculatorHelper.InterpretParentheses(Expression);
            int i = SubExpressions.Count;
            int p = SubExpressions.Count;
            Console.WriteLine(i);
            
            while(s <  i)
            {
                ContainsParentheses = SubExpressions[s].Contains("(") || SubExpressions[s].Contains(")");
                if (ContainsParentheses)
                { SubExpressions.Insert(s, "M1M" + s + "M"); }
                else { ContainsSomething = true; };

                Console.WriteLine("SubExpressions: {0}", SubExpressions[s]);

                ContainsExponents = SubExpressions[s].Contains("^");
                if (ContainsParentheses)
                { SubExpressions.Insert(s,"M2M" + s + "M"); }
                else { ContainsSomething = true; };

                ContainsMultiplications = SubExpressions[s].Contains("*") || SubExpressions[s].Contains("/");
                if (ContainsParentheses)
                { SubExpressions.Insert(s, "M3M" + s + "M"); }
                else { ContainsSomething = true; };

                ContainsAddition = SubExpressions[s].Contains("+");
                if (ContainsParentheses)
                { SubExpressions.Insert(s, "M4M" + s + "M"); }
                else { ContainsSomething = true; };

                ContainsSubtraction = SubExpressions[s].Contains("-");
                if (ContainsParentheses)
                { SubExpressions.Insert(s, "M5M" + s + "M"); }
                else { ContainsSomething = true; };
                if( s == p)
                {
                    break;
                } 
            }
            bool ContainsClass(bool Contains)
            {
                if (ContainsSomething)
                { return true; }
                else
                { return false; }

            }
            
            return SubExpressions;

        }

        //will add summary later
        //just splits on the operators and puts into list
        public static List<string> InterpretOperators(string Expression)
        {
            List<string> SubExpressions = new List<string>();
            string Temp = string.Concat(Expression.Where(c => !char.IsWhiteSpace(c)));
            Temp = Temp.Replace("+", "R+R").Replace("-", "R-R").Replace("*", "R*R").Replace("/", "R/R");
            string[] TempArray1 = Temp.Split("R");

            for (int j = 0; j < TempArray1.Length; j++)
            {
                SubExpressions.Add(TempArray1[j]);
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


        public static bool IsSimple(string Expression)
        {
            Regex Tester = new Regex(SIMPLE_EXPRESSION_REGEX);
            return false;
        }
    }
}