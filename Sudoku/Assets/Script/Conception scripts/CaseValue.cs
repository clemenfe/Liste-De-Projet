using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDeSession
{
    public class CaseValue : ICase
    {

        public Point position { get; } //Représente la position globale (matrice 9x9) de la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes
        public Point region { get; } //Représente dans quelle 'région' (matrice 3x3) où se situe la case -- Valeur inchangeable après la création d'un board pour des raisons évidentes




        public int? value { get; set; } //Représente la valeur INSÉRÉE dans une case

        public bool isRed { get; set; } //Représente l'état booléene de si la case doit être rouge (à cause d'un doublons avec une ou plusieurs cases dans sa colonne/rangé/région3x3)
        public CaseValue(Point _position, Point _region)
        {
            value = null;
            position = _position;
            region = _region;
            isRed = false;
        }

        public void changeValue(int _value)
        {
            if (value == _value) { value = null; } //Si la valeur est égale à celle à l'entré, enlever la valeur
            else { value = _value; } //Si non, changer la valeur
        }

        public void Reset() {
            value = null;
        }

        public int? getOldValue()
        {
            return value;
        }
    }
}
