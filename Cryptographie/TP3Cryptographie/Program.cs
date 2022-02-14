using System;

namespace TP3Cryptographie {
    class Program {
        static void Main(string[] args) {

            int choix; //Déclaration des variables
            bool quitter = false;

            string msgCrypt_OR_Uncrypt; //Le message retourné par la fonction de décryptage
            string msg; //Le message à crypter ou a décrypter
            string key; //La clef de chiffrement

            //Va boucler pour permettre un retour au menu
            do {

                //Affichage du memu
                Console.WriteLine("Veuillez choisir un choix parmi les suivants :\n\n");
                Console.WriteLine("1. Chiffrer un message par Cipher Block Chaining.\n");
                Console.WriteLine("2. Déchiffrer un message par Cipher Block Chaining.\n");
                Console.WriteLine("3. Chiffrer et déchiffrer le même message par Cipher Block Chaining.\n");
                Console.WriteLine("4. Chiffrer par RSA (BONUS).\n");
                Console.WriteLine("5. Quitter");


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

                    //Pour chiffrer en CBC
                    case 1:
                        Console.Write("Veuillez entrer le message à crypter : ");
                        msg = Console.ReadLine();

                        Console.Write("\n\nVeuillez entrer la clef de cryptage : ");
                        key = Console.ReadLine();

                        msgCrypt_OR_Uncrypt = Chiffrement.Chiffrer(msg, key);

                        Console.WriteLine($"\n\nLe message crypté est : {msgCrypt_OR_Uncrypt}");

                        break;

                    //Pour déchiffrer en CBC
                    case 2:

                        Console.Write("Veuillez entrer le message à décrypter : ");
                        msg = Console.ReadLine();

                        Console.Write("\n\nVeuillez entrer la clef de décryptage : ");
                        key = Console.ReadLine();

                        msgCrypt_OR_Uncrypt = Chiffrement.Dechiffrer(msg, key);

                        Console.WriteLine($"\n\nLe message décrypté est : {msgCrypt_OR_Uncrypt}");
                        break;


                    //Pour chiffrer et déchiffrer en CBC
                    case 3:
                        Console.Write("Veuillez entrer le message à crypter : ");
                        msg = Console.ReadLine();

                        Console.Write("\n\nVeuillez entrer la clef de cryptage : ");
                        key = Console.ReadLine();

                        msgCrypt_OR_Uncrypt =  Chiffrement.Chiffrer(msg, key); //On Chiffre le message

                        Console.WriteLine($"\n\nLe message crypté est : {msgCrypt_OR_Uncrypt}");

                        msgCrypt_OR_Uncrypt = Chiffrement.Dechiffrer(msgCrypt_OR_Uncrypt, key); //On déchiffre le même message

                        Console.WriteLine($"\n\nLe message décrypté est : {msgCrypt_OR_Uncrypt}");

                        break;

                    //Pour chiffrer/déchiffrer en RSA (Bonus)
                    case 4:

                        RSA.RSAMain(); //Réinitialiser une grammaire
                        break;

                    
                    //Si on veut quitter le programme.
                    case 5:
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
                if (0 < choix && choix < 5) {
                    Console.WriteLine("\n\nPress any key to continue...\n");
                    Console.ReadKey(); //Pour mettre la console sur "pause"
                    Console.Clear();
                }
            } while (!quitter);


        }
    }
}
