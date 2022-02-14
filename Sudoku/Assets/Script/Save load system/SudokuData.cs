using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SudokuData {

    //Les différentes données dans les tableaux
    /*  private int[,] _position;
      private int[,] _region;*/
    private int?[,] _commentaireCoin;
    private int?[,] _commentaireMilieu;
    private int?[] _chiffreFinal;

    private float[][] _colorFinale;
    private float[,][] _colorCoin;
    private float[][] _colorCommentaire;


    //Constucteur
    public SudokuData(SudokuBoard sudoku) {

        int i = 0; //Varriable Compteur
        

        //On créé la dimension des tableaux
        _chiffreFinal = new int?[sudoku.GetBoard().Length];
        _commentaireCoin = new int?[sudoku.GetBoard().Length, sudoku.GetBoard()[0].GetcaseCmCoin().HowManyMaxCorner()];
        _commentaireMilieu = new int?[sudoku.GetBoard().Length, sudoku.GetBoard()[0].GetcaseCmCentre().MaxCommentInCenter()];

        _colorFinale = new float[sudoku.GetBoard().Length][];
        _colorCoin = new float[sudoku.GetBoard().Length, sudoku.GetBoard()[0].GetcaseCmCoin().HowManyMaxCorner()][];
        _colorCommentaire = new float[sudoku.GetBoard().Length][];

        //On met les valeurs dans nos données
        foreach (var cases in sudoku.GetBoard()) {

            
            
            _chiffreFinal[i] = cases.GetcaseValue().value; //On initialise la valeur de la case

            _colorFinale[i] = sudoku.GetCaseColor(i); //On obtient les couleurs des cases.


            //Pour chaque note en coin, on les initialises.
            for (int k = 0; k < cases.GetcaseCmCoin().HowManyMaxCorner(); k++) {
                _commentaireCoin[i, k] = cases.GetcaseCmCoin().GetCmCornerValue(k);
                _colorCoin[i, k] = sudoku.GetCaseColor(i + k, sudoku.GetCoinInteractableCases());
                                
            }


            //Pour chaque note au milieu, on les initialises.
            for (int j = 0; j < cases.GetcaseCmCentre().MaxCommentInCenter(); j++) {
                _commentaireMilieu[i, j] = cases.GetcaseCmCentre().GetCmMilieuValue(j);
                _colorCommentaire[i] = sudoku.GetCaseColor(i, sudoku.GetCentreInteractableCases());
            }
            i++; //On augmente le compteur
            

            
        }

    }


    public int? GetChiffreFinal(int index) {

        
        return _chiffreFinal[index];

    }

    public int? GetCommentaireCoin(int indexCase, int indexCommentaire) {

        return _commentaireCoin[indexCase, indexCommentaire];

    }
    
    public int? GetCommentaireMilieu(int indexCase, int indexCommentaire) {

        return _commentaireMilieu[indexCase, indexCommentaire];

    }

    public Color GetColor(int index)
    {

        Color color = new Color(_colorFinale[index][0], _colorFinale[index][1], _colorFinale[index][2], _colorFinale[index][3]);

        return color;
    }

    public Color GetColorCoin(int index, int index2) {

        Color color = new Color(_colorCoin[index, index2][0], _colorCoin[index, index2][1], _colorCoin[index, index2][2], _colorCoin[index, index2][3]);

        return color;
    }

    public Color GetColorCommentaire(int index) {

        Color color = new Color(_colorCommentaire[index][0], _colorCommentaire[index][1], _colorCommentaire[index][2], _colorCommentaire[index][3]);

        return color;
    }

}
