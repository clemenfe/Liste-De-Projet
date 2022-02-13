using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice.Expressions {
    static class ExpressionConverter {

        
        //On convertit un String en IExpression à l'aide du ExpressionBuilder
        public static IExpression StringToExpression(string input) {

            ExpressionBuilder expressionBuilder = null; //Pour que le programme ne chiale pas qu'il n'est pas initialisé

            var tokens = input.Split(' '); //Je sépare les différents éléments qui compose le string

            //Le expressionBuilder référencer celui qu'on calcule dans cette fonction
            expressionBuilder = _TempBuilder(expressionBuilder, tokens.ToList(), false);
            

            return expressionBuilder.Build();
        }



        //Fonction Récursive pour pouvoir gérer les parenthèses
        //On construit un builder pour pouvoir gérer les parenthèses. (parenthese est true lorsqu'on est dans une récursion).
        private static ExpressionBuilder _TempBuilder(ExpressionBuilder expressionBuilder, List<string> tokens, bool parenthese) {


            ExpressionBuilder sousBuilder = null; //On initialise le SousBuilder a null pour empêcher le programme de chialer.
            int number; //Représente le nombre dans l'expression

            Stack<char> operateur = new(); //Va contenir les operateur


            //On boucle sur tous les tokens 
            for (int i = 0; i < tokens.Count(); i++) {

                //Sur la première itération
                if (i == 0) {

                    //Si ça commence par une parenthèse
                    if (tokens.ElementAt(0).StartsWith("(")) {

                        tokens[0] = string.Join("", tokens[0].Skip(1)); //On enlève le premier charactère du premier élément dans la liste, car c'est une parenthèse.

                        if (!parenthese) { //Va s'exécuter seulement si l'expression commence par une parenthèse dans les tests.

                            i = tokens.FindIndex(x => x.EndsWith(')')); //On met i à la valeur ou on ferme la parenthèse, car ça l'a été calculé.

                            expressionBuilder = _TempBuilder(expressionBuilder, tokens, true);

                            

                        }

                        //Si on est dans une récursion, on crée un nouveau sousBuilder
                        else
                            sousBuilder = new(int.Parse(tokens.ElementAt(0).ToString()));
                    }

                    //Ça commence par un chiffre. 
                    else {

                        //Si paranthèse est faux, on crée un nouveau Builder associé à la variable expressionBuilder
                        if(!parenthese)
                        expressionBuilder = new(int.Parse(tokens.ElementAt(0).ToString()));

                        //Sinon le nouveau Builder sera initialisé à la variable sousBuilder
                        else
                            sousBuilder = new(int.Parse(tokens.ElementAt(0).ToString()));
                    }
                }

                //Lorsqu'on n'est pas à la première itération
                else {


                    //On essaie de convertir. Si ça marche, on sauvegarde la donnée et on passe à la prochaine itération.
                    if (int.TryParse(tokens[i], out number) || (sousBuilder is not null && !parenthese && operateur.Any())) {
                                                                //S'il le sousBuilder est initialisé, qu'il y a un opérauteur et qu'on n'est pas dans une récursion,
                                                                //ça veut dire qu'on ajoute la sousExpression au ExpressoinBuilder.
                        

                        //On regarde si l'élément fait partie des opérateurs*. 
                        switch (operateur.Pop()) {

                            //Addition
                            case '+':

                                //Lorsqu'on n'est pas dans une sous expression
                                if (!parenthese && sousBuilder is null)
                                    expressionBuilder.Add(number);

                                //Si on est dans une récursion 
                                else if (parenthese)
                                    sousBuilder.Add(number);


                                //On ajoute la sousExpression au ExpressoinBuilder.
                                else {

                                    expressionBuilder.Add(sousBuilder);
                                    sousBuilder = null; //On remet le sousBuilder a null
                                }

                                break;

                            //Soustraction
                            case '-':

                                if (!parenthese && sousBuilder is null)
                                    expressionBuilder.Subtract(number);

                                else if (parenthese)
                                    sousBuilder.Subtract(number);

                                else {

                                    expressionBuilder.Subtract(sousBuilder);
                                    sousBuilder = null; //On remet le sousBuilder a null
                                }

                                break;

                            case '*':

                                if (!parenthese && sousBuilder is null)
                                    expressionBuilder.Multiply(number);

                                else if (parenthese)
                                    sousBuilder.Multiply(number);

                                
                                else {

                                    expressionBuilder.Multiply(sousBuilder);
                                    sousBuilder = null; //On remet le sousBuilder a null
                                }

                                break;


                            case '/':

                                if (!parenthese && sousBuilder is null)
                                    expressionBuilder.Divide(number);

                                else if (parenthese)
                                    sousBuilder.Divide(number);

                                else {

                                    expressionBuilder.Divide(sousBuilder);
                                    //i--; //Car on vient de passer sur l'itération d'un autre chiffre.
                                    sousBuilder = null; //On remet le sousBuilder a null
                                }

                                break;
                                                          
                        }

                    }

                    //Si l'élément dans le stack est une parenthèse fermante, on sort de la récursion
                    else if (operateur.Any() && operateur.Peek().Equals(')'))
                        return sousBuilder;

                    //Sinon
                    else {

                        //Si l'élément commence avec une parenthèse, on instancie sousBuilder en créant une récursion de cette fonction
                        if (tokens.ElementAt(i).StartsWith('(')) {
                            sousBuilder = _TempBuilder(expressionBuilder, tokens.Skip(i).ToList(), true); //On skip les éléments qui ont déjà été ajouté au expressionBuilder.

                            i = tokens.FindIndex(x => x.EndsWith(')')); //On place la variable i à l'itération où nous sommes rendu soit;
                            tokens[i] = string.Join("", tokens[i].SkipLast(1)); //On enlève la parenthèse. (Sinon on tombe dans une boucle infini, car FindIndex cherche le premier élément respectant la condition)

                            
                            i--; //On contre le i++ de la boucle et on premet d'ajouter le sous builder au expression builder en passant sur l'itération du chiffre qui a la parenthèse.

                           
                               
                        }

                        //Si l'élément (le nombre) ce termine par une parenthèse
                        else if (tokens.ElementAt(i).EndsWith(')')) {

                            operateur.Push(tokens.ElementAt(i).Last()); //On ajoute la parenthèse au Stack Operateur

                            tokens[i] = string.Join("", tokens[i].SkipLast(1)); //On enlève la parenthèse.
                            i--; //Pour revenir à cette itération.

                           //On crée un stack temporaire pour stocker les données du stack operateur
                            Stack<char> tempStack = new();
                            char tempChar = ' '; //Initialisé pour empêcher le programme de chialer.

                            //On met la parenthèse en dessous du stack
                            //On boucle tant que le Stack Operateur n'est pas vide
                            while (operateur.Any()) {

                                //Si l'opérateur n'est pas une parenthèse fermante
                                if (!operateur.Peek().Equals(')'))
                                    tempStack.Push(operateur.Pop()); //On envoie les opérateurs dans le stack temporaire
                                //C'est une parenthèse fermante.
                                else
                                    tempChar = operateur.Pop(); //On initialise le tempChar avec le Pop du operateur
                            } 

                            //On boucle tant qu'on a pas vidé notre stack temporaire
                            while (tempStack.Any()) {

                                //Si le stack opérateur est vide, on y ajoute la parenthèse fermante.
                                if (!operateur.Any())
                                    operateur.Push(tempChar);
                                //Sinon, on pop le stack temporaire dans le stack opérateur
                                else
                                    operateur.Push(tempStack.Pop());
                            }


                        }

                        //Si c'est un opérateur
                        else
                            //On ajoute l'élément au stack. On regarde le premier char pour gérer les parenthèses. Car une parenthèse est toujours suivi d'un chiffre.
                            operateur.Push(tokens.ElementAt(i).First());
                        
                    }

                }



            }

            //Si on n'est pas dans une récursion, on retourne le ExpressionBuilder.
            if (!parenthese)
                return expressionBuilder;

            //Sinon, on retourne la sous expression
            else
                return sousBuilder;

        }

    }
}
