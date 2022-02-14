using ProjetDeSession;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CaseMemory : MonoBehaviour
{
    public AbstractInteractableCase lastInteration { get; set; }
    public List<AbstractInteractableCase> MultipleInterations { get; } = new List<AbstractInteractableCase>();
    public bool selectMultiple { get; private set; } = false;

    private void OnMouseDown()
    {
        if (!GameController.Instance.inColorMode && !selectMultiple && lastInteration != null)
        {
            lastInteration.selected = false;
            lastInteration = null;
        }
    }

    public void switchSelectMultiple()
    {
        removeLastInteraction();

        if (selectMultiple)
        {
            selectMultiple = false;
            foreach (AbstractInteractableCase a in MultipleInterations)
                a.selected = false;
            MultipleInterations.RemoveRange(0, MultipleInterations.Count);
        }
        else
        {
            selectMultiple = true;
            GameController.Instance.orderNewMultipleCommand();
        }
    }

    public void removeLastMultipleInteractions()
    {
        if (selectMultiple)
        {
            foreach (AbstractInteractableCase a in MultipleInterations)
                a.selected = false;
            MultipleInterations.RemoveRange(0, MultipleInterations.Count);
        }
    }

    public void removeLastInteraction()
    {
        if (!selectMultiple && lastInteration != null)
        {
            lastInteration.selected = false;
            lastInteration = null;
        }
    }
}
