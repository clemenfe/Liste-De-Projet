using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDeSession
{
    public class NumberEntryCommand : ICommand
    {
        private ICase caseSelectionne;
        private int newNum;
        private int? oldNum;
        private AbstractInteractableCase caseVisuelle;


        public NumberEntryCommand(int newNum, ICase caseSelectionne, AbstractInteractableCase caseVisuelle)
        {
            this.newNum = newNum;
            this.caseSelectionne = caseSelectionne;
            this.caseVisuelle = caseVisuelle;
            oldNum = caseSelectionne.getOldValue();
        }

        public void execute() 
        {
            executeWith(newNum);
        }

        
        public void undo() //Pour undo tel que demandé dans notre programme
        {
            executeWith(newNum);
            if (caseSelectionne.GetType().Equals(typeof(CaseValue)) && oldNum != null)
                executeWith((int)oldNum);
        }

        private void executeWith(int num)
        {
            caseSelectionne.changeValue(num);
            caseVisuelle.SetValue(num);
            if (caseSelectionne.GetType().Equals(typeof(CaseValue))) //Si on a modifié une case
            {
                GameController.Instance.getGameBoard().verifyDuplicate(); //Vérifie l'ensemble des éléments sur la grille afin d'en noter ceux qui ont un doublon
            }
        }
        /*
        public void redo() //Pour undo tel que demandé dans notre programme, il s'agit simplement de re-execute la command étant donnée 1. si on veut annuler l'insertion d'un nombre, il faut simplement insérer le même nombre 2. si on veut annuler la suppression d'un nombre, il s'agit simplement de ré-insérer le nombre en question ; même principe pour undo
        {
            execute();
        }
        */
    }
}
