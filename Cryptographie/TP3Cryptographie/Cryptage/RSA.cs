using System;
using System.Numerics;

namespace TP3Cryptographie {
    static class RSA {



        /**********************************************************************************************************
        Ceci est un programme que j'avais fait comme TP5 de mon cours math discrète au Cégep (- Félix)
        Vous sembliez adorer la cryptographie alors je vous ai inclus ce travail avec le TP3 que vous avez demandé.
        J'ai renommer la fonction Main pour RSAMain pour empêcher toute ambiguité avec le compoliteur.
        **********************************************************************************************************/




        //Calcul du PGCD
        private static bool _PGCD(int nb1, int nb2) {
            int nb3 = nb1;
            //Pour effectuer le calcul du PGCD
            do {

                //Pour empêcher une erreur de division par 0 du modulo
                if (nb2 != 0) {


                    if (nb1 >= nb2) {
                        nb1 %= nb2;

                    }
                }

                if (nb1 != 0) {
                    if (nb2 > nb1) {
                        nb2 %= nb1;

                    }
                }

            } while ((nb1 != 0) && (nb2 != 0));



            if (nb1 == 0) {
                if (nb2 == 1) {

                    return true;
                }

                else {
                    Console.WriteLine("e n'est pas premier avec " + nb3 + "\nVeuillez entrer une autre valeur!");
                    return false;
                }
            }

            else {
                if (nb1 == 1) {
                    return true;
                }

                else {
                    Console.WriteLine("e n'est pas premier avec " + nb3 + "\nVeuillez entrer une autre valeur!");
                    return false;
                }
            }

        }


        //Nombre Premier
        private static bool _NbPremier(int nombre) {
            bool premier = true;
            for (int i = (nombre - 1); i > 1; i--) {

                //Le modulo d'un nombre premier ne sera jamais égal à 0 pour tout naturel différent de 1 et de lui-même.
                if (nombre % i == 0) {
                    i = 0; //Pour sortir de la boucle.

                    premier = false;
                }
            }

            //Pour dire que le chiffre est un nombre premier (comme 1, 0 et tous chiffres négatifs ne sont pas premier, mais ils peuvent avoir déjoué mon algorithme "complexe", cette condition va s'assurer que ce soit SEULEMENT LES VRAIS nombre premier que le programme va afficher comme premier. 
            if ((premier == true) && (nombre > 1)) {
                Console.WriteLine(nombre + " est un nombre premier!");
                return true;
            }

            else if ((premier == false) || (nombre <= 1)) {
                Console.WriteLine(nombre + " n'est pas un nombre premier!");
                return false;
            }

            else return false;

        }

        //Pour calculer le modulo inverse
        static BigInteger _ModInverse(int a, int n) {
            int i = n, v = 0, d = 1;
            while (a > 0) {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }




        //Fonction main (renommer en RSAMain pour l'inclure à se programme)
        public static void RSAMain() {


            //Nombre premier trouvé sur Wikipédia

            int p;// exemple = 7919;    Ou 23

            int q; //exemple = 6553;    Ou 13

            int e; //exemple = 11; // (11 est premier avec 7918 * 6552)     ou 7

            //Les gros nombres premier fonctionne si c'est seulement une chaîne de caractère sans espace et sans symbole. Sinon, on obtient une arithmetic overflow exception.
            //Les petits nombres premier sont théoriquement moins sécuritaires au niveau du chiffrement, mais il permette d'utiliser les espace et les symboles*

            BigInteger d;
            

            string mot_A_Coder; //*Parfois il peut y avoir des erreurs si les symboles ne sont pas des lettres. exmple : ☺☻♦◘
            string affichage = "";
            int dimension;

            Console.WriteLine("Veuillez entrer le mot que vous voulez crypter : ");

            mot_A_Coder = Console.ReadLine();

            do {
                Console.WriteLine("Veuillez entrer le paramètre p : ");
                while (!int.TryParse(Console.ReadLine(), out p)) ;
            } while (_NbPremier(p) == false);

            do {
                Console.WriteLine("Veuillez entrer le paramètre q : ");
                while (!int.TryParse(Console.ReadLine(), out q)) ;
            } while (_NbPremier(q) == false);

            do {
                Console.WriteLine("Veuillez entrer le paramètre e : ");
                while (!int.TryParse(Console.ReadLine(), out e)) ;
            } while (_PGCD((p - 1) * (q - 1), e) == false);

            int n = p * q;

            char[] charCryptage = mot_A_Coder.ToCharArray(); //Conversion de la variable mot_A_coder en tableau




            dimension = charCryptage.Length;
            int[] codeInteger = new int[dimension];
            BigInteger[] crypter = new BigInteger[dimension];
            BigInteger[] decrypter = new BigInteger[dimension];


            //Pour convertir de Char en Int. (Ce programme ne ne fait pas de groupe pour contrer l'analyse de fréquences.)
            for (int i = 0; i < crypter.Length; i++) {
                codeInteger[i] = (int)charCryptage[i] - 97; //97, car c'est la valeur de 'a' en ASCII

            }


            for (int i = 0; i < crypter.Length; i++) {

                crypter[i] = new BigInteger(Math.Pow(codeInteger[i], e)); //La position des lettres n'est pas la même que dans les notes de cours pour simplifier le code.

                crypter[i] %= n;

                affichage += crypter[i];
            }

            Console.WriteLine("Le message crypté est : " + affichage);

            Console.WriteLine("Décryptage du message en cours!");


            d = _ModInverse(e, (p - 1) * (q - 1));

            affichage = "";


            for (int i = 0; i < decrypter.Length; i++) {

                decrypter[i] = BigInteger.ModPow(crypter[i], d, n); ;

                decrypter[i] += 97; //On annule le - 97 du début
                affichage += (char)decrypter[i];


            }

            Console.WriteLine("Le message décodé est : {0}", affichage);
            


        }



    }
}

