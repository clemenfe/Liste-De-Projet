using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDeSession
{
    public class CaseCmMilieu : ICase
    {
        public Point position { get; } //Représente la position globale (matrice 9x9) de la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes
        public Point region { get; } //Représente dans quelle 'région' (matrice 3x3) où se situe la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes




        private List<int> cmMilieu;  //Représente les commentaires dans le milieu (chiffres DIFFÉRENTS) ---- Accessible seulement par les méthodes qui y sont associées

        public CaseCmMilieu(Point _position, Point _region)
        {
            position = _position;
            region = _region;
            cmMilieu = new List<int>();
        }

        public void changeValue(int cm)
        {

            if (cmMilieu.Where(c => c.Equals(cm)).Any()) //Les nombres en commentaires doivent être différents
            {
                cmMilieu.Remove(cm);
            }

            else
            {
                cmMilieu.Add(cm);
                //cmMilieu.Sort(); //Trier la liste après chaque insertion permet de la garder triée (Utile pour l'affichage) ---- Potentiellement à optimiser (insertSort)
            }


        }


        //On retourne un int contenant un commentaire au milieu (Utile pour la sauvegarde)
        public int? GetCmMilieuValue(int index) {


            try {
                return cmMilieu[index];
            }

            //Pour prévenir un index out of bound exception
            catch {
                return null;
            }

        }

        public int MaxCommentInCenter() {

            return 8; //9 commentaires - 1 (car on part de 0) = 8
        }

        public void Reset() {
            cmMilieu.Clear();
        }

        public int? getOldValue()
        {
            try
            {
                return cmMilieu.Last();
            }
            catch
            {
                return null;
            }
        }
    }
}
