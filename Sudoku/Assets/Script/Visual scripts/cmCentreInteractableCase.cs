using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class cmCentreInteractableCase : AbstractInteractableCase
{
    private List<int> currentvalues = new List<int>();

    protected override void Start()
    {
        base.Start();
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
                int nonNullValue = (int)newValue;

                gameController.NumberEntry(nonNullValue, caseType, this);

                

                
            }
            /*else
            {
                foreach(int value in currentvalues)
                {
                    gameController.NumberEntry(value, caseType, this); //On retire le commentaire en centre déjà présent
                }
                currentvalues.RemoveRange(0, currentvalues.Count);
                textBox.text = "";
            }*/
        }
    }

    //Est utilisé pour faire afficher les notes aux centres après un chargement
    public override void SetValue(int? value) {

        int nonNullValue = (int)value;

        if (currentvalues.Where(v => v == nonNullValue).Any())
        {
            //On retire le commentaire en centre déjà présent
            currentvalues.Remove(nonNullValue);
        }
        else
        {
            currentvalues.Add(nonNullValue);
        }

        currentvalues.Sort();
        textBox.text = "";

        foreach (int v in currentvalues) {
            textBox.text += v;
        }
    }

    public void ClearInput() {
        textBox.text = "";
        currentvalues.RemoveRange(0, currentvalues.Count());
    }
}
