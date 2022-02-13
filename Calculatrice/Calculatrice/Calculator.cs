using Calculatrice.Expressions;
using Calculatrice.Visitors;
using System;

namespace Calculatrice
{

    //Sert de Façade pour les calculs (Patron façade)
    public class Calculator
    {



        public int Calculate(string input)
        {

            //On converti un String en IExpression            
            IExpression expression = ExpressionConverter.StringToExpression(input);


            return Calculate(expression);
        }

        public int Calculate(IExpression expr)
        {
            CalculateVisitor visitor = new();

            Visit(expr, visitor); //On visite l'expression

            return visitor.Result;
        }

        public void Visit(IExpression expr, IExpressionVisitor visitor) {

            expr.Accept(visitor); //On accepte le visiteur

        }

        
    }
}
