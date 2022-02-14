using ProjetDeSession;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualCase : MonoBehaviour
{
    private CaseValue caseValue;
    private CaseCmCoin caseCmCoin;
    private CaseCmMilieu caseCmCentre;
    private InteractableCase interactableCase;
    private IEnumerable<BoxCollider2D> cmInputDetectionBox;

    //Méthode static permettant la création d'une case visuelle
    public static void createVisualCase(CaseValue caseValue, CaseCmCoin caseCmCoin, CaseCmMilieu caseCmCentre, int posX, int posY)
    {
        GameController gc = GameController.Instance;
        GameObject nouvelleCase = Instantiate(gc.getCasePrefab(), gc.transform);
        VisualCase newCase = nouvelleCase.GetComponent<VisualCase>();
        newCase.initCase(caseValue, caseCmCoin, caseCmCentre, posX, posY);
        newCase.setOffCm();
        gc.addCase(newCase);
    }

    public void initCase(CaseValue caseValue, CaseCmCoin caseCmCoin, CaseCmMilieu caseCmCentre, int X, int Y)
    {
        this.caseValue =  caseValue;
        this.caseCmCoin = caseCmCoin;
        this.caseCmCentre = caseCmCentre;

        transform.localPosition = new Vector3(X, Y);

        interactableCase = GetComponent<InteractableCase>();
        interactableCase.setCaseType(caseValue);

        foreach(cmCoinInteractableCase c in GetComponentsInChildren<cmCoinInteractableCase>())
        {
            c.setCaseType(caseCmCoin);
        }

        GetComponentInChildren<cmCentreInteractableCase>().setCaseType(caseCmCentre);

        var boxes = GetComponentsInChildren<BoxCollider2D>().ToList();
        boxes.Remove(GetComponent<BoxCollider2D>()); //On retire le collider de la zone de caseValue
        cmInputDetectionBox = boxes;
    }

    public CaseValue GetcaseValue()
    {
        return caseValue;
    }

    public CaseCmCoin GetcaseCmCoin()
    {
        return caseCmCoin;
    }

    public CaseCmMilieu GetcaseCmCentre()
    {
        return caseCmCentre;
    }

    public void setOffCm()
    {
        foreach (BoxCollider2D b in cmInputDetectionBox)
            b.enabled = false;

        interactableCase.activateInput();
    }

    public void setOnCm()
    {
        foreach (BoxCollider2D b in cmInputDetectionBox)
            b.enabled = true;

        if (interactableCase.hasValue())
            interactableCase.deactivateInput();
    }



}
