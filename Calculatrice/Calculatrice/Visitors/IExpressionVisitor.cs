using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculatrice.Expressions;

namespace Calculatrice.Visitors
{
    public interface IExpressionVisitor
    {

        public void Visit(Expression expr); //Les fonctions Visit (Patron visiteur)
        public void Visit(Nombre nombre);

    }
}
