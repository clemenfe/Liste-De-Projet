using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharingData : MonoBehaviour
{

    
    private string _txtPosition;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetPositionString(string posStr) {
        _txtPosition = posStr;
    }

    public string[] GetPosData() {

        string[] strPos = new string[2];
        strPos[0] = _txtPosition;

        if (strPos[0] == "1") {
            strPos[1] = "st";
        }

        else if (strPos[0] == "2") {
            strPos[1] = "nd";
        }

        else if (strPos[0] == "3") {
            strPos[1] = "rd";
        }

        else {
            strPos[1] = "th";
        }

        return strPos;
    }
}
