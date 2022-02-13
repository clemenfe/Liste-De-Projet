using Calculatrice.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculatrice.Visitors
{
    public class PrintVisitor : IExpressionVisitor
    {

        
        private string _otherString;

        //Override du ToString
        public override string ToString()
        {

            return _otherString;
        }

        
        //L'expression est une expression
        public void Visit(Expression expr) {

            _otherString = expr.ToString();

        }

        //Arrive si l'expression est un nombre seul.
        public void Visit(Nombre nombre) {


            _otherString = nombre.ToString();

        }
    }
}
