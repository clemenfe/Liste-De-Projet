using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjetDeSession
{
    public class CaseCmCoin : ICase
    {

        public Point position { get; } //Représente la position globale (matrice 9x9) de la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes
        public Point region { get; } //Représente dans quelle 'région' (matrice 3x3) où se situe la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes

        private const int _HOWMANYMAXCORNER = 4; //Constante qui garde le nombre de coin

        private List<int> cmCoin; //Représente les commentaires en coins (jusqu'à 4 chiffres DIFFÉRENTS) ---- Accessible seulement par les méthodes qui y sont associées

        public CaseCmCoin(Point _position, Point _region)
        {
            position = _position;
            region = _region;
            cmCoin = new List<int>();
        }

        public void changeValue(int cm)
        {

            if (cmCoin.Where(c => c.Equals(cm)).Any()) //Les nombres en commentaires doivent être différents
            {
                cmCoin.Remove(cm);
            }

            else if (cmCoin.Count >= _HOWMANYMAXCORNER) //Les commentaires dans les coins ne peuvent être plus de 4
            {
                throw new Exception();
            }

            else
            {
                cmCoin.Add(cm);
                //cmCoin.Sort(); //Trier la liste après chaque insertion permet de la garder triée (Utile pour l'affichage) ---- Potentiellement à optimiser (insertSort)
            }

        }


        //Méthode qui nous dit combien il y a de coin (Utile pour la sauvegarde)
        public int HowManyMaxCorner() {

            return _HOWMANYMAXCORNER;

        }

        //Méthode qui nous dit la valeur d'un coin précis (Utile pour la sauvegarde)
        public int? GetCmCornerValue(int index) {

            try {
                return cmCoin[index];
            }

            //Pour prévenir un IndexOutOfBoundException
            catch {
                return null;
            }
        }

        public void Reset() {
            cmCoin.Clear();
        }

        public int? getOldValue()
        {
            try
            {
                return cmCoin.Last();
            }
            catch
            {
                return null;
            }
        }
    }
}
