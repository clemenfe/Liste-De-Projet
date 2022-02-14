using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3Cryptographie.Méthode_d_extension {
    public static class EnumerableExtension {

        //Pour convertir un IEnumerable<T> en IEnumerable<int>
        public static IEnumerable<int> ToInt<T>(this IEnumerable<T> source) {
                       
            //On boucle pour chaque élément.
            foreach (T token in source) {

                //Il est important que le type de l'élément (T) puisse avoir un ToString() qui soit des chiffres; (override si custom Object)
                if (int.TryParse(token.ToString(), out int intToken))
                    yield return intToken;

                //Si le ToString du token ne ce converti pas en int, on passe à la prochaine itération
                else
                    continue;

            }
                        

        }


    }


    public static class RandomExtension {

        //Pour pouvoir générer un bit aléatoire
        public static byte RandomByte(this Random random) {

            byte[] tempByte = new byte[1]; //Car la fonction prend un array en paramètre
                        
            random.NextBytes(tempByte);//On génère le nombre aléatoire

            return tempByte[0]; //On affecte le nombre à la variable

            

        }


    }
}
