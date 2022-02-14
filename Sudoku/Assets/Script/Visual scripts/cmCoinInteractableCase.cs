using ProjetDeSession;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class cmCoinInteractableCase : AbstractInteractableCase
{
    public int? currentValue = null;
    private CmsInCase cmsInCase;

    protected override void Start()
    {
        base.Start();

        cmsInCase = GetComponentInParent<CmsInCase>();
    }
    
    protected override void getInputForSudoku()
    {
        string input = sudokuInputs.getSudokuInput();


        if (input != null)
        {
            int? newValue = null;
            try
            {
                newValue = int.Parse(input);
            }
            catch
            {
                newValue = null;
            }

            if (newValue != null)
            {
                if (newValue == currentValue)
                {
                    checkForRemoval(currentValue);
                }
                else
                {

                checkForRemoval(currentValue);
                checkForRemoval(newValue);

                    try
                    {
                        gameController.NumberEntry((int)newValue, caseType, this); //On ajoute le commentaire en coin
                    }
                    catch
                    {
                        Debug.LogError("Trop de notes");
                    }
                }
            }
            /*else
            {
                if (currentValue != null)
                    gameController.NumberEntry((int)currentValue, caseType, this); //On retire la valeur courante des commentaires en coin
                currentValue = null;
                textBox.text = "";
            }*/
        }
    }

    public override void SetValue(int? value)
    {
        if (value == currentValue)
        {
            currentValue = null;
            base.SetValue(null);
        }
        else
        {
            currentValue = value;
            base.SetValue(value);
        }
    }

    private void checkForRemoval(int? value)
    {
        if (value != null && cmsInCase.anyCoinValueIs((int)value))
        {
            gameController.NumberEntry((int)value, caseType, this); //On retire le cm qu'on veut entrer dans le commentaire en coin
            cmsInCase.removeTextValueFromCoin((int)value);
        }
    }

    public void removeTextValue()
    {
        currentValue = null;
        textBox.text = "";
    }
}
