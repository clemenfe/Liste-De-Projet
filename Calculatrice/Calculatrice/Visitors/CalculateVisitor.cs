using Calculatrice.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice.Visitors
{
    public class CalculateVisitor : IExpressionVisitor
    {
        public int Result { get; set; }

        private Stack<Nombre> _stackNumber = new();
        private Stack<char> _stackChar = new();

        //Lorsqu'on visite un nombre
        public void Visit(Nombre nombre) {


            _stackNumber.Push(nombre);
            Result = nombre.ToInt32(); //Seulement si on veut considérer un nombre comme une expression. (voir CalculateSimpleString2 de MA feuille de test).
        }

        //Lorsqu'on visite une expression
        public void Visit(Expression expression) {

            expression.Left.Accept(this); //Le visiteur visite la gauche et la droite donc il va visité toute les branches de l'arbre pour atteindre les feuilles (Dans notre cas, les nombres)
            expression.Right.Accept(this);

            _stackChar.Push(expression.operateur); //On garde l'opérateur dans le stack


            if (_stackNumber.Count() > _stackChar.Count()) {

                const int NB_OPERATOR = 2; //Si dans un futurs on décide que les opérations s'applique sur un nombre différent de chiffre, on aurait qu'à modifier la valeur de cette constante et à modifier les opérations précises
                int[] operande = new int[NB_OPERATOR];

                //On y va de façon descendante pour contrer l'effet qu'une opération est effectué à l'envers. À cause que le stack redonne le dernier élément du dessus
                for (int i = NB_OPERATOR; i > 0; i--) {

                    operande[i - 1] = _stackNumber.Pop().ToInt32(); //On met le chiffre dans la variable operande

                }

                //Pour effectuer l'opération mathématique.
                switch (_stackChar.Pop()) {


                    //Addition
                    case '+':

                        //On effectue l'opération.
                        Result = operande[0] + operande[1];

                        break;

                    //Soustraction
                    case '-':

                        //On effectue l'opération.
                        Result = operande[0] - operande[1];

                        break;

                    case '*':

                        //On effectue l'opération.
                        Result = operande[0] * operande[1];

                        break;


                    case '/':

                        //On effectue l'opération.
                        Result = operande[0] / operande[1];

                        break;

                }

                _stackNumber.Push(new Nombre(Result)); //On sauvegarde le résultat dans la pile

            }
        }

    }

   }
        




        /*MÉTHODE AVANT L'AJOUT DU PATRON COMPOSITE.
        //On évalue l'expression
        private int _EvalPostfix(IExpression expression) {

            const int NB_OPERATOR = 2;
            int[] number = new int[NB_OPERATOR]; //2 dimension, car on applique l'opérateur sur deux opérandes
            string operateur;

            List<string> expressionPostfix = _ConvertToPostfix(expression.ToString()).ToList(); //À cause de la récursivité de la fonction, il est sûrement judicieux de stocké le résultat.
            _ = int.TryParse(expressionPostfix.ElementAt(0), out number[0]); //Si l'expression est juste un nombre

            //On boucle tant que l'expression contient plus d'un element
            while (expressionPostfix.Count > 1) {

                int indexOfOperator = expressionPostfix.FindIndex(x => !int.TryParse(x.ToString(), out _)); //On trouve le premier opérateur (en partant de la gauche)

                for(int i = NB_OPERATOR, j = 0; i > 0; i--, j++)
                    number[j] = int.Parse(expressionPostfix.ElementAt(indexOfOperator - i)); //-2 car une opération s'applique sur deux termes et en postfix, les termes sont devants les opérandes.
                //number[1] = int.Parse(expressionPostfix.ElementAt(indexOfOperator - 1)); //-1 car une opération s'applique sur deux termes et en postfix, les termes sont devants les opérandes.
                operateur = expressionPostfix.ElementAt(indexOfOperator); //On trouve l'opérateur


                //Pour effectuer l'opération mathématique.
                switch (operateur) {

                    //Addition
                    case "+":

                        //On effectue l'opération.
                        number[0] = number[0] + number[1];

                        break;

                    //Soustraction
                    case "-":

                        //On effectue l'opération.
                        number[0] = number[0] - number[1];

                        break;

                    case "*":

                        //On effectue l'opération.
                        number[0] = number[0] * number[1];

                        break;


                    case "/":

                        //On effectue l'opération.
                        number[0] = number[0] / number[1];

                        break;

                }

                expressionPostfix[indexOfOperator] = number[0].ToString(); //On remplace l'opérateur par le résultat de l'opération

                //On supprime les éléments, car ils ont été substitué par un autre.
                for (int i = indexOfOperator - 1; i >= indexOfOperator - NB_OPERATOR; i--)
                    expressionPostfix.RemoveAt(i); 

            }

            return number[0]; 
        }

        //Dans cette partie, on séprare les éléments qui compose le string
        private IEnumerable<string> _ConvertToPostfix(string expressionInfix) {


            var tokens = expressionInfix.Split(' ');


            return _ConvertToPostfix(tokens);


        }



        //On convertie en PostFix.
        private IEnumerable<string> _ConvertToPostfix(IEnumerable<string> tokens) {

            var postfixStack = new Stack<string>();
            var operatorStack = new Stack<string>();

            int i = 1; //variable compteur

            int continueHowManyTimes = 0;

            //Le premier est toujours un chiffre donc on le skip
            foreach (var token in tokens) {


                //On skip des itérations. (Ce produit lorsqu'on reçoit une évaluation Postfix de la récursivité.
                if (continueHowManyTimes > 0) {

                    continueHowManyTimes--;

                    i++;

                    continue;

                }



                //Si on réussi à parse, c'est un chiffre
                if (int.TryParse(token, out var _)) {
                    postfixStack.Push(token);


                    if (operatorStack.Any())
                        postfixStack.Push(operatorStack.Pop()); //On ajoute l'opérateur au Postfix stack tout en l'enlevant du operatorStack.
                }
                else if (!token.StartsWith("(") && !token.EndsWith(")")) {
                    operatorStack.Push(token);
                }

                else if (token.StartsWith("(")) {

                    int j = 0;

                    //Le while est utile si on a plusieurs parenthès ouvrante (Exemple : ((2 + 3) / 2);) NOTE : CE N'EST PAS TOUS LES TYPES DE PARENTHÈSES IMBRIQUÉES QUI SONT PRÉSENTEMENT GÉRÉ PAR MON CODE.
                    while (token.ElementAt(j).Equals('('))
                        j++; //On veut compter le nombre de paranthès fermante qui suive le chiffre.

                    postfixStack.Push(token[j..]); //On enlève la parenthèse ouvrante. Elle est positionné à la position 0 du string. Donc on garde ce qui est à partir de la position 1. Et on stock le chiffre restant dans le stack.

                    //string miniExpressionPostfix = string.Join("", _ConvertToPostfix(tokens.Skip(i))); //On "converti" l'IEnumerable en string
                    IEnumerable<string> miniExpressionPostfix = _ConvertToPostfix(tokens.Skip(i)); //Si on met en string comme je faisait précédemment (ligne commenté), on se retrouve avec l'expression dans un seul emplacement du stack.

                    //On ajoute chaque terme au stack.
                    foreach (var miniEx in miniExpressionPostfix)
                        postfixStack.Push(miniEx); //On met l'expression obtenue dans le stack.

                    continueHowManyTimes = miniExpressionPostfix.Count(); //On skip le nombre d'intération de miniExpressionPostfix, car ils ont été faites dans une autres récursion.

                     if (operatorStack.Any())
                        postfixStack.Push(operatorStack.Pop()); //On ajoute l'opérateur au Postfix stack tout en l'enlevant du operatorStack.

                }

                // token == ) 
                else {

                    int j = 0;

                    //Le while est utile si on a plusieurs parenthès fermante (Exemple : (2 + (3 + 2));) NOTE : CE N'EST PAS TOUS LES TYPES DE PARENTHÈSES IMBRIQUÉES QUI SONT PRÉSENTEMENT GÉRÉ PAR MON CODE.
                    while (token.Reverse().ElementAt(j).Equals(')'))
                        j++; //On veut compter le nombre de paranthès fermante qui suive le chiffre.

                    postfixStack.Push(string.Join("", token.SkipLast(j))); //On ne veut pas le dernier élément, car c'est une paranthèse fermante.

                    if (operatorStack.Any())
                        postfixStack.Push(operatorStack.Pop()); //On ajoute l'opérateur au Postfix stack tout en l'enlevant du operatorStack.

                    return postfixStack.Reverse();
                }


                i++; //On augmente le compteur.
            }

            //Si le stack n'est pas vide, on le vide.
            /*while (operatorStack.Any())
                postfixStack.Push(operatorStack.Pop()); //On ajoute l'opérateur au Postfix stack tout en l'enlevant du operatorStack.

            return postfixStack.Reverse();
        }


        */



        //On évalue l'expression
        //VERSION SANS GESTION PARANTHÈSE
        /*
        private int _EvalPostfix(IExpression expression) {

            int[] number = new int[2]; //2 dimension, car on applique l'opérateur sur deux opérandes

            number[0] = int.Parse(expression.ConvertToPostfix().First()); //En notation Postfix, le premier élément est toujours un chiffre.

            //On boucle pour chaque char dans l'expression
            foreach (var value in expression.ConvertToPostfix().Skip(1)) {


                //Si c'est un chiffre, on le converti et on le stock.
                if (int.TryParse(value.ToString(), out int resultTemp)) {

                    number[1] = resultTemp;


                }

                //C'est un opérateur
                else {

                    switch (value) {

                        //Addition
                        case "+":

                            //On effectue l'opération.
                            number[0] = number[0] + number[1];

                            break;

                        //Soustraction
                        case "-":

                            //On effectue l'opération.
                            number[0] = number[0] - number[1];

                            break;

                        case "*":

                            //On effectue l'opération.
                            number[0] = number[0] * number[1];

                            break;


                        case "/":

                            //On effectue l'opération.
                            number[0] = number[0] / number[1];

                            break;


                    }


                }
            }

            return number[0];


        }*/




    

