using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice.Expressions
{
    public class ExpressionBuilder  //Patron Builder
    {

        private IExpression _expression; //Permet d'utiliser le patron Composite.



        public ExpressionBuilder(int initial) {



            _expression = new Nombre(initial);
            
        }

        public void Add(int n) {

            //_AppendExpression(n, "+");//On représente une addition dans un String
            _expression = new Expression(new Nombre(n), '+', _expression); //On applique le patron Composite dans le patron Builder

        }
        public void Add(ExpressionBuilder e) {

            //_AppendExpression(e, "+"); //On représente l'addition en ajoutant le string qui représente l'expression passé en paramètre
            _expression = new Expression(e._expression, '+', _expression); //On applique le patron Composite dans le patron Builder

        }

        public void Subtract(int n) {

            _expression = new Expression(new Nombre(n), '-', _expression);

        }
        public void Subtract(ExpressionBuilder e) {

            _expression = new Expression(e._expression, '-', _expression); //On applique le patron Composite dans le patron Builder

        }

        public void Multiply(int n) {

            _expression = new Expression(new Nombre(n), '*', _expression);

        }
        public void Multiply(ExpressionBuilder e) {

            _expression = new Expression(e._expression, '*', _expression); //On applique le patron Composite dans le patron Builder

        }

        public void Divide(int n) {

            _expression = new Expression(new Nombre(n), '/', _expression);


        }
        public void Divide(ExpressionBuilder e) {

            _expression = new Expression(e._expression, '/', _expression); //On applique le patron Composite dans le patron Builder

        }

        public IExpression Build()
        {
            

            
            return _expression; //On retourne l'IExpression
        }
                                  
        
    }
}
