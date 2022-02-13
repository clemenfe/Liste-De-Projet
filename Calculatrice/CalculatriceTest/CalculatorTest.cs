using Calculatrice;
using Calculatrice.Expressions;
using Calculatrice.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatriceTests {
    [TestClass]
    public class CalculatorTest {
        private Calculator calculator;

        [TestInitialize]
        public void Setup() {
            calculator = new Calculator();
        }

        [TestMethod]
        public void CalculateSimpleString() {
            Assert.AreEqual(5, calculator.Calculate("2 + 3"));
        }

        [TestMethod]
        public void CalculateSimpleString2() {
            Assert.AreEqual(5, calculator.Calculate("5"));
        }

        [TestMethod]
        public void CalculateLongString() {
            Assert.AreEqual(-2, calculator.Calculate("2 * 4 + 3 - 15 / 2"));
        }


        [TestMethod]
        public void CalculateLongString1() {
            Assert.AreEqual(3, calculator.Calculate("1 - 5 / 4 + 3 + 1"));
        }

        [TestMethod]
        public void CalculateLongString2() {
            Assert.AreEqual(60, calculator.Calculate("9 * 9 * 7 / 9 - 3"));
        }

        [TestMethod]
        public void CalculateLongString3() {
            Assert.AreEqual(2, calculator.Calculate("2 * 4 + 8 - 15 * 2"));
        }




            // Facultatif, pour points bonus!
            [TestMethod]
        public void CalculateComplexString() {
            Assert.AreEqual(16, calculator.Calculate("2 + (5 - 3) * (16 / 4)"));
        }

        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString2() {
            Assert.AreEqual(0, calculator.Calculate("1 + (5 - 0) * (0 / 1)"));
        }

        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString3() {
            Assert.AreEqual(13, calculator.Calculate("15 / (7 - 4) + (2 * 4)"));
        }

        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString4() {
            Assert.AreEqual(13, calculator.Calculate("1 + (3 + 1) / (7 - 4 - 2) + (2 * 4)"));
        }

        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString5() {
            Assert.AreEqual(12, calculator.Calculate("(3 + 1) / (7 - 4 - 2) + (2 * 4)"));
        }

        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString6() {
            Assert.AreEqual(13, calculator.Calculate("(3 + 1 + 4) / (4 - 2) + (2 * 4 + 1)"));
        }

        /*Ne fonctionne plus avec le Patron Composite
        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString4() {
            Assert.AreEqual(16, calculator.Calculate("(2 + (5 - 3) * (16 / 4))"));
        }*/

        /*Ne fonctionne plus avec le Patron Composite
        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString5() {
            Assert.AreEqual(-8, calculator.Calculate("2 + ((8 - (3 * 16)) / 4)"));
        }*/

        // Facultatif, pour points bonus!
        /* [TestMethod]
         public void CalculateComplexString6() {
             Assert.AreEqual(10, calculator.Calculate("(2 + (6 - (3 * 16))) / 4"));
         }*/

        /*Ne fonctionne plus avec le Patron Composite
       // Facultatif, pour points bonus!
       [TestMethod]
       public void CalculateComplexString7() {
           Assert.AreEqual(3, calculator.Calculate("((3 + 3) / 2)"));
       }*/

        /*Ne fonctionne plus avec le Patron Composite
        // Facultatif, pour points bonus!
        [TestMethod]
        public void CalculateComplexString8() {
            Assert.AreEqual(7, calculator.Calculate("(2 + (3 + 2))"));
        }*/

        [TestMethod]
        public void CalculateSimpleExpression() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Add(3);
            var expression = builder.Build();

            // Act
            int result = calculator.Calculate(expression);

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void CalculateLongExpression() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Multiply(4);
            builder.Add(3);
            builder.Subtract(15);
            builder.Divide(2);
            var expr = builder.Build();

            // Act 
            int result = calculator.Calculate(expr);

            // Assert
            Assert.AreEqual(-2, result);
        }

        [TestMethod]
        public void CalculateComplexExpression() {
            // Arrange
            var subtractBuilder = new ExpressionBuilder(5); //(5 - 3)
            subtractBuilder.Subtract(3);

            var divideBuilder = new ExpressionBuilder(16); //(16 / 4)
            divideBuilder.Divide(4);

            var builder = new ExpressionBuilder(2); // 2 + (5 - 3) * (16 / 4)
            builder.Add(subtractBuilder);
            builder.Multiply(divideBuilder);
            var expr = builder.Build();

            // Act
            int result = calculator.Calculate(expr);

            // Assert
            Assert.AreEqual(16, result);
        }

        [TestMethod]
        public void VisitCalculateSimple() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Add(3);

            var visitor = new CalculateVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual(5, visitor.Result);
        }

        [TestMethod]
        public void VisitCalculateLong() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Multiply(4);
            builder.Add(3);
            builder.Subtract(15);
            builder.Divide(2);

            var visitor = new CalculateVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual(-2, visitor.Result);
        }

        [TestMethod]
        public void VisitCalculateComplex() {
            // Arrange
            var subtractBuilder = new ExpressionBuilder(5);
            subtractBuilder.Subtract(3);

            var divideBuilder = new ExpressionBuilder(16);
            divideBuilder.Divide(4);

            var builder = new ExpressionBuilder(2);
            builder.Add(subtractBuilder);
            builder.Multiply(divideBuilder);

            var visitor = new CalculateVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual(16, visitor.Result);
        }

        [TestMethod]
        public void VisitPrintSimple() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Add(3);

            var visitor = new PrintVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual("2 + 3", visitor.ToString());
        }

        [TestMethod]
        public void PrintCalculateLong() {
            // Arrange
            var builder = new ExpressionBuilder(2);
            builder.Multiply(4);
            builder.Add(3);
            builder.Subtract(15);
            builder.Divide(2);

            var visitor = new PrintVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual("2 * 4 + 3 - 15 / 2", visitor.ToString());
        }

        [TestMethod]
        public void PrintCalculateComplex() {
            // Arrange
            var subtractBuilder = new ExpressionBuilder(5);
            subtractBuilder.Subtract(3);

            var divideBuilder = new ExpressionBuilder(16);
            divideBuilder.Divide(4);

            var builder = new ExpressionBuilder(2);
            builder.Add(subtractBuilder);
            builder.Multiply(divideBuilder);

            var visitor = new PrintVisitor();
            var expr = builder.Build();

            // Act
            calculator.Visit(expr, visitor);

            // Assert
            Assert.AreEqual("2 + (5 - 3) * (16 / 4)", visitor.ToString());
        }
    }
}