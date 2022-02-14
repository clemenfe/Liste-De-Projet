using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ProjetDeSession;

public class SudokuInputManager : MonoBehaviour
{
    private int[] values;
    private bool[] keys;

    //KeyCode values of alpha1 to alpha9 and keypad1 to keypad9 in Unity inputs
    private Dictionary<int, string> sudokuUsefulInputs = new Dictionary<int, string>()
    {
        [24] = "1",
        [25] = "2",
        [26] = "3",
        [27] = "4",
        [28] = "5",
        [29] = "6",
        [30] = "7",
        [31] = "8",
        [32] = "9",
        [78] = "1",
        [79] = "2",
        [80] = "3",
        [81] = "4",
        [82] = "5",
        [83] = "6",
        [84] = "7",
        [85] = "8",
        [86] = "9",
        //[1] = "" //backspace
    };

    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameController.Instance.switchSelectMultiple();
        }

        /*//debug purpuses
        for (int i = 0; i < values.Length; i++)
        {
            keys[i] = Input.GetKey((KeyCode)values[i]);
            if (keys[i])
            {
                print(i);
            }
        }*/
    }

        public string getSudokuInput()
    {
        
        foreach (KeyValuePair<int, string> kvp in sudokuUsefulInputs)
        {
            
            keys[kvp.Key] = Input.GetKeyDown((KeyCode)values[kvp.Key]);
            if (keys[kvp.Key])
            {
                return kvp.Value;
            }
        }
        return null;
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
