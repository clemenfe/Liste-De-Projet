using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travail_Pratique_Matrice {


    class Systeme {

        private readonly Matrice _matriceCoefficiant, _matriceResultat;
        

        

       /* //Consructeur (Selon l'ancienne méthode où on faisant sel = new(_CreerMatrice(), _CreerMatrice());
        public Systeme(Matrice matrice, Matrice result) {

            //La matrice de résultat n'a pas le choix d'être m X 1
            if (result.GetData().GetLength(1) != 1)
                throw new Exception();

            //Si les matrices sont de taille différente
            else if (result.GetData().GetLength(0) != matrice.GetData().GetLength(0))
                throw new Exception();

            _matriceCoefficiant = matrice;
            _matriceResultat = result;

            

        }*/

        //Constructeur (Fonction avec la fonction _CreerMatriceSEL())
        public Systeme ((Matrice, Matrice) tupleMatrix) {

            _matriceCoefficiant = tupleMatrix.Item1;
            _matriceResultat = tupleMatrix.Item2;

        }


        //On override la méthode To String
        public override string ToString() {

            string strSystem = ""; //Variable qui va contenir nos équations

            //On boucle sur les rangées
            for (int i = 0; i < _matriceCoefficiant.GetData().GetLength(0); i++) {

                //On boucle sur les colonnes
                for (int j = 0; j < _matriceCoefficiant.GetData().GetLength(1); j++) {

                    //On affiche pas les 0, car 0 * x = 0
                    if (_matriceCoefficiant.GetData()[i, j] == 0 && j != _matriceCoefficiant.GetData().GetLength(1) - 1)

                        continue; //On passe à la prochaine itération

                    //Si le dernier élément de la rangée est 0
                    else if (_matriceCoefficiant.GetData()[i, j] == 0 && j == _matriceCoefficiant.GetData().GetLength(1) - 1) {
                        
                        strSystem += $"= {_matriceResultat.GetData()[i, 0]}\n";
                        continue;
                    }

                    //C'est un chiffre positif
                    else if (_matriceCoefficiant.GetData()[i, j] > 0) {

                        //On met un + devant le chiffre (on ne le fait pas à la première itération)
                        if (i != 0 && j != 0) {

                            //Si à l'itération précédente on n'a pas 0, on ajoute un +
                            if(strSystem.ToCharArray()[^1].ToString() != "\n")
                            strSystem += "+ ";
                        }

                        //On ajoute la valeur dans le string
                        strSystem += _matriceCoefficiant.GetData()[i, j].ToString();

                        
                    }

                    else
                        //On ajoute la valeur dans le string
                        strSystem += _matriceCoefficiant.GetData()[i, j].ToString();


                    strSystem += $"x{j + 1} "; //On affecte la variable et son indice

                    //Si on est sur le dernier index
                    if (j == _matriceCoefficiant.GetData().GetLength(1) - 1) {

                        strSystem += $"= {_matriceResultat.GetData()[i, 0]}\n";

                    }
                }

            }

            return strSystem;

        }

        

        //retourne une matrice X contenant les valeurs des inconnues en appliquant la règle de Cramer;
        public Matrice TrouverXParCramer() {
            
            
            //La matriceCramer est une copie de la matrice des coefficiants pour ne pas altérer les valeurs.
            Matrice matriceCramer = new((uint)_matriceCoefficiant.GetData().GetLength(0), (uint)_matriceCoefficiant.GetData().GetLength(1), _matriceCoefficiant.GetData().Clone() as double[,]);

            //La matrice qui va contenir les résultats                                            On utilise Clone, car sinon la matrice devient une référence de l'autre matrice.
            Matrice resultat = new((uint)_matriceResultat­.GetData().GetLength(0), 1, _matriceResultat­.GetData().Clone() as double[,]);

            //Théorème du déterminant

            double? det = matriceCramer.Determinant();

            if (det == 0 || det == null)
                return null;


            //Résolution par Cramer

            for (int j = 0; j < matriceCramer.GetData().GetLength(1); j++) {

                //On modifie les éléments de la rangée pour chaque colonne
                for (int i = 0; i < matriceCramer.GetData().GetLength(0); i++) {

                    //On modifie l'élément
                    matriceCramer.SetElement(i, j, _matriceResultat.GetElement(i, 0));
                                      

                }

                Console.WriteLine((double)matriceCramer.Determinant());
                resultat.SetElement(j, 0, (double)matriceCramer.Determinant() / (double)det); //On divise le déterminant de la matrice obtenu par celui de la matrice de base


                //On remet l'élément initiale de la rangée de la colonne
                for (int i = 0; i < matriceCramer.GetData().GetLength(0); i++) {

                    //On remet l'élément initial
                    matriceCramer.SetElement(i, j, _matriceCoefficiant.GetElement(i, j));


                }

            }

            return resultat;

        }

        public Matrice TrouverXParInversionMatricielle() {
            try {
                
                //On ne regarde pas ici si le déterminant est de 0, car la méthode MatriceInverse() le fait.
                return _matriceCoefficiant.MatriceInverse().Multiplication(_matriceResultat);
            }

            catch {
                
                return null; //Car le déterminant est null
            }

        }

        public Matrice TrouverXParJacobi(double epsilon) {

            //retourne une matrice X contenant les valeurs des inconnues en appliquant la méthode itérative de Jacobi;
            // Avant de procéder, on vérifie d’abord que la condition de convergence est respectée (la dominance diagonale stricte); si on ne peut s’assurer de la convergence, alors on retourne « null » après avoir affiché un message explicatif à la console;
            //Le paramètre « epsilon » représente le taux d’écart minimal acceptable entre les valeurs des inconnues de deux itérations successives afin de réussir le test de terminaison, c’est-à-dire de juger la solution comme ayant convergée.

            //Vérification de la convergence
            if (!_JacobiConvergence())
                return null; //Ça ne converge pas.

            //On applique la formule


            double sommation; //Va contenir le résultat de la sommation dans la formule

            double[,] vectorInitial = new double[(uint)_matriceResultat­.GetData().GetLength(0), 1]; //Vecteur initial
            double[,] vectorInitial2 = new double[(uint)_matriceResultat­.GetData().GetLength(0), 1]; //Vecteur initial (Pour être sûre qu'on ne se retrouve pas à référencer le même objet


            Matrice nextIteration = new((uint)_matriceResultat­.GetData().GetLength(0), 1, vectorInitial);
            Matrice currentIteration = new((uint)_matriceResultat­.GetData().GetLength(0), 1, vectorInitial2);

            int howManyUnderEpsilon = 0; //Va conter le nombre de NextIteration - CurrentIteration qui sont inférieur à epsilon 

            do {                             

                for (int i = 0; i < _matriceCoefficiant.GetData().GetLength(0); i++) {              

                    sommation = 0; //On réinitialise la sommation

                    for (int j = 0; j < _matriceCoefficiant.GetData().GetLength(1); j++) {

                        //On passe à la prochaine itération
                        if (j == i)
                            continue;

                        sommation += _matriceCoefficiant.GetElement(i, j) * currentIteration.GetElement(j, 0); //Équivaut à la partie sommation seulement dans la formule
                        nextIteration.SetElement(i, 0, (1 / _matriceCoefficiant.GetElement(i, i)) * (_matriceResultat.GetElement(i, 0) - sommation)); //La formule complète

                    }

                    //On vérifie la condition pour quitter
                    if (nextIteration.GetElement(i, 0) - currentIteration.GetElement(i, 0) < epsilon) {

                        howManyUnderEpsilon++; //Tous les éléments doivent remplir cette condition pour quitter donc on incrémente un compteur chaque fois que la condition est vrai

                    }

                    currentIteration.SetElement(i, 0, nextIteration.GetElement(i, 0)); //On met les valeurs de currentIteration au même valeur que next iteration
                    
                }

                //Tous les éléments ont remplis la condition pour quitter
                if (howManyUnderEpsilon == _matriceCoefficiant.GetData().GetLength(0))
                    return nextIteration;

                else
                    howManyUnderEpsilon = 0; //On réinitialise

                

            } while (true); //On boucle à l'infini. Seul la condition de nextIteration - currentIteration < Epsilon peut nous faire quitter
        }

        //On boucle dans la matrice pour si elle peut converger
        private bool _JacobiConvergence() {

            bool strictDominante = true, irreductibleDominante1 = true, irreductibleDominante2 = true;
            
            double magnitudeNonDiagonale; //Va contenir la magnitude des éléments non-diagonaux

            //On boucle
            for (int i = 0; i < _matriceCoefficiant.GetData().GetLength(0); i++) {

                magnitudeNonDiagonale = 0; //On réinitialise à chaque rangée

                for (int j = 0; j < _matriceCoefficiant.GetData().GetLength(1); j++) {


                    //On vérifie si les lignes peuvent se réduire
                    for (int k = 0; k < _matriceCoefficiant.GetData().GetLength(0); k++) {

                        if (_matriceCoefficiant.GetElement(i, j) % _matriceCoefficiant.GetElement(k, j) == 0)
                             irreductibleDominante2 = false;
                    }

                    //On passe à la prochaine itération
                    if (j == i)
                        continue;

                    //On additionne la valeur absolu des éléments
                    magnitudeNonDiagonale += Math.Abs(_matriceCoefficiant.GetElement(i, j));

                }

                //La règle n'est pas respecter
                if (Math.Abs(_matriceCoefficiant.GetElement(i, i)) <= magnitudeNonDiagonale)
                    strictDominante = false;

                else if (Math.Abs(_matriceCoefficiant.GetElement(i, i)) < magnitudeNonDiagonale)
                    irreductibleDominante1 = false;
            }

            //On vérifie la convergence
            if (strictDominante || (irreductibleDominante1 && irreductibleDominante2))
                return true;

            else 
                return false;
            
        }

        

    

    }
}
