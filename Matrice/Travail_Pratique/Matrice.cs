using System;
using System.Collections.Generic;

namespace Travail_Pratique_Matrice {
    public class Matrice {

        private double[,] _matrice;

        //Constructeur
        public Matrice(uint dimensionX, uint dimensionY, double[,] data) {

            if (dimensionX == 0 || dimensionY == 0)
                throw new IndexOutOfRangeException(); //Une matrice ne peut pas avoir un côté dimensionné de 0.

            //On dimensionne notre matrice
            else {
                _matrice = new double[dimensionX, dimensionY];
            }

            //On vérifie si les données à entrée sont de même dimension que ce que l'on veut ajoutés
            if (_matrice.Length != data.Length)
                throw new RankException();

            else {
                _matrice = data; //On affecte nos données dans la matrice
            }

        }


        //Constructeur privé pour empêcher des erreurs de valeur n'ont initialisé.
        private Matrice(uint dimensionX, uint dimensionY) {

            if (dimensionX == 0 || dimensionY == 0)
                throw new IndexOutOfRangeException(); //Une matrice ne peut pas avoir un côté dimensionné de 0.

            //On dimensionne notre matrice
            else {
                _matrice = new double[dimensionX, dimensionY];
            }

        }


        
        /*************************************************************************************
        Entrée : La matrice avec laquelle additionner la matrice actuelle
        Rôle   : Fonction pour additionner deux matrices
        Sortie : La matrice résultante
        *************************************************************************************/
        public Matrice Additionner(Matrice matrix) {

            if (_matrice.GetLength(0) != matrix.GetData().GetLength(0) || _matrice.GetLength(1) != matrix.GetData().GetLength(1))
                throw new RankException(); //Les tableaux ne sont pas de même dimension!
             
            Matrice resultMatrix = new((uint)_matrice.GetLength(0), (uint)_matrice.GetLength(1)); //on construit la matrice résultante

            //On assigne chaque élément un à un
            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++) {

                    //On additionne les éléments et ils sont stockés dans notre matrice résultante
                    resultMatrix.SetElement(i, j, (this.GetElement(i, j) + matrix.GetElement(i, j)));
                    
                }
                
            }

            return resultMatrix; //On retourne la nouvelle matrice

        }

        
        /*************************************************************************************
        Entrée : L'index de la ligne et de la colonne
        Rôle   : On obtient l'élément à une position précise dans la matrice.
        Sortie : L'élément à l'index voulu
        *************************************************************************************/
        public double GetElement(int ligne, int colonne) {

            return _matrice[ligne, colonne];

        }


        
        /*************************************************************************************
        Entrée : Aucune
        Rôle   : On obtient les données de la matrice sous forme de tableau 2D.
        Sortie : Un tableau 2D représentant les données de la matrice
        *************************************************************************************/
        public double[,] GetData() {

            return _matrice;

        }

        
        /*************************************************************************************
        Entrée : L'index de la rangée, de la colonne et la nouvelle valeur
        Rôle   : On modifie la donnée à une position précise dans la matrice
        Sortie : Aucune
        *************************************************************************************/
        public void SetElement(int indexX, int indexY, double data) {

            _matrice[indexX, indexY] = data;

        }

        
        /*************************************************************************************
        Entrée : Aucune
        Rôle   : On modifie la méthode ToString pour qu'elle retourne les données de la matrice
        Sortie : Une matrice sous forme de String
        *************************************************************************************/
        public override string ToString() {

            string strMatrice = "| ";

            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++) {

                    strMatrice += _matrice[i, j].ToString(); //On ajoute la valeur de la matrice à un String.
                    strMatrice += " | "; //On met un élément séparateur de matrice.

                }

                //Si on n'est pas sur la dernière ligne de la matrice, va afficher le prochain |
                if (i < _matrice.GetLength(0) - 1) 
                    strMatrice += "\n| "; //Élément séparateur qui apparît au début de la prochaine ligne

            }
            
            return strMatrice;
        }

        
        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Fonction Boolean qui va dire si la matrice est carré
        Sortie : Un bool
        *************************************************************************************/
        public bool EstCarree() {

            return _matrice.GetLength(0) == _matrice.GetLength(1);

        }



        /*************************************************************************************
        Entrée : Un objet (idéalement de type matrice)
        Rôle   : On redéfini le Equals de la Matrice (Utile pour les tests unitaires)
        Sortie : Un bool
        *************************************************************************************/
        public override bool Equals(object obj) {

            //Si l'objet est une Matrice
            if(obj is Matrice matrice) {

                if (matrice._matrice.Length != this._matrice.Length)
                    return false;

                //On vérifie chaque élément
                for (int i = 0; i <  matrice._matrice.GetLength(0); i++) {

                    for (int j = 0; j < matrice._matrice.GetLength(1); j++) {

                        //Si un des éléments n'est pas pareil, on retourne faux
                        if (matrice._matrice[i, j] != _matrice[i, j])
                            return false;
                    }


                }
            }

            //l'objet n'est pas une matrice
            else {

                return false;
            }

            return true; //Si on atteint ici, c'est que tout est égale
        }

        //On doit l'override si on override le Equals
        public override int GetHashCode() {
            return HashCode.Combine(_matrice);
        }

        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Faire la matrice transposer
        Sortie : Une matrice
        *************************************************************************************/
        public Matrice Transpose() {

            double[,] newMatData = new double[_matrice.GetLength(1), _matrice.GetLength(0)];

            for (int i =0; i < _matrice.GetLength(1); i++) {
                for (int j = 0; j < _matrice.GetLength(0); j++) {

                    newMatData[i, j] = _matrice[j, i];

                }


            }

            return new Matrice((uint)_matrice.GetLength(1), (uint)_matrice.GetLength(0), newMatData);

        }


        
        /*************************************************************************************
        Entrée : La matrice mère, l'index de la rangée et de la colonne à exclure
        Rôle   : Créer une matrice sans certaine colonne (calcul du déterminant)
        Sortie : Une Matrice
        *************************************************************************************/
        private Matrice _MatriceDeSubstitution(Matrice currentMatrix, int rangee_Exclus, int colonne_Exclus) {

            //On crée une matrice avec 1 dimension en X et en Y en moins. (Si au départ 4X4, celle-ci sera 3X3)
            Matrice matriceSubstitution = new((uint)currentMatrix._matrice.GetLength(0) - 1, (uint)currentMatrix._matrice.GetLength(1) - 1);

            int rangerIndex = -1; //Représente l'index de la rangée
            for (int i = 0; i < currentMatrix._matrice.GetLength(0); i++) {
                
                //Si on est dans l'index de la rangée à exclure, on passe à la prochaine intération
                if (i == rangee_Exclus)
                    continue;
                rangerIndex++; //On incrémente l'index de la rangée. (Ne s'incrémentera pas si on est dans la rangée Exclus)


                int colonneIndex = -1; //Index des colonnes de la matrice de substitution

                for (int j = 0; j < currentMatrix._matrice.GetLength(1); j++) {
                    //On passe à la prochaine itération si on est dans la colonne à exclure
                    if (j == colonne_Exclus)
                        continue;
                    matriceSubstitution._matrice[rangerIndex, ++colonneIndex] = currentMatrix._matrice[i, j];
                }
            }
            return matriceSubstitution;
        }

        
        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Calcul du déterminant
        Sortie : Un double nullable
        *************************************************************************************/
        public double? Determinant() {

            //Si la matrice n'est pas carré
            if (!EstCarree())
                return null; //Aucun déterminant possible

            //Si la matrice est 1X1
            else if (_matrice.GetLength(0) == 1)
                return _matrice[0, 0];

            //Si on a une matrice 2X2
            else if (_matrice.GetLength(0) == 2)
                //On retourne la valeure du calcul du déterminant.
                return ((_matrice[0, 0] * _matrice[1, 1]) - (_matrice[0, 1] * _matrice[1, 0])); //Formule mathématique du calcul du déterminant

            else {
                double somme = 0; //Va contenir la somme de tous les déterminants des matrices de Substitutions

                //On boucle pour chaque colonne de la matrice
                for (int i = 0; i < _matrice.GetLength(1); i++) {

                    //On additionne la somme de chaque sous matrice en appelant la fonction Determinant() sur une matrice de Substitution
                    somme += _ChangerSigne(i) * _matrice[0, i] * _Determinant(_MatriceDeSubstitution(this, 0, i));
                }

                return somme;
            }
        }

        
        /*************************************************************************************
        Entrée : Une matrice de substitution
        Rôle   : Calcul du déterminant
        Sortie : Un double
        *************************************************************************************/
        private double _Determinant(Matrice tempMatrix) {

            //Presque la même formule que le Determinant public sauf qu'on prend une matrice de Substitution en paramètre
            
            if (tempMatrix._matrice.GetLength(0) == 2) {
                //On retourne la valeure du calcul du déterminant.
                return (tempMatrix._matrice[0, 0] * tempMatrix._matrice[1, 1]) - (tempMatrix._matrice[0, 1] * tempMatrix._matrice[1, 0]);
            }

            else {
                double somme = 0;

                for (int i = 0; i < tempMatrix._matrice.GetLength(1); i++) {

                    somme += _ChangerSigne(i) * tempMatrix._matrice[0,i] * _Determinant(_MatriceDeSubstitution(tempMatrix, 0, i));
                }

                return somme;
            }

        }

        
        /*************************************************************************************
        Entrée : Index de la colonne
        Rôle   : Fonction qui va permettre lors de la multiplication dans le calcul du déterminant de changer le signe des valeurs.
        Sortie : Un int (1 ou -1)
        *************************************************************************************/
        private int _ChangerSigne(int indexColonne) {

            //Représente l'index de la colonne de la matrice
            //L'index est pair
            if (indexColonne % 2 == 0) {
                return 1;
            }
            //L'index est impair 
            else {
                return -1;
            }
        }




        
        /*************************************************************************************
        Entrée : La matrice avec laquelle multiplié 
        Rôle   : Multiplication de Matrices (NON-COMMUTATIF) (on fait this multiplier par matriceB)
        Sortie : La matrice résultante de l'opération
        *************************************************************************************/
        public Matrice Multiplication(Matrice matriceB) {

            //Multiplication de matrice (M x N * N x P = M x P)
            if (this._matrice.GetLength(1) != matriceB._matrice.GetLength(0))
                throw new Exception(); //Incompatibilité des matrices

            Matrice matResultante = new((uint) this._matrice.GetLength(0), (uint) matriceB._matrice.GetLength(1)); //Multiplication de matrice (M x N * N x P = M x P)

            //On boucle sur chaque rangée de la première matrice
            for (int i = 0; i < this._matrice.GetLength(0); i++) {

                //On boucle sur chaque colonne de la deuxième matrice
                for (int j = 0; j < matriceB._matrice.GetLength(1); j++) {

                    matResultante._matrice[i, j] = 0; //La matrice doit être initialisé pour faire +=

                    //on boucle sur chaque colonne de la première matrice
                    for (int r = 0; r < _matrice.GetLength(1); r++) {
                        matResultante._matrice[i, j] += _matrice[i, r] * matriceB._matrice[r, j]; //On effectue la multiplication matricielle sur chaque terme
                    }
                }
            }

            return matResultante;
        }

        /*************************************************************************************
        Entrée : La matrice avec laquelle multiplié 
        Rôle   : Multiplication de plusieurs Matrices (NON-COMMUTATIF) 
        Sortie : La matrice résultante de l'opération
        *************************************************************************************/
        public Matrice Multiplication(List<Matrice> matrice) {

            //On copie les valeurs de this dans matriz (Fun Fact : Matriz signifie matrice en espagnol)
            Matrice matriz = new((uint)_matrice.GetLength(0), (uint)_matrice.GetLength(1), _matrice.Clone() as double [,]);

            //On boucle sur chaque valeur dans la list
            foreach (var matrix in matrice) {

                matriz = matriz.Multiplication(matrix); //On fait la multiplication et on sauvegarde le résultat dans matriz

            }

            return matriz; //On retourne la matrice résultante 

        }


        /*************************************************************************************
        Entrée : La valeur
        Rôle   : Multiplier la matrice par un scalaire
        Sortie : la matrice résultante
        *************************************************************************************/
        public Matrice ProduitScalaire(double value) {

            Matrice resultMatrix = new((uint)_matrice.GetLength(0), (uint)_matrice.GetLength(1)); //on construit la matrice résultante

            //On assigne chaque élément un à un
            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++) {

                    //On additionne les éléments et ils sont stockés dans notre matrice résultante
                    resultMatrix.SetElement(i, j, (this.GetElement(i, j) * value)); //On multiplie l'élément par le scalaire

                }

            }

            return resultMatrix;
        }

        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Vérifier si la matrice est régulière
        Sortie : Un boolean
        *************************************************************************************/
        public bool EstReguliere() {

            var det = Determinant(); //On calcul le déterminant

            //Si le déterminant n'est pas 0 ou null, la matrice est régulière
            if (det != 0 && det != null)
                return true;

            else
                return false;
        }

        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Vérifier si la matrice est régulière tout en retournant le déterminant si elle l'est
        Sortie : un tuple (boolean et double)
        *************************************************************************************/
        private (bool, double) _EstReguliere() {
            var det = Determinant(); //On calcul le déterminant

            //Matricde régulière. On retourne le déterminant
            if (det != 0 && det != null)
                return (true, (double)det);

            //Non régulière
            else
                return (false, 0); //On retourne 0, car il est considérer comme nul dans la régularité d'une matrice. Et aussi, car on retourne un double non nullable
        }

        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Calculé la trace de la matrice
        Sortie : Un double Nullable
        *************************************************************************************/
        public double? Trace() {

            double trace = 0; // On initialise pour le +=

            //Si la matrice n'est pas carré, on retourne null
            if (!EstCarree())
                return null;

            for (int i = 0; i < _matrice.GetLength(0); i++) {

                trace += _matrice[i, i]; //On additionne les éléments qui forment la diagonale de la matrice
                
            }
            
            return trace;
            
        }

        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Calculer la CoMatrice
        Sortie : la CoMatrice
        *************************************************************************************/
        public Matrice CoMatrice() {

            //Si la matrice n'est pas carr, la CoMatrice n'existe pas
            if (!EstCarree())
                throw new Exception();

            //Si c'est une matrice 1X1
            else if (this._matrice.GetLength(0) == 1) {

                Matrice matriceIdentity1X1 = new(1, 1); //CoMatrice
                matriceIdentity1X1.SetElement(0, 0, 1); //On donne la valeur 1 à l'élément 

                //La comatrice de toute matrice de taille (1,1) est la matrice identité I1 = (1). (Source : https://fr.wikipedia.org/wiki/Comatrice)

                return matriceIdentity1X1;
            }

            Matrice matriceB = new((uint)_matrice.GetLength(0), (uint)_matrice.GetLength(1)); //CoMatrice
            Matrice matriceX; //MatriceX est une matrice de Substitution

            for (int i = 0; i < matriceB._matrice.GetLength(0); i++) {
                for (int j = 0; j < matriceB._matrice.GetLength(1); j++) {

                    matriceX = _MatriceDeSubstitution(this, i, j); //On créer une matrice de Substitution

                    matriceB._matrice[i, j] = _ChangerSigne(i + j) * (double)matriceX.Determinant(); 

                    
                }
            }

            return matriceB;
        }


        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Calculer la matrice inverse
        Sortie : la Matrice inverse
        *************************************************************************************/
        public Matrice MatriceInverse() {
                        
            double determ;
            bool isRegular;

            (isRegular, determ) = _EstReguliere(); //On calcul le déterminant et on regarde si elle est régulière par le fait même

            //On empêche une division par 0 (Matrice irrégulière)
            if (!isRegular)
                throw new Exception();

            //Si matrice 1X1
            else if (this._matrice.GetLength(0) == 1) {

                Matrice matriceInv1X1 = new(1, 1, _matrice);
                _matrice[0, 0] = 1 / matriceInv1X1._matrice[0, 0]; //La matrice A•A^-1 = I soit 1. Donc x • 1/x = 1.

            }
            
            Matrice Inverse = new((uint)_matrice.GetLength(0), (uint)_matrice.GetLength(1)); ;
            //Console.WriteLine(determ);

            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++)
                    //On effectue le calcul pour trouver la matrice inverse
                    Inverse._matrice[i, j] = (this.CoMatrice().Transpose()._matrice[i, j]) / determ;
            }

            return Inverse;
        
        }

        /*************************************************************************************
        Entrée : Un boolean nullable et un boolean non nullable
        Rôle   : Vérifier si la matrice est triangulaire
        Sortie : Un boolean
        *************************************************************************************/
        public bool EstTriangulaire(bool? typeDeTriangularité, bool strict) {

            //Si elle n'est pas carrée, elle n'est pas triangulaire
            if (!EstCarree()) {
                return false;
            }

            //Note : une matrice 1X1 est triangulaire stricte si son seul élément vaut 0

            //On vérifie triangulaire strict
            if (strict) {

                //Triangulaire suppérieur
                if (typeDeTriangularité == true)
                    return _TriangulaireSupperieur(true);

                //triangulaire inférieur
                else if (typeDeTriangularité == false)
                    return _TriangulaireInferieur(true);

                //Peut importe
                else {

                    //Si elle n'est pas triangulaire suppérieur, on test inférieur
                    if (!_TriangulaireSupperieur(true)) {
                        return _TriangulaireInferieur(true);
                    }

                    //Elle est triangulaire suppérieur
                    else
                        return true;

                }
            }

            //Note : une matrice 1X1 est toujours triangulaire
            //Même qu'en haut, sans vérifier si strict
            else {
                if (typeDeTriangularité == true)
                    return _TriangulaireSupperieur(false);

                else if (typeDeTriangularité == false)
                    return _TriangulaireInferieur(false);

                else {

                    if (!_TriangulaireSupperieur(false)) {
                        return _TriangulaireInferieur(false);
                    }
                    else
                        return true;

                }
            }
        }


        /*************************************************************************************
        Entrée : Un bool
        Rôle   : Vérifier si la matrice est triangulaire Suppérieur
        Sortie : Un boolean
        *************************************************************************************/
        private bool _TriangulaireSupperieur(bool strict) {
            
            //On boucle sur chaque élément
            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++) {

                    //Si on veut vérifier si Strict. 
                    if (strict && (j <= i)) {

                        //L'élément n'est pas 0 donc elle n'est pas triangulaire
                        if (_matrice[i, j] != 0) {
                            return false;
                        }
                    }

                    //Si on ne vérifie pas si strict
                    else if (!strict && (j < i)) {

                        //L'élément n'est pas 0 donc elle n'est pas triangulaire
                        if (_matrice[i, j] != 0) {
                            return false;
                        }
                    }
                }
                
            }

            //Si on atteint ce return, c'est qu'elle est triangulaire
            return true;

        }

        /*************************************************************************************
        Entrée : Un bool
        Rôle   : Vérifier si la matrice est triangulaire Inférieur
        Sortie : Un boolean
        *************************************************************************************/
        private bool _TriangulaireInferieur(bool strict) {

            for (int i = 0; i < _matrice.GetLength(0); i++) {
                for (int j = 0; j < _matrice.GetLength(1); j++) {

                    //Si on vérifie si strict
                    if (strict && (j >= i)) {

                        //L'élément n'est pas 0 donc elle n'est pas triangulaire
                        if (_matrice[i, j] != 0) {
                            return false;
                        }
                    }

                    //Si on ne vérifie pas si strict
                    else if (!strict && (j > i)) {

                        //L'élément n'est pas 0 donc elle n'est pas triangulaire
                        if (_matrice[i, j] != 0) {
                            return false;
                        }
                    }
                }

            }

            //Si on atteint ici, c'est qu'elle est triangulaire
            return true;

        }

    }
}
