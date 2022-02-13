using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculatrice.Visitors;

namespace Calculatrice.Expressions
{
    public interface IExpression
    {
        //On Accept le visiteur
        public void Accept(IExpressionVisitor visitor);

       
    }
}
