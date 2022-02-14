using System;

namespace Devoir1 {
    class Program {

        //Fonction main
        static void Main(string[] args) {
            int choix; //Déclaration des variables
            bool quitter = false;

            //Va boucler pour permettre un retour au menu
            do {

                //Affichage du memu
                Console.WriteLine("Veuillez choisir un choix parmi les suivants :");
                Console.WriteLine("1.Créer une grammaire.\n");
                Console.WriteLine("2.Modifier une grammaire.\n");
                Console.WriteLine("3.Réinitialiser une grammaire\n");
                Console.WriteLine("4.Charger un fichier\n");
                Console.WriteLine("5.Enregistrer un fichier\n");
                Console.WriteLine("6.Validateur Syntaxique\n");
                Console.WriteLine("7.Quitter");
                

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

                    case 1:
                        MethodDevoir1.Create(); //Créer une grammaire
                        break;

                    case 2:

                        MethodDevoir1.ModifierGrammaire(); //Modifier une grammaire
                        
                        break;

                    case 3:

                        MethodDevoir1.Reinitialiser(); //Réinitialiser une grammaire
                        break;

                    case 4:

                        MethodDevoir1.ChargerFichier(); //Charger un fichier
                        break;

                    case 5:

                        MethodDevoir1.Enregistrer(); // Enregistrer un fichier
                        break; 

                    case 6:

                        MethodDevoir1.Validateur(); //Valider une expression
                        break;

                    //Si on veut quitter le programme.
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
                }
            } while (!quitter);

        }
    }
}
