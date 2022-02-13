using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculatrice.Visitors;

namespace Calculatrice.Expressions {

    public class Expression : IExpression {


        public readonly IExpression Left; //Patron Composite
        public readonly IExpression Right;
        public readonly char operateur;

        //On accepte le visiteur (Patron visiteur)
        public void Accept(IExpressionVisitor visitor) {

            visitor.Visit(this); //On visit cette expression
        }
       

        public Expression(IExpression RightExpression, char operateur, IExpression LeftExpression) {

            Right = RightExpression;
            this.operateur = operateur;
            Left = LeftExpression;

        }

        public override string ToString() {

            
            StringBuilder strBuilder = new(); //On créer un StringBuilder qui se chargera de construire un String

            //On ajoute L'expression de gauche au StringBuilder.
            strBuilder.Append(Left.ToString());
            strBuilder.Append(' ');
            strBuilder.Append(operateur); //On sépare l'opérateur par des espaces.
            strBuilder.Append(' ');

            //Si le membre de droite est une expression, on encadre l'expression de parenthèse
            if (Right is Expression) {
                strBuilder.Append('(');
                strBuilder.Append(Right.ToString());
                strBuilder.Append(')');
            }

            //Sinon, on fait juste ajouter le membre de droite au String Builder.
            else
                strBuilder.Append(Right.ToString());

            return strBuilder.ToString();



            
        }

       




    }
}
