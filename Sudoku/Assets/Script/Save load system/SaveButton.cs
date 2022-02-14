using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetDeSession; //Pour avoir GameController

public class SaveButton : MonoBehaviour {

    private SudokuBoard _sudoku;
    private GameController _gameController;

    public void Start() {
        _gameController = FindObjectOfType<GameController>(); //On dit au code qui est notre GameObject
        _sudoku = _gameController.GetSudokuBoard(); //On trouve le SudokuBoard créé par le Controller
    }

    public void Save() {

        SaveSystem.Save(_sudoku);

    }

    public void Load() {

        SudokuData data = SaveSystem.Load();

        _sudoku.LoadData(data);

    }


}
