using System;
using System.Collections.Generic;
using System.IO;


namespace Devoir1 {
    static class MethodDevoir1 {
        private static List<String> _formule = new(); //Variable local qui va contenir toutes les grammaires


        /***************************
        Entrée : Aucune
        Rôle   : Créer une grammaire
        Sortie : Aucune
        ***************************/
        public static void Create() {
            bool suivant = false;

            //On boucle tant qu'on veut entrer une grammaire
            do {
                Console.WriteLine("Entrez la grammaire : ");
                _formule.Add(Console.ReadLine()); //On stock la grammaire dans la List


                //On boucle tant que l'entrée est invalide
                while (true) {
                    Console.WriteLine("Voulez-vous ajouter une grammaire supplémentaire ? (o/n): ");
                    string reponse = Console.ReadLine();
                    reponse = reponse.ToUpper(); //Pour pouvoir gérer le cas si l'utilisateur entre la lettre en majuscule ou minuscule

                    if (reponse == "O") {
                        suivant = true; //On permet l'ajout de d'autres grammaires
                        break; //On sort de la boucle
                    }
                    else if (reponse == "N") {
                        suivant = false; //On permet de quitter la boucle do
                        break; //On sort de la boucle
                    }

                    //ENTRÉE INVALIDE
                    else {

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine($"{reponse} n'est pas une entrée valide!");
                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                        suivant = false;
                    }
                }
            } while (suivant);

            //On affiche les règles de grammaire
            Console.WriteLine("Les règles de la grammaire sont : \n");
            foreach (var grammaire in _formule) {
                Console.WriteLine(grammaire);
                
            }



        }

        /*******************************************************
        Entrée : Aucune
        Rôle   : Enregistrer ine grammaire dans un fichier texte
        Sortie : Aucune
        *******************************************************/
        public static void Enregistrer() {
            string grammaireStr = ""; 
            foreach (var grammaire in _formule) {
                grammaireStr += grammaire + "\n"; //On ajoute chaque grammaire dans un String
            }
            File.WriteAllText("Grammaire.txt", grammaireStr); //On crée / Overrite un document nommé Grammaire.txt et on écrit le string qui contient toute les règles de grammaire
            Console.WriteLine("Enregistrement effectuer avec succès!");
        }


        /***********************************************
        Entrée : Auncune
        Rôle   : On réinitialise la grammaire en mémoire
        Sortie : Aucune
        ***********************************************/
        public static void Reinitialiser() {

            _formule.Clear(); //Fonction de List qui permet d'effacer tous ce qu'elle contient
            Console.WriteLine("La grammaire a été réinitialisé avec succès!");
            
            
        }


        /**************************
        Entrée : Aucune
        Rôle   : Charger un fichier
        Sortie : Aucune
        **************************/
        public static void ChargerFichier() {

            try {

                Reinitialiser(); //Si on load 2 fois le même fichier, les grammaires ne se dupliqueront pas!

                String textLoad;

                textLoad = File.ReadAllText("Grammaire.txt"); //On met le contenu du fichier dans une grammaire

                Console.WriteLine("Fichier charger avec succès");

                Console.WriteLine($"\nLes grammaires sont :");

                //On affiche les grammaires et ont les ajoute à notre list (Les grammaires sont séparé par \n)
                foreach (var grammaire in textLoad.Split("\n")) {

                    _formule.Add(grammaire);
                    Console.WriteLine(grammaire);
                }
            }

            //Si le programme ne trouve pas le fichier Grammaire.txt
            catch {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Le fichier est inexistant!");
                Console.ResetColor();

                //On demande s'il veut créer un nouveau fichier (Boucle tant que l'entrée est valide
                while (true) {
                    Console.WriteLine("Voulez-vous créer un nouveau fichier ? (o/n): ");
                    string reponse = Console.ReadLine();
                    reponse = reponse.ToUpper();

                    if (reponse == "O") {
                        Enregistrer(); //Note le fichier sera donc vide
                        break;
                    }
                    else if (reponse == "N") {
                        
                        break;
                    }

                    //En cas d'entrée invalide
                    else {

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine($"{reponse} n'est pas une entrée valide!");
                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                        
                    }
                }

            }

        }

        
        /******************************
        Entrée : Aucune
        Rôle   : Modifier une grammaire
        Sortie : Aucune
        ******************************/
        public static void ModifierGrammaire() {

            //S'il n'y a pas de grammaire, on affiche un message d'erreur
            if(_formule.Count == 0) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();

                Console.WriteLine("Il n'y a aucune grammaire alors on ne peut pas effectuer de modification");
                Console.ResetColor();

            }

            //La modification à lieu ici
            else {

                //Les options de modifications
                Console.WriteLine("Quelle option de grammaire voulez-vous effectuer?");
                Console.WriteLine("1. Ajouter une grammaire");
                Console.WriteLine("2. Remplacer une grammaire");
                Console.WriteLine("3. Supprimer une grammaire");

                int choixOperation; //Va contenir le numéro de l'opération que l'utilisateur veut effectuer
                //On boucle tant que l'utilisateur n'entre pas un entier entre 1 et 3 inclus.
                while (!int.TryParse(Console.ReadLine(), out choixOperation) || !(choixOperation > 0 && choixOperation < 4)) {

                    Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                    Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                    Console.WriteLine("\nVous n'avez pas entré un nombre entre 1 et 3!\n\n"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                    Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                    Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
                }


                //On affiche les règles de la grammaire et ont les numérotes
                Console.WriteLine("Les règles de la grammaire sont : \n");

                int i = 1; //Va permettre de numéroter les grammaires
                foreach (var grammaire in _formule) {
                    Console.WriteLine("{0}. {1}", i, grammaire);
                    i++;
                }

                //On demande à l'utilisateur de choisir la grammaire sur laquelle il veut effectuer l'opération
                int choixGrammaire = 0;
                //L'utilisateur n'a pas besoin de choisir une grammaire pour pouvoir en ajouter une.
                if (choixOperation != 1) {
                    Console.Write("Veuillez choisir la règle sur laquelle vous voulez effectuer l'opération : ");

                    
                    //On boucle tant que l'utilisateur n'entre pas un entier et entre 1 et le nombre de grammaire.
                    while (!int.TryParse(Console.ReadLine(), out choixGrammaire) || !(choixGrammaire > 0 && choixGrammaire < i)) {

                        Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                        Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                        Console.WriteLine("\nVous n'avez pas entré un nombre entre 1 et {0}!\n\n", (i - 1)); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                        Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                        Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
                    }
                }

                //Permet de choisir l'opération qu'on veut effectuer
                switch (choixOperation) {

                    //Ajouter une grammaire
                    case 1:

                        Create(); //On ajoute une grammaire (même fonction que pour en créer une)

                        break;


                    //Remplacer une grammaire
                    case 2 :
                        _SupprimerGramaire(choixGrammaire - 1); //On supprime la grammaire ( - 1, car les numéros afficher à la console partent de 1 et non de 0)
                        Create(); //On ajoute une ou plusieurs grammaires pour la remplacer
                        

                        break;

                    case 3:

                        _SupprimerGramaire(choixGrammaire - 1); //On supprime la grammaire ( - 1, car les numéros afficher à la console partent de 1 et non de 0)

                        Console.WriteLine("La supression c'est effectué avec succès!");

                        //On affiche les grammaires
                        Console.WriteLine("Les règles de la grammaire sont : \n");
                        foreach (var grammaire in _formule) {
                            Console.WriteLine(grammaire);

                        }



                        break;

                }


            }

        }



        /**************************************************
        Entrée : L'index de la grammaire à supprimer
        Rôle   : Supprimer une grammaire
        Sortie : Aucune
        **************************************************/
        private static void _SupprimerGramaire(int index) {

            _formule.RemoveAt(index); //On retire la grammaire de la liste

        }


        /********************************
        Entrée : Aucune
        Rôle   : On valide les grammaires
        Sortie : Aucune
        ********************************/
        public static void Validateur() {

            
            //Pour éliminer les espaces dans les règles
            for(int i = 0; i < _formule.Count; i++) {

                _formule[i] = _formule[i].Replace(" ", ""); //On enlève les espaces
                _formule[i] = _formule[i].Replace("\r", ""); //On enlève les \r

                //On enlève les entrées vides des grammaires (arrive lorsqu'on charge une grammaire à partir d'un fichier)
                if (_formule[i] == "\n" || _formule[i] == "") {
                    _formule.RemoveAt(i);
                    i--; //Car la taille de la liste va réduire
                }

            }

            //Expression Lambda qui regarde s'il y a au moins une grammaire commençant par S
            if (_formule.Contains(_formule.Find(x => x.StartsWith("S")))) {
                    

                //Expression lambda qui regarde s'il y a au moins une expression qui regarde s'il y a un 0, un 1 ou un e après la flêche
                //Entre dans le if si ce N'est PAS le cas
                if (!_formule.Contains(_formule.Find(x => x.Contains(">0") || x.Contains(">1") || x.Contains(">e")))) {
                

                    //Message d'erreur
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Beep();
                    Console.WriteLine("Une de vos règles de grammaire est invalide!");
                    Console.ResetColor();
                    Console.WriteLine("Retour au menu principal...");

                    return; //Retour au menu principal;

                }

                Console.WriteLine("Votre grammaire est correcte !!");

                String choix;
                Char[] bytes;
                bool nombreCorrect = false;

                //On demande d'entrer une expression (ex. : 0110, 10111, etc.)
                //On boucle tant que cette expression a des valeurs différentes de '0' et '1'
                do {
                    Console.Write("Veuillez entrer l'expression : ");
                    choix = Console.ReadLine();
                    bytes = choix.ToCharArray(); //Convertit l'entrée en Char[]

                    //On boucle pour évaluer toutes les valeurs du Char[]
                    for (int i = 0; i < bytes.Length; i++) {

                        //S'il n'y a pas de '0' ou de '1'
                        if (bytes[i] != '0' && bytes[i] != '1') {

                            //Le nombre est incorrect
                            nombreCorrect = false;

                            //Message d'erreur
                            Console.Beep();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("L'entrée est invalide! Seul des 0 et des 1 sont permis!");
                            Console.ResetColor();

                            break; //On quitte la boucle for pour demander d'entrer une nouvelle valeur
                        }

                        nombreCorrect = true; //le bool est à true, l'entrée à cette itération est correct

                    }
                } while (!nombreCorrect); 

                _EtatTransition(bytes); //On appel la fonction pour évaluer l'expression

            }

            //Il n'y a pas de règle qui commence avec S.
            else {

                //On boucle tant que l'utilisateur n'entre pas une grammaire qui commence par S
                do {
                    Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                    Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                    Console.WriteLine("\nVous n'avez pas de grammaire commencant avec l'élément de départ(S) !\n\n"); //On affiche un message expliquant qu'il n'a pas entrer un entier positif.

                    Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                    Console.WriteLine("Veuillez entrer votre grammaire commencant avec l'élément de départ : "); //On affiche un message dans la console pour demander d'entrer un entier positif.
                    _formule.Add(Console.ReadLine());
                } while (!_formule.Contains(_formule.Find(x => x.StartsWith("S"))));
            }

        }


        /**************************************************
        Entrée : char[] représentant l'expression à évaluer
        Rôle   : Valider une expression
        Sortie : Aucune
        **************************************************/
        private static void _EtatTransition(char[] expression) {
            int i = 0; //Va correspondre à l'index où la grammaire est situé.
            int arrow; //Va permettre le bon fonctionnement de programme dépendamment de l'utilisation du type de flêche ( > ou ->)
            bool isEtatFinal = false; //Booléen qui dit si c'est l'état final
            int choix;

            //Pour permettre de choisir le type de flêche utilisé
            Console.WriteLine("Veuillez choisir le type de flêche que vous avez utilisé.");
            Console.WriteLine("1. >");
            Console.WriteLine("2. ->");


            //On boucle si l'entrée n'est pas un entier ou elle est différente de 1 ou 2.
            while (!int.TryParse(Console.ReadLine(), out choix) || (choix == 1 && choix == 2)) {

                Console.Beep(); //On émet un son pour capter l'attention de l'utilisateur
                Console.ForegroundColor = ConsoleColor.Red; //On met la couleur des lettres en rouge pour montrer une action illégal
                Console.WriteLine("\nVous n'avez pas entré un nombre entre 1 et 2!\n\n"); //On affiche un message expliquant qu'il n'a pas entrer 1 ou 2.

                Console.ResetColor(); //On remet la couleur de la console à sa couleur par défaut
                Console.WriteLine("Veuillez choisir le type de flêche que vous avez utilisé.");
                Console.WriteLine("1. >");
                Console.WriteLine("2. ->");
                Console.WriteLine("Veuillez entrer un nombre entier correspondant à la valeur de votre choix : ");  //On affiche un message dans la console pour demander d'entrer un entier positif.
            }

            //C'est la flêche >
            if (choix == 1) {
                arrow = 0;
            }

            //C'est la flêche ->
            else
                arrow = 1;


            //On boucle pour chaque 0 et 1 dans l'expression
            for (int j = 0; j < expression.Length; j++) {

                //À la première itération
                if (j == 0) {

                    try {

                        //On trouve l'index de la grammaire qui commence par S et qui prend un chemin ayant la même valeur que celui de l'expression
                        i = _formule.IndexOf(_formule.Find(x => x.StartsWith($"S>{expression[j]}")|| x.StartsWith($"S->{expression[j]}")));
                        Console.WriteLine(_formule[i]); //On affiche la grammaire choisi
                    }

                    //Arrive si on bloque (S'il n'y a plus de chemin possible à emprunter)
                    catch {
                        isEtatFinal = false; //Ce n'est pas un état final
                        break; //On quitte la boucle
                    }
                }


                //Si ce n'est pas la première itération, ni la dernière
                else if (j != 0 && j != expression.Length - 1) {

                    try {

                        //On trouve l'index d'une grammaire qui commence vers où la grammaire précédante menais et qui prend un chemin ayant la même valeur que celui de l'expression
                        i = _formule.IndexOf(_formule.Find(x => x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}>{expression[j]}") || x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}->{expression[j]}")));
                        Console.WriteLine(_formule[i]);
                    }

                    //Arrive si on bloque (S'il n'y a plus de chemin possible à emprunter)
                    catch {

                        isEtatFinal = false; //Ce n'est pas un état final
                        break; //On quitte la boucle

                    }
                }

                //Lors de la dernière itération
                else {

                    List<String> tempStrList;

                    //La liste contient toute les expressions commençant par la fin de la grammaire précédante et qui prend un chemin ayant la même valeur que celui de l'expression
                    tempStrList = _formule.FindAll(x => x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}>{expression[j]}") || x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}->{expression[j]}") || x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}>e") || x.StartsWith($"{_formule[i].ToCharArray()[3 + arrow]}->e"));

                    //On regarde si la liste est de longeur (3 + arrow) Ex : A>e (Length == 3), A->1 (Length == (3 + arrow) où arrow == 1) donc Length == 4)
                    if (tempStrList.Contains(tempStrList.Find(x => x.Length == (3 + arrow)))) {

                        isEtatFinal = true; //C'est un état final

                        i = tempStrList.FindIndex(x => x.Length == (3 + arrow)); //on trouve l'index de cette grammaire

                    }

                    //Ce n'est pas un état final
                    else {
                        isEtatFinal = false;

                        i = 0;
                    }


                    Console.WriteLine(tempStrList[i]); //On affiche la grammaire


                }

            }

            //Si c'est un état final
            if (isEtatFinal) {

                Console.ForegroundColor = ConsoleColor.Green; //Affichage en vert parce qu'on est content
                Console.WriteLine("L'expression est valide!");
                Console.ResetColor();
            }

            //Ce n'est pas un état final
            else {
                Console.ForegroundColor = ConsoleColor.DarkRed; //Affichage en rouge foncé
                Console.WriteLine("L'expression est invalide");
                Console.ResetColor();
            }
            
        }

    }


}
