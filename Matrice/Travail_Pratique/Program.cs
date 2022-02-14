using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Travail_Pratique_Matrice {
    class Program {


        /*********************************************************
         Entrée : La matrice à afficher
         Rôle   : Afficher le résultat de la matrice sous forme x1 = chiffre, x2 = autreChiffre...
         Sortie : Aucune
         *********************************************************/
        private static void _AfficherSELResult(Matrice result) {

            for (int i = 0; i < result.GetData().GetLength(0); i++) {

                Console.WriteLine($"x{i + 1} = {result.GetElement(i, 0)}"); //0 pour les colonnes, car c'est un vecteur d'une certaine façon. On s'est assuré dans le constructeur que le nombre de colonne soit de 1.

            }

        }

        /*********************************************************
         Entrée : La fonction à exécuter
         Rôle   : Chronométrer le temps d'exécution d'une fonction
         Sortie : L'intervalle de temps mesuré
         *********************************************************/
        private static TimeSpan _TempsDeCalcul(Func<Matrice> function, string msg) {

            Matrice? result; //Référence la sortie de la fonction

            Stopwatch chrono = new();

            //On commence le chronomètre
            chrono.Start();

            
            result = function(); //On exécute la fonction

            //On arrête le chornomètre
            chrono.Stop();

            //On affiche un message pour dire qu'on ne peut pas résoudre le SEL
            if (result == null) {

                Console.WriteLine(msg);

            }

            //On affiche également le résultat de la fonction
            else
                _AfficherSELResult(result);

            //On retourne l'intervalle de temps chronométré
            return chrono.Elapsed;


        }

        /*********************************************************
         Entrée : La fonction à exécuter
         Rôle   : Chronométrer le temps d'exécution d'une fonction
         Sortie : L'intervalle de temps mesuré
         *********************************************************/
        private static TimeSpan _TempsDeCalcul(Func<double, Matrice> function, double epsilon, string msg) {

            Matrice? result; //Référence la sortie de la fonction

            Stopwatch chrono = new();

            //On commence le chronomètre
            chrono.Start();

            
            result = function(epsilon); //On exécute la fonction

            //On arrête le chornomètre
            chrono.Stop();

            //On affiche un message pour dire qu'on ne peut pas résoudre le SEL
            if (result == null) {

                Console.WriteLine(msg);

            }

            //On affiche également le résultat de la fonction
            else
                _AfficherSELResult(result);

            //On retourne l'intervalle de temps chronométré
            return chrono.Elapsed;


        }

        /*********************************************************
        Entrée : Une référence du string que l'on veut incrémenter
        Rôle   : Incrémenter un string
        Sortie :  Le string incrémenter
        *********************************************************/
        private static string _NextString(ref string str) {
            //La référence du String permet de modifier également le String, ce qui est utile pour notre programme.


            char strLastChar = str[^1]; //On obtient l'élément à la dernière position du string

            //Les éléments possibles sont compris entre A et Z.         
            if (strLastChar != 'Z') {

                strLastChar++; //On incrémente la valeur du Char

                str = str.Replace(str[^1], strLastChar);

            }

            //Pour simuler ('Z'++ = 'A')
            else {

                //Pour simuler('Z'++ = 'A')
                strLastChar = 'A';
                str += strLastChar.ToString();

                //Les résultats ressembleront à ZA, ZZA, ZZZA...

            }
            


            return str;
        }


        /***************************************************************************
        Entrée : Un message qui sera affiché dans la console
        Rôle   : Faire un input dans la console et valider que c'est un Unsigned Int
        Sortie : Un Unsigned Int correspondant à l'entrée de l'utilisateur
        ***************************************************************************/
        private static uint _ReadUInt(string message) {

            uint strConvertiEnUInteger; //Un Uint qui va contenir l'entrée de L'utilisateur


            Console.Write(message);

            //On boucle tant que ce n'est pas un UInt positif
            while (!uint.TryParse(Console.ReadLine(), out strConvertiEnUInteger) || strConvertiEnUInteger <= 0) {

                Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                Console.WriteLine("\nVous n'avez pas entré un nombre plus grand que 0"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                Console.Write(message);  //On affiche un message dans la console pour demander d'entrer un entier positif.
            }

            return strConvertiEnUInteger;
        }

        /*********************************************************************
        Entrée : Un message qui sera affiché dans la console
        Rôle   : Faire un input dans la console et valider que c'est un double
        Sortie : Un double
        *********************************************************************/
        private static double _ReadDouble(string message) {
           
            double strConvertiEnDouble;


            Console.Write(message);

            //On boucle tant que l'utilisateur n'entre pas un double
            while (!double.TryParse(Console.ReadLine(), out strConvertiEnDouble)) {

                Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                Console.WriteLine("\nVous n'avez pas entré un nombre"); //On affiche un message expliquant qu'il n'a pas entrer nombre

                Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                Console.Write(message);  //On affiche un message dans la console pour demander d'entrer un entier positif.
            }

            Console.WriteLine(""); //Simule un \n
            return strConvertiEnDouble;
        }


        /******************************************************************
        Entrée : Aucune
        Rôle   : Construire un objet de type Matrice dans la classe Program
        Sortie : Une Matrice
        ******************************************************************/
        private static Matrice _CreerMatrice() {
            
            uint nbRow, nbCol; //Variable correspondant au nombre de ranger et de colonne d'une matrice

            
            //On initialise les variables
            nbRow = _ReadUInt("Veuillez entrer le nombre de rangée : ");
            nbCol = _ReadUInt("Veuillez entrer le nombre de colonne : ");
                

            double[,] data = new double[nbRow, nbCol]; //On crée un tableau 2D avec les dimensions désiré de la matrice

            //On boucle pour afficher les positions précises de la matrices
            for (int i = 0; i < nbRow; i++) {
                for (int j = 0; j < nbCol; j++) {

                    //On demande à l'utilisateur d'entrée les valeurs désiré pour la matrice. On les affectes dans le Tableau 2D
                    data[i, j] = _ReadDouble($"Veuillez entrer la valeur à la position [{i + 1}, {j + 1}] de la matrice : ");

                }
           
            }
            
            //On construit la matrice à l'aide du constructeur public. On lui fournit le nombre de rangée, le nombre de Colonne et les données de la matrice
            Matrice matrice = new Matrice(nbRow, nbCol, data);

            return matrice;
        }


        /******************************************************************
       Entrée : Aucune
       Rôle   : Construire 2 Matrices pour le SEL
       Sortie : Un tuple de 2 Matrice
       ******************************************************************/
        private static (Matrice, Matrice) _CreerMatriceSEL() {
            uint nbRow; //Variable correspondant au nombre de ranger et de colonne d'une matrice


            //On initialise les variables
            nbRow = _ReadUInt("Veuillez entrer le nombre de rangée/colonne (matrice carrée) : ");
            


            double[,] data = new double[nbRow, nbRow]; //On crée un tableau 2D avec les dimensions désiré de la matrice

            Console.WriteLine("Matrice des coefficiants :");
            //On boucle pour afficher les positions précises de la matrices
            for (int i = 0; i < nbRow; i++) {
                for (int j = 0; j < nbRow; j++) {

                    //On demande à l'utilisateur d'entrée les valeurs désiré pour la matrice. On les affectes dans le Tableau 2D
                    data[i, j] = _ReadDouble($"Veuillez entrer la valeur à la position [{i + 1}, {j + 1}] de la matrice : ");

                }

            }

            Console.WriteLine("Matrice des résultats :");
            double[,] dataResult = new double[nbRow, 1]; //On crée un tableau 2D avec les dimensions désiré de la matrice
            
            for(int i = 0; i < nbRow; i++) {

                dataResult[i, 0] = _ReadDouble($"Veuillez entrer la valeur à la position [{i + 1}, {1}] de la matrice : ");

            }

            //On construit la matrice à l'aide du constructeur public. On lui fournit le nombre de rangée, le nombre de Colonne et les données de la matrice
            Matrice matriceCoe = new Matrice(nbRow, nbRow, data);
            Matrice matriceResult = new Matrice(nbRow, 1, dataResult
                );

            return (matriceCoe, matriceResult);
        }



        /*****************************************************************************************************************************************
        Entrée : Un String passé par réference
        Rôle   : Faire un Input dans la console où on affecte la valeur à une variable passé par référence et on retourne également cette variable
        Sortie : Un string
        *****************************************************************************************************************************************/
        private static string _Read2(out string str) {

            str = Console.ReadLine();

            return str;
        }

        /************************************************************************
        Entrée : Une ListDictionary et la couleur de l'affichage dans la console
        Rôle   : Choisir la matrice sur laquelle on veut effectuer nos opérations
        Sortie : La clef du Dictionnaire (string)
        ************************************************************************/
        private static string _ChoisirMatrice(ListDictionary matrixList, ConsoleColor color) {

            //On efface la console, on change la couleur et on affiche un message
            Console.Clear();
            Console.ForegroundColor = color;
            Console.WriteLine("Voici la liste des matrice : ");

            //On Affiche toute les clefs disponnible (nom des différentes Matrice)
            foreach (var key in matrixList.Keys) {

                Console.WriteLine(key);

            }

            string strKey;

            //On demande à l'utilisateur de choisir la matrice sur laquelle il veut effectuer l'opération
            Console.Write("Veuillez choisir la matrice sur laquelle vous voulez effectuer l'opération : ");
            while (!matrixList.Contains(_Read2(out strKey).ToUpper())) {

                Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                Console.WriteLine("\nVous n'avez pas entré un nom de matrice valide"); //On affiche un message expliquant qu'il n'a pas entrer un nom existant.

                Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                Console.ForegroundColor = color;
            }

            //On retourne la clef et on s'assure qu'elle est en majuscule
            Console.Clear(); //On efface la console
            return strKey.ToUpper();

        }


        /*************************************************************************************
        Entrée : Une référence sur une ListDictionary et une référence sur un String
        Rôle   : Afficher les différentes opérations matricielle et permettre de les effectuer
        Sortie : Aucune
        *************************************************************************************/
        private static void _MenuMatrice(ref ListDictionary matrixContainer, ref string currentMatrixName) {

            int choix; //Déclaration des variables
            bool quitter = false;
            Matrice tempMatrice; //On ne l'initialise pas, car elle va servir de référence sur les différentes Matrice qui agieront sur le programme

            
            //Est créé seulement lorsqu'on entre dans le menu matrice pour la première fois
            if (matrixContainer.Count == 0)

                //Matrice initiale
                matrixContainer.Add(_NextString(ref currentMatrixName), _CreerMatrice());


            //Va boucler pour permettre un retour au menu
            do {

                //Affichage du memu
                Console.WriteLine("Veuillez choisir un choix parmi les suivants :");
                Console.WriteLine("1.Ajouter une matrice.\n");
                Console.WriteLine("2.Additionner.\n");
                Console.WriteLine("3.Produit Scalaire\n");
                Console.WriteLine("4.Multiplication\n");
                Console.WriteLine("5.Triangularité de la matrice\n");
                Console.WriteLine("6.Trace\n");
                Console.WriteLine("7.Déterminant\n");
                Console.WriteLine("8.Transposer la matrice\n");
                Console.WriteLine("9.Obtenir la CoMatrice\n");
                Console.WriteLine("10.Obtenir la matrice inverse\n");
                Console.WriteLine("11.Vérification si c'est une matrice carrée\n");
                Console.WriteLine("12.Vérification si la matrice est régulière\n");
                Console.WriteLine("13.Modifier une matrice existante\n");
                Console.WriteLine("14.Visualiser une matrice\n");
                Console.WriteLine("15.Quitter");


                //On boucle tant que l'utilisateur n'entre pas un entier.
                while (!int.TryParse(Console.ReadLine(), out choix)) {

                    Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                    Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                    Console.WriteLine("\nVous n'avez pas entré un nombre entier!\n\n"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                    Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                    Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
                }

                //On choisi L'option que l'utilisateur à choisit
                switch (choix) {

                    //Ajouter une matrice
                    case 1:

                        Console.Clear();
                        tempMatrice = _CreerMatrice(); //On crée une nouvelle Matrice
                        matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);
                        Console.Clear();
                        Console.WriteLine($"La matrice est : \n{tempMatrice}"); //On affiche la matrice qu'on vient d'ajouter


                        break;

                    //Addition
                    case 2:

                        //On demande à l'utilisateur de choisir ça matrice. On réféencie cette matrice dans tempMatrice, car matrixContainer ne peut utiliser d'opération de type Matrice
                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Green)];

                        //On additionne et on affecte la matrice de Retour à tempMatrice     On change la couleur de la console pour montrer à l'utilisateur qu'il doit choisir une 2e matrice et qu'il n'y a pas eu de "Bug"
                        tempMatrice = tempMatrice.Additionner((Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.DarkYellow)]);
                        matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice); //On ajoute la matrice résultante à l'opération dans la ListDictionary

                        //On remet la couleur de la console par défaut
                        Console.ResetColor();
                        Console.WriteLine($"La matrice résultante est : \n{tempMatrice}"); //On affiche la matrice résultant l'opération


                        break;


                    //Produit Scalaire
                    case 3:


                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Magenta)];

                        tempMatrice = tempMatrice.ProduitScalaire(_ReadDouble("Veuillez entrer le nombre par lequel vous voulez multiplier la matrice : "));
                        matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);

                        Console.ResetColor();
                        Console.WriteLine($"La matrice résultante est : \n{tempMatrice}");



                        break;


                    //Multiplication Matricielle
                    case 4:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Cyan)];

                        

                        choix = (int) _ReadUInt("Avec combien de matrice voulez-vous multiplier la matrice principale : ");

                        if (choix == 1) {


                            tempMatrice = tempMatrice.Multiplication((Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.DarkMagenta)]);
                            
                            Console.ResetColor();

                        }

                        //Multiplication avec plusieurs matrices
                        else {

                            //Va contenir toute les matrices avec lesquelles ont veut multiplier la première matrice
                            List<Matrice> multipleMatriz = new();

                            //On ajoute le nombre de matrice que l'utilisateur veut multiplier
                            for (int i = 0; i < choix; i++) {

                                //On alterne sur les couleurs pour montrer à l'utilisateur qu'il n'y a pas d'erreur
                                if (i % 2 == 0)
                                    multipleMatriz.Add((Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Blue)]);

                                else
                                    multipleMatriz.Add((Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.DarkGreen)]);

                            }
                            Console.ResetColor();

                            tempMatrice = tempMatrice.Multiplication(multipleMatriz);
                            Console.WriteLine($"La multiplication c'est effectué en {multipleMatriz.Count} opérations. CQFD\n"); //multipleMatriz.Count est la grandeur de la list donc le nombre d'opération effectué.
                        }

                        matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);

                        Console.WriteLine($"La matrice résultante est : \n{tempMatrice}");
                                                                                               
                        choix = 4; //On remet choix à sa valeur avant l'entré dans ce case. Permet de passer dans le if qui dit Press Any Key To Continue
                        

                        break;

                    //Vérifier si c'est une matrice triangulaire
                    case 5:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Green)];
                        Console.ResetColor();

                        bool? type;  //True : Suppérieur, False : Inférieur, Null : Peut importe
                        bool strict; //True : On vérifie Triangulaire Strict
                        bool triangle; //True : Est Triangulaire, False : n'est pas triangulaire
                        string userAnswer; //Entrée de l'utilisateur


                        //On boucle tant que l'utilisateur n'entre pas une entrée valide
                        do {
                            Console.WriteLine("Pour vérifier la triangularité suppérieur (Entrez : S)\nPour la triangularité inférieur (Entrez : I)\nPour n'importe quelle des deux méthodes (Entrez : ?)");

                            userAnswer = Console.ReadLine();

                            //Pour pouvoir vérifier Triangularité suppérieur
                            if (userAnswer.ToUpper().Equals("S")) {
                                type = true;
                                break;
                            }

                            //Pour vérifier trinagularité inférieur
                            else if (userAnswer.ToUpper().Equals("I")) {
                                type = false;
                                break;
                            }

                            //Pour vérifier n'importe quelle triangularité
                            else if (userAnswer.Equals("?")) {
                                type = null;
                                break;
                            }

                            //Message d'erreur, entrée invalide
                            else {

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("{0} EST UNE ENTRÉE INVALIDE!!!", userAnswer);
                                Console.ResetColor();

                            }


                        } while (true);

                        Console.Clear(); //On réinitialise la console


                        //On boucle tant que l'entré est invalide
                        do {
                            Console.WriteLine("Voulez-vous vérifier si la matrice est triangulaire stricte? \nPour oui (Entrez : Y)\nPour non (Entrez : N)");

                            userAnswer = Console.ReadLine();


                            //On veut Triangulaire Strict
                            if (userAnswer.ToUpper().Equals("Y")) {
                                strict = true;
                                break;
                            }

                            //On ne désire pas vérifier si c'est strict
                            else if (userAnswer.ToUpper().Equals("N")) {
                                strict = false;
                                break;
                            }

                            //Message d'erreur
                            else {

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("{0} EST UNE ENTRÉE INVALIDE!!!", userAnswer);
                                Console.ResetColor();

                            }


                        } while (true);

                        Console.Clear(); //On réinitialise la console

                        triangle = tempMatrice.EstTriangulaire(type, strict); //On vérifie en fonction de ce que l'utilisateur à demander


                        //Triangulaire Suppérieur
                        if (type == true) {

                            //Triangulaire Suppérieur Strict
                            if (strict) {

                                //Si Triangulaire Strict
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire suppérieur stricte.");

                                //N'est pas triangulaire Strict
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire suppérieur stricte.");
                            }

                            //Triangulaire Suppérieur (sans vérification si Strict)
                            else {

                                //Est triangulaire Suppérieur
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire suppérieur.");


                                //N'est pas triangulaire Suppérieur
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire suppérieur.");
                            }
                                

                        }

                        //Triangulaire Inférieur
                        else if (type == false) {

                            //Triangulaire Inférieur Strict
                            if (strict) {

                                //La matrice est triangulaire inférieur stricte
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire inférieur stricte.");

                                //La matrice N'est PAS triangulaire inférieur stricte
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire inférieur stricte.");
                            }

                            //Triangulaire inférieur sans Vérification si strict
                            else {

                                //Est Triangulaire Inférieur
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire inférieur.");

                                //N'est pas triangulaire inférieur; 
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire inférieur.");
                            }
                        }

                        //Peut importe le type de triangularité
                        else {

                            //Si Strict
                            if (strict) {

                                //La matrice est triangulaire Strict
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire stricte.");

                                //La matrice n'est pas triangulaire Strict
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire stricte.");
                            }

                            //On ne vérifie pas si strict
                            else {

                                //La matrice est triangulaire
                                if (triangle)
                                    Console.WriteLine("La matrice est triangulaire.");

                                //La matrice n'est pas triangulaire
                                else
                                    Console.WriteLine("La matrice n'est pas triangulaire.");
                            }
                        }


                        break;

                    //Pour obtenir la trace de la matrice
                    case 6:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Yellow)];
                        Console.ResetColor();

                        double? trace = tempMatrice.Trace(); //Si la trace retourne null, la matrice n'est pas carré

                        //La matrice n'est pas carré
                        if (trace == null)
                            Console.WriteLine("La matrice n'a pas de trace. Ce n'est pas une matrice carré.");

                        else
                            Console.WriteLine("La trace de la matrice est : {0}", trace);


                        break;
                    
                    //Calcul du déterminant
                    case 7:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Green)];
                        Console.ResetColor();

                        double? determinant = tempMatrice.Determinant(); //Si null, matrice non carré

                        //Matrice non carrée
                        if (determinant == null)
                            Console.WriteLine("La matrice n'a pas de déterminant. Ce n'est pas une matrice carré.");

                        else
                            Console.WriteLine("Le déterminant de la matrice est : {0}", determinant);

                      
                        break;
                    

                    //Obtenir la matrice transposer
                    case 8:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.DarkGreen)];
                        Console.ResetColor();

                        tempMatrice = tempMatrice.Transpose();

                        matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);

                        Console.WriteLine("La matrice transposé est : \n{0}", tempMatrice);

                        break;
                    

                    //Obtenir la CoMatrice
                    case 9:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Magenta)];
                        Console.ResetColor();

                        //Si la matrice n'est pas carré, ça va mettre une erreur
                        try {

                            tempMatrice = tempMatrice.CoMatrice();

                            matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);

                            Console.WriteLine("La comatrice est : \n{0}", tempMatrice);

                        }

                        //On attrape l'erreur tel un joueur de Baseball professionnel et on affiche un message à l'utilisateur lui expliquant la raison pour laquelle on ne peut pas effectuer son opération
                        catch {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il n'existe pas de coMatrice pour une matrice non carrée!");
                            Console.ResetColor();
                        }

                        break;
                    

                    //Calcul de la matrice inverse
                    case 10:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Magenta)];
                        Console.ResetColor();

                        //Si la matrice n'est pas régulière, on lance un erreur
                        try {

                            tempMatrice = tempMatrice.MatriceInverse();

                            matrixContainer.Add(_NextString(ref currentMatrixName), tempMatrice);

                            Console.WriteLine("La matrice inverse est : \n{0}", tempMatrice);

                        }

                        //On attrape l'erreur tel le joueur de BasketBall StephCurry. La matrice n'est pas régulière
                        catch {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il n'existe pas de matrice inverse pour une matrice irrégulière!");
                            Console.ResetColor();
                        }


                        break;
                    

                    //On vérifie si la matrice est carré
                    case 11:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Blue)];
                        Console.ResetColor();

                        if (tempMatrice.EstCarree())

                            Console.WriteLine("La matrice est carrée!");

                        else
                            Console.WriteLine("La matrice n'est pas carrée!");

                        break;
                    

                    //On vérifie si la matrice est régulière
                    case 12:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.DarkBlue)];
                        Console.ResetColor();

                        if (tempMatrice.EstReguliere())

                            Console.WriteLine("La matrice est régulière!");

                        else
                            Console.WriteLine("La matrice n'est pas régulière!");


                        break;
                    

                    //Modifier la valeur d'une matrice
                    case 13:

                        tempMatrice = (Matrice)matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Blue)];
                        Console.ResetColor();

                        try {

                            //On modifie l'élément à la position demandé par l'utilisateur. (-1, car en programmation on part de [0, 0], mais en mathématique, les matrices partent de [1, 1]
                            tempMatrice.SetElement((int)_ReadUInt("Veuillez entrer l'index de la rangée de la matrice : ") - 1,
                                (int)_ReadUInt("Veuillez entrer l'index de la rangée de la colonne : ") - 1,
                                _ReadDouble("Veuillez entrer la nouvelle valeur : "));
                        }

                        //Si l'utilisateur tente de modifier la matrice à un emplacement inexistant (Ex. La position [5, 5] d'une matrice 2X2)
                        catch {

                            Console.WriteLine("Il est impossible de changer la valeur à une position inexistante.");

                        }

                        break;

                    //Pour visualiser une matrice
                    case 14:


                        Console.WriteLine(matrixContainer[_ChoisirMatrice(matrixContainer, ConsoleColor.Gray)].ToString());
                        

                        break;

                    //Si on veut quitter le menu des opérations Matricielle.
                    case 15:
                        quitter = true;
                        break;

                    //Arrive en cas de valeur invalide
                    default:

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine("Valeur invalide...");
                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut

                        break;
                }

                //On s'assure que le choix est valide 
                if (0 < choix && choix < 15) {
                    Console.WriteLine("\n\nPress any key to continue...\n");
                    Console.ReadKey(); //Pour mettre la console sur "pause"
                    Console.Clear();
                }
            } while (!quitter);

            Console.Clear(); //On efface la console
        }


        /*************************************************************************************
        Entrée : Aucune
        Rôle   : Afficher/Effectuer les opérations de résolutions de SEL
        Sortie : Aucune
        *************************************************************************************/
        private static void _MenuSEL() {

            int choix = 0; //Déclaration des variables
            bool quitter = false;

            Systeme sel;
            Matrice? answer;


            sel = new(_CreerMatriceSEL());

            Console.Clear();

           

            //Va boucler pour permettre un retour au menu
            do {

                //Affichage du memu
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Menu Système d'équations linéaires :");
                Console.ResetColor();
                Console.WriteLine("Veuillez choisir un choix parmi les suivants :");
                Console.WriteLine("1.Système de Cramer.\n");
                Console.WriteLine("2.Inversion matricielle.\n");
                Console.WriteLine("3.Méthode de Jacobi.\n");
                Console.WriteLine("4.Comparer la vitesse d'exécution de différente méthode.\n");
                Console.WriteLine("5.Modifier le système d'équation\n");
                Console.WriteLine("6.Visualiser le système d'équation\n");
                Console.WriteLine("7.Quitter");


                //On boucle tant que l'utilisateur n'entre pas un entier.
                while (!int.TryParse(Console.ReadLine(), out choix)) {

                    Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                    Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                    Console.WriteLine("\nVous n'avez pas entré un nombre entier!\n\n"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                    Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                    Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
                }

                switch (choix) {

                    //Méthode de Cramer
                    case 1:

                        Console.Clear();

                        answer = sel.TrouverXParCramer();

                        if (answer != null) {
                            Console.WriteLine("Résolution du système d'équation par Cramer !! ");
                            Console.WriteLine("Les valeurs de X pour résoudre le système d'équation sont : \n{0}", answer);

                            _AfficherSELResult(answer);
                        }

                        //Message à la console, car le déterminant est null
                        else
                            Console.WriteLine(" Le déterminant est nul. Impossible d'utiliser la méthode de Cramer ");

                        break;

                    
                    //Méthode de la Matrice Inverse
                    case 2:

                        Console.Clear();
                        
                        answer = sel.TrouverXParInversionMatricielle();

                        if (answer != null) {
                            Console.WriteLine("Résolution du système d'équation par Inversion Matricielle !! ");
                            Console.WriteLine("Les valeurs de X pour résoudre le système d'équation sont : \n{0}", answer);

                            _AfficherSELResult(answer);
                        }
                        //Message à la console, car le déterminant est null
                        else
                            Console.WriteLine(" Le déterminant est nul. Impossible d'utiliser la méthode par inversion matricielle ");


                        break;

                    //Méthode de Jacobi
                    case 3:

                        Console.Clear();

                        answer = sel.TrouverXParJacobi(_ReadDouble("Veuiller entrer la valeur d'epsilon : "));

                        if (answer != null) {
                            Console.WriteLine("Les valeurs de X pour résoudre le système d'équation sont : \n{0}", answer);

                            _AfficherSELResult(answer);
                        }
                        //Message à la console, car le déterminant est null
                        else
                            Console.WriteLine("La convergence vers un point fixe ne peut être assuré. Impossible d'utiliser la méthode de Jacobi.");

                        break;


                    //Comparer vitesse d'exécution
                    case 4:
                        Console.Clear();

                        TimeSpan time;

                        //On test Cramer 
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Résolution par la méthode de Cramer : ");                        
                        time = _TempsDeCalcul(sel.TrouverXParCramer, "Le déterminant est nul. Impossible d'utiliser la méthode de Cramer!");

                        Console.WriteLine("La résolution d'équation c'est effectué en {0}\n", time);

                        //On test la méthode de matrice inverse
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Résolution par la méthode de l'inverse matricielle : ");                        
                        time = _TempsDeCalcul(sel.TrouverXParInversionMatricielle, "Le déterminant est nul. Impossible d'utiliser la méthode de l'inverse matricielle!");

                        Console.WriteLine("La résolution d'équation c'est effectué en {0}\n", time);


                        //On test la méthode de Jacobie
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Résolution par la méthode de Jacobi : ");                        
                        time = _TempsDeCalcul(sel.TrouverXParJacobi, _ReadDouble("Veuiller entrer la valeur d'epsilon : "), "La convergence vers un point fixe ne peut être assuré. Impossible d'utiliser la méthode de Jacobi.");

                        Console.WriteLine("La résolution d'équation c'est effectué en {0}", time);

                        Console.ResetColor();

                        break;

                        //Modifier un SEL
                    case 5:

                        sel = new(_CreerMatriceSEL()); //On créer un nouveau. L'ancien sera rammassé par le Garbage collector de C#

                        break;

                    //Afficher le système d'équation
                    case 6:

                        Console.Clear();
                        Console.WriteLine("Le système d'équation est : \n{0}", sel);

                        break;
                    
                    //Quitter
                    case 7:

                        quitter = true;

                        break;

                    //Arrive en cas de valeur invalide
                    default:

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine("Valeur invalide...");
                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut

                        break;
                }

                //On s'assure que le choix est valide 
                if (0 < choix && choix < 7) {
                    Console.WriteLine("\n\nPress any key to continue...\n");
                    Console.ReadKey(); //Pour mettre la console sur "pause"
                    Console.Clear();
                }
            } while (!quitter);

            Console.Clear(); //On efface la console

        

        }


        static void Main(string[] args) {

            int choix; //Déclaration des variables
            bool quitter = false;

            //Va contenir toutes les matrices qu'on va ajouter au fur et à mesure dans le programme.
            ListDictionary matrixContainer = new();
            string currentMatrixName = "@";//'@' est le char avant 'A' 

            //Va boucler pour permettre un retour au menu
            do {

                //Affichage du memu
                Console.WriteLine("Veuillez choisir un choix parmi les suivants : \n");
                Console.WriteLine("1.Effectuer des opérations matricielle.");
                Console.WriteLine("2.Résoudre un Système d'équation linéaire (SEL).");
                Console.WriteLine("3.Quitter");


                //On boucle tant que l'utilisateur n'entre pas un entier.
                while (!int.TryParse(Console.ReadLine(), out choix)) {

                    Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                    Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                    Console.WriteLine("\nVous n'avez pas entré un nombre entier!\n\n"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                    Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                    Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
                }

                //On choisi L'option que l'utilisateur à choisit
                switch (choix) {


                    //On veut faire des opérations matricielles
                    case 1:

                        Console.Clear();
                        //En passant les varriables par référence, on permet que lorsque l'on quitte le menu Matrice pour retourner au menu Principal, les valeurs ne sont pas effacé
                        _MenuMatrice(ref matrixContainer, ref currentMatrixName); //On va vers le menu Matrice
                        
                        break;


                    //Si on veut résoudre un SEL
                    case 2:

                        Console.Clear();
                        _MenuSEL();
                        

                        break;

                    
                    //Si on veut quitter le programme.
                    case 3:
                        quitter = true;
                        break;

                    //Arrive en cas de valeur invalide
                    default:

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine("Valeur invalide...");
                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut

                        break;
                }

                
            } while (!quitter);

            

            


        }
    }
}
