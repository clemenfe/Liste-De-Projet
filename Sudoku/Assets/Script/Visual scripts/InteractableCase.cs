using ProjetDeSession;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableCase : AbstractInteractableCase
{
    [SerializeField] Color errorColor;
    [SerializeField] GameObject cms;

    private int? currentValue = null;
    private CaseValue caseValue;
    private BoxCollider2D inputDetectionBox;

    protected override void Start()
    {
        base.Start();

        inputDetectionBox = GetComponent<BoxCollider2D>();
    }

    public override void setCaseType(ICase caseT)
    {
        base.setCaseType(caseT);
        if (caseT.GetType().Equals(typeof(CaseValue)))
        {
            caseValue = (CaseValue)caseT;
        }
    }

    private void Update()
    {
        if (currentValue != null)
        {
            cms.SetActive(false);
        }
        else
        {
            cms.SetActive(true);
        }

        if (selected)
        {
            caseSelected();
        }
        else
        {
            if (caseValue.isRed)
            {
                caseError();
            }
            else
            {
                caseUnselected();
            }
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
                /*if (newValue == currentValue)
                {
                    currentValue = null;
                    textBox.text = "";
                }
                else
                {
                    
                    currentValue = newValue;
                    textBox.text = newValue.ToString();
                }*/
                gameController.NumberEntry((int)newValue, caseType, this);
            }
            /*else
            {
                if(currentValue != null)
                    gameController.NumberEntry((int)currentValue, caseType, this);
                currentValue = null;
                textBox.text = "";
            }*/
        }
    }

    private void caseError()
    {
        backgroundSprite.color = errorColor;
    }

    public bool hasValue()
    {
        return currentValue != null;
    }

    public void deactivateInput()
    {
        if (inputDetectionBox == null)
            inputDetectionBox = GetComponent<BoxCollider2D>();

        inputDetectionBox.enabled = false;
    }

    public void activateInput()
    {
        if (inputDetectionBox == null)
            inputDetectionBox = GetComponent<BoxCollider2D>();

        inputDetectionBox.enabled = true;
    }
}
