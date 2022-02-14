using System;
using System.Collections.Generic;
using TP3Cryptographie.Méthode_d_extension;
using System.Linq;

namespace TP3Cryptographie {
    static class Chiffrement {

        

        //private static byte[] _vecteurInit; //variable qui contient le Vecteur d'initialisation
        private static byte? _vecteurInit; //variable qui contient le Vecteur d'initialisation



        //Fonction pour chiffrer
        public static string Chiffrer(string msg, string clef) {

            _SingletonVI(); //On initialise le vecteur d'initialisation s'il n'est pas initialisé.

            IEnumerable<char> transposed = _Transposition(msg, clef.Split(' ').ToInt(), true); //On applique la clef de transposition

            List<int> chiffred = new(); //On crée une liste qui va contenir les bits chiffrées

            //On boucle pour effectuer le chiffrement
            for (int i = 0; i < transposed.Count(); i++) {


                //Lors de la première itération
                if (i == 0)
                    chiffred.Add(transposed.First() ^ (byte)_vecteurInit);

                //Lors des autres itération
                else 
                    chiffred.Add(transposed.ElementAt(i) ^ chiffred.ElementAt(i - 1));

            }

            IEnumerable<char> chiffredSymbol = chiffred.Select(x => (char)x); //On converti les éléments en char pour avoir le Symbol

            return string.Join("", chiffredSymbol); //On converti l'IEnumerable<char> en un string

         }

        //Fonction pour déchiffrer
        public static string Dechiffrer(string msg, string clef) {

            _SingletonVI();

            IEnumerable<char> byteMsg = msg.Select(x => x); //On converti le message en IEnumerable de char

            List<int> unChiffred = new();

            //On boucle pour effectuer le déchiffrement
            for (int i = 0; i < byteMsg.Count(); i++) {


                //Lors de la première itération
                if (i == 0)
                    unChiffred.Add(byteMsg.First() ^ (byte)_vecteurInit);

                //Lors des autres itération
                else
                    unChiffred.Add(byteMsg.ElementAt(i) ^ byteMsg.ElementAt(i - 1));

            }

            string almostDecryptMsg = string.Join("", unChiffred.Select(x => (char)x)); //On cast chaque élément en char pour ensuite les convertir en string

            IEnumerable<char> unTransposed = _Transposition(almostDecryptMsg, clef.Split(' ').ToInt(), false); //On transpose vers la gauche

            return string.Join("", unTransposed.Select(x => x)); //On cast chaque élément en char pour ensuite les convertir en string

        }


        //Cette fonction permet de générer un vecteur d'initialisation aléatoire.
        //En ce basant sur le patron Singleton, nous pouvons nous assurer que le vecteur sera le même pour le cryptage et le décryptage
        //tout en s'assurant qu'il est généré aléatoirement.
        private static void _SingletonVI() {

            //S'il n'est pas initialisé, on l'initialise.
            if (_vecteurInit is null) {
                                
                Random _randomGenerator = new(); //On crée un générateur de nombre

                _vecteurInit = _randomGenerator.RandomByte(); //J'utilise ma méthode personnalisé pour créer un byte

                /* Mauvais, car Byte est un unsigned int de 8 bits
                 _vecteurInit = new byte[8]; //On l'initialise sur 8 bit.

                //On boucle sur chaque octet pour mettre des 0 ou des 1 généré aléatoirement
                for (int i = 0; i < _vecteurInit.Length; i++)
                    _vecteurInit[i] = (byte)_randomGenerator.Next(0, 2);

                */


            }

        }


        /*INCORECT*/
        private static IEnumerable<char> _Transposition(string msg, IEnumerable<int> key, bool isChiffring) {

            int row; //Va être le nombre de rangée dans le tableau pour transposer.

            //On essaie de trouver le nombre de ranger qui va permettre d'entrer tous les charactères selon le nombre de colonne.
            int tempMsgLenghtAugmented = msg.Length;
            while (tempMsgLenghtAugmented % key.Count() != 0)
                tempMsgLenghtAugmented++;

            row = tempMsgLenghtAugmented / key.Count();




            char[,] transposedAsTableau = new char[row, key.Count()]; //On déclare un tableau de char
            char[] transposed = new char[row * key.Count()];

            //Comme le dit le devis, on suppose une clef correcte.

            //Lorsqu'on chiffre!
            if (isChiffring) {

                
                

                //On boucle pour positionner la colonne 

                for (int i = 0, k = 0; i < row; i++) {

                    //j sert à positionner la rangée
                    for (int j = 0; j < key.Count(); j++, k++) {
                        //k sert à choisir le charactère dans le message

                        if (k == msg.Length)
                            goto InversionDesAxes;

                        //-1, car on part à l'index 0
                        transposedAsTableau[i, key.ElementAt(j) - 1] = msg[k];
                    }

                }

            InversionDesAxes:

                //On boucle pour mettre les éléments dans le tableau
                for (int i = 0, k = 0; i < transposedAsTableau.GetLength(1); i++) {


                    for (int j = 0; j < transposedAsTableau.GetLength(0); j++, k++) {

                        transposed[k] = transposedAsTableau[j, i]; //On inverse les colonnes et les rangées

                    }
                }

            }


            //Si on déchiffre
            else {

                //On "inverse" les axes du messages.
                char[] msgInverse = msg.ToCharArray();
                int lastIndexJ = 0; //Pour pouvoir ce souvenir sur qu'elle "rangée on itère" (Valeur de départ de j)

                //On boucle
                for(int i = 1, j = row; i < msg.Length; i++) {

                    
                    msgInverse[i] = msg[j]; //On affecte la valeur de chaque Char du msgInverse à celui du msg où on saute chaque fois la valeur de row pour "simuler un tableau 2D"

                    j += row; //On augmente j avec la valeur de row

                    //Si j est plus grand ou égale à la longueur du message, on remet à sa valeur de départ plus 1. (on simule ainsi un changement de rangée d'un tableau 2D)
                    if (j >= msg.Length) {
                        
                        lastIndexJ++; //La valeur de départ augmente

                        j = lastIndexJ; //On met j à la valeur de départ

                    }
                        


                }


                //On boucle pour mettre les éléments dans le tableau
                //On boucle pour positionner la colonne 
                for (int i = 0, k = 0, z = -1; i < row; i++) {

                    //j sert à positionner la rangée
                    for (int j = 0; j < key.Count(); j++, k++) {
                        //k sert à choisir le charactère dans le message

                        if (k >= key.Count()) {
                            k = 0;
                            z += key.Count();
                        }

                                                            //-1, car on part à l'index 0
                        transposedAsTableau[i, j] = msgInverse[key.ElementAt(k) + z];
                    }
                    
                }

                //On boucle pour mettre les éléments dans le tableau
                for (int i = 0, k = 0; i < transposedAsTableau.GetLength(0); i++) {


                    for (int j = 0; j < transposedAsTableau.GetLength(1); j++, k++) {

                        transposed[k] = transposedAsTableau[i, j]; //On inverse les colonnes et les rangées

                    }
                }


            }

            

            return transposed;
        
        }



    }


      
    
}
