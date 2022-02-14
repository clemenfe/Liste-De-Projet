using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    SharingData data;
    private string[] strPos = new string[2];

    [SerializeField] TextMeshProUGUI _nbrTxt;
    [SerializeField] TextMeshProUGUI _Txt;

    private void Awake() {
        data = FindObjectOfType<SharingData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        strPos = data.GetPosData();

        _nbrTxt.text = strPos[0];
        _Txt.text = strPos[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
