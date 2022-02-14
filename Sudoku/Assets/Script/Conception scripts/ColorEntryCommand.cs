using ProjetDeSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Conception_scripts
{
    class ColorEntryCommand : ICommand
    {
        private Color newColor;
        private Color oldColor;
        private AbstractInteractableCase caseVisuelle;

        public ColorEntryCommand(Color newColor, AbstractInteractableCase caseVisuelle)
        {
            this.newColor = newColor;
            this.caseVisuelle = caseVisuelle;
            oldColor = caseVisuelle.currentColor;
        }

        public void execute()
        {
            caseVisuelle.SetColor(newColor);
        }

        public void undo()
        {
            caseVisuelle.SetColor(oldColor);
        }
    }
}
