using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CmsInCase : MonoBehaviour
{
    private cmCoinInteractableCase[] cmsInCoin;

    private void Awake()
    {
        cmsInCoin = GetComponentsInChildren<cmCoinInteractableCase>();
    }

    public void removeTextValueFromCoin(int cm)
    {
        foreach(cmCoinInteractableCase c in cmsInCoin)
        {
            if (c.currentValue == cm)
            {
                c.removeTextValue();
            }
        }
    }

    public bool anyCoinValueIs(int cm)
    {
        return cmsInCoin.Select(cmInCoin => cmInCoin.currentValue == cm).Where(b => b == true).Any();
    }
}
