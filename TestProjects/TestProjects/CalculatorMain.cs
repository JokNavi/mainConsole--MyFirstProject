
namespace ManyProjects
{
    public static class CalculatorMain
    {
        public static string OrderOfOperations()
        {
            Console.WriteLine("\n Input your desired compound expression.");
            //
            //2*2+3-2^6/2*2
            string CompoundExpression = "1*1*1-2+5^5/5*3/2+6";
            string LayeredExpression = "";
            string SimpleExpression = "7-32";
            string BracketExpression = "(2 * 2) + 3 - (((2^6) / 2) * 2)";
         
            //string OperatorS = CalculatorHelper.WhatOperator(LayeredExpression);
            //Console.WriteLine(CalculatorHelper.IsLayered(LayeredExpression));
            //Console.WriteLine(CalculatorHelper.EvaluateLayeredExpression(LayeredExpression, OperatorS));

            //Console.WriteLine(CalculatorHelper.IsSimple(SimpleExpression));
            //Console.WriteLine(CalculatorHelper.EvaluateSimpleExpression(SimpleExpression));
            
            //Console.WriteLine(CalculatorHelper.IsCompound(CompoundExpression));
            Console.WriteLine(CalculatorEvaluator.EvaluateCompound(CompoundExpression));

            //Console.WriteLine(CalculatorEvaluator.AddBrackets(CompoundExpression));
            //Console.WriteLine(CalculatorEvaluator.MaxBrackets(BracketExpression));
            //Console.WriteLine("Finished Calculation: "+CalculatorEvaluator.WorkOutBrackets(BracketExpression));
            
        
            return "Error";
        }
    }
}