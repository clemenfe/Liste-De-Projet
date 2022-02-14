using Assets.Script.Visual_scripts;
using ProjetDeSession;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AbstractInteractableCase : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] protected TextMeshProUGUI textBox;

    public bool selected { get; set; }

    protected GameController gameController;
    protected VisualCase caseSudoku;
    protected ICase caseType;
    public Color currentColor { get; private set; }
    protected SpriteRenderer backgroundSprite;
    protected CaseMemory caseMemory;
    protected SudokuInputManager sudokuInputs;

    protected virtual void Start()
    {
        selected = false;

        gameController = GameController.Instance;
        backgroundSprite = GetComponent<SpriteRenderer>();
        currentColor = backgroundSprite.color;
        caseMemory = FindObjectOfType<CaseMemory>();
        sudokuInputs = FindObjectOfType<SudokuInputManager>();
    }

    protected abstract void getInputForSudoku();

    //On affiche l'input (Utilise après un chargement de fichier)
    public virtual void SetValue(int? value)
    {
        textBox.text = value.ToString();
    }

    public void SetColor(Color newColor)
    {
        currentColor = newColor;
    }

    private void OnMouseDown()
    {
        /*if (gameController.inColorMode)
        {
            gameController.ColorEntry(PinceauCouleur.nouvelleCouleur, this);
        }
        else*/ if (!selected && !caseMemory.selectMultiple)
        {
            selected = true;
            if (caseMemory.lastInteration != null)
                caseMemory.lastInteration.selected = false;
            caseMemory.lastInteration = this;

        }
        else
        {
            if (caseMemory.MultipleInterations.Contains(this))
            {
                caseMemory.MultipleInterations.Remove(this);
                selected = false;
            }
            else
            {
                caseMemory.MultipleInterations.Add(this);
                selected = true;
            }
        }
    }

    private void Update()
    {
        if (selected)
        {
            caseSelected();
        }
        else
        {
            caseUnselected();
        }
    }

    protected void caseSelected()
    {
        if (textBox != null)
        {
            backgroundSprite.color = selectedColor;
            getInputForSudoku();
        }
    }

    protected void caseUnselected()
    {
        backgroundSprite.color = currentColor;
    }

    public virtual void setCaseType(ICase caseT)
    {
        caseType = caseT;
    }
}
