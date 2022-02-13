using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculatrice.Visitors;

namespace Calculatrice.Expressions {
    public class Nombre : IExpression {

        private readonly int _number;
       
        public Nombre(int number) {

            _number = number;
        }

        //On accept le visiteur (Patron visiteur)
        public void Accept(IExpressionVisitor visitor) {

            visitor.Visit(this); //Le visiteur visite c'est objet.
        }


        //Pour obtenir le nombre
        public override string ToString() {
            return _number.ToString();
        }

        //On obtient le int du nombre
        public int ToInt32() {

            return _number;

        }
    }
}
