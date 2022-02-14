using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ProjetDeSession;
using System.Drawing;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class Tests {

    private bool checkBoard(int case1, int case2) {
        bool isValide = true;
        GameController gameController = GameController.Instance2;
        gameController.WakeMeUp();
        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(case1 * 3));
        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(case2 * 3));
        foreach (CaseValue caseSudoku in gameController.GetSudokuBoard().board.OfType<CaseValue>())
            if (caseSudoku.isRed)
                isValide = false;
        return isValide;
    }

    [Test]
    public void TestGrilleVide() {
        Assert.IsTrue(checkBoard(0, 0));
    }


    [Test]
    public void TestRedNextToEach() {
        Assert.IsTrue(!checkBoard(0, 1));
    }

    [Test]
    public void TestNotRed() {
        Assert.IsTrue(checkBoard(0, 13));
    }
    [Test]
    public void TestSameLine() {
        Assert.IsTrue(!checkBoard(0, 4));
    }

    [Test]
    public void TestSameLine2() {
        Assert.IsTrue(!checkBoard(0, 27));
    }
    [Test]
    public void TestSameSquare() {
        Assert.IsTrue(!checkBoard(0, 10));
    }

    [Test]
    public void TestMultipleRed() {
        Assert.IsTrue(!(checkBoard(0, 8) && checkBoard(34, 36) && checkBoard(40, 42) && checkBoard(43, 44) && checkBoard(52, 72) && checkBoard(80, 81)));
    }


    //Génère un SudokuBoard
    private SudokuBoard Board(int[] caseID, int[] value) {
        GameController gameController = GameController.Instance2;
        gameController.WakeMeUp();



        //Note : normalement, il faudrait faire de la validation pour s'assurer que la taille des 3 Tableaux soient pareil pour éviter des IndexOutOfRangeException
        //Mais comme la fonction n'est utilisé que pour faire des tests, on créer les tableaux de la même taille.
        for (int i = 0; i < value.Length; i++)

            gameController.NumberEntry(value[i], gameController.GetSudokuBoard().board.ElementAt(caseID[i]));

        return gameController.GetSudokuBoard();
    }

    private SudokuBoard BoardFromLoad(SudokuData data) {
        GameController gameController = GameController.Instance2; ;
        gameController.WakeMeUp();

        gameController.GetSudokuBoard().LoadDataNoColor(data);
        return gameController.GetSudokuBoard();
    }


    //Fonction utilisé pour s'assurer du bon type de case désiré pour les tests
    private void TypeCase(int[] caseID) {


        for (int i = 0; i < caseID.Length; i++) {

            //Valeur finale
            if (caseID[i] % 3 == 0)
                Debug.Log($"La case {caseID[i]} est une Value");

            //Commentaire en coin
            else if (caseID[i] % 3 == 1)
                Debug.Log($"La case {caseID[i]} est un Coin");

            //Commentaire au milieu
            else if (caseID[i] % 3 == 2)
                Debug.Log($"La case {caseID[i]} est un Milieu");
        }
    }


    //Test Sauvegarde / Chargement
    //On test une grille où les cases ne sont pas rouge et qu'il y a seulement des valeurs finales


    
    [Test]
    public void TestSave_Load_CorrectBoard_ValueOnly() {

        int[] caseID = { 0, 15, 45, 30 }; //Emplacement des cases/Type de case (voir fonction TypeCase() plus haut)
        int[] value = { 8, 1, 3, 7 }; //Valeur entré dans les cases

        //TypeCase(caseID);

        SudokuBoard sudokuBoard = Board(caseID, value); //On créer un SudokuBoard

        SudokuData _dataSave = new SudokuData(sudokuBoard); //On se créer un sauvegarde des données de notre Sudoku

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave)); //On regarde si les données dans la grille sont les mêmes que celles chargés

    }

    //On test une grille où il y a des cases rouges et qu'il y a seulement des valeurs finales
    [Test]
    public void TestSave_Load_BadBoard_ValueOnly() {

        int[] caseID = { 0, 39, 141, 93 };
        int[] value = { 1, 1, 3, 7 };
        

        SudokuBoard sudokuBoard = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }

    //On test une grille où qu'il y a seulement des commentaire au centre
    [Test]
    public void TestSave_Load_CommentInMiddle() {

        int[] caseID = { 2, 17, 47, 38 };
        int[] value = { 58, 137, 13, 7 };

        
        SudokuBoard sudokuBoard = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }

    //On test une grille où qu'il y a seulement des commentaires en coin
    [Test]
    public void TestSave_Load_CommentInCorner() {

        int[] caseID = { 1, 16, 46, 37 };
        int[] value = { 8, 1, 3, 7 };


        SudokuBoard sudokuBoard = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }

    //On test une grille où qu'il y a seulement les deux types de commentaires
    [Test]
    public void TestSave_Load_BothCommentType() {

        int[] caseID = { 1, 16, 2, 17 };
        int[] value = { 8, 1, 13, 123456789 };


        SudokuBoard sudokuBoard = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }

    //On test une grille où il y a les deux types de commentaires et des cases finales (Pas de cases Rouge)
    [Test]
    public void TestSave_Load_All_ValidGrid() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoard = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }

    //On test une grille où il y a les deux types de commentaires et des cases finales (Avec des cases Rouge)
    [Test]
    public void TestSave_Load_All_InvalidGrid() {

        int[] caseID = { 4, 16, 2, 17, 0, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 5, 7, 5 };


        SudokuBoard sudokuBoard = Board(caseID, value);
        

        SudokuData _dataSave = new SudokuData(sudokuBoard);

        Assert.AreEqual(sudokuBoard, BoardFromLoad(_dataSave));

    }


    [Test]
    public void Undo1Item() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent);

        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();

        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(5 * 3));

        gameController.Undo();

        Assert.AreEqual(sudokuBoardCurrent, BoardFromLoad(_dataSave));
    }

    [Test]
    public void Undo2Times() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent);

        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();

        gameController.NumberEntry(2, gameController.GetSudokuBoard().board.ElementAt(1));
        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(16));

        
        
        gameController.Undo();
        gameController.Undo();

        Assert.AreEqual(sudokuBoardCurrent, BoardFromLoad(_dataSave));
    }

    [Test]
    public void UndoRedo1() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);

        

        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();


        gameController.NumberEntry(2, gameController.GetSudokuBoard().board.ElementAt(1));

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent); //On sauvegarde l'état du GameObject

        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(16));


        gameController.Undo();
        gameController.Redo();
        


        SudokuBoard expectedSudoku = BoardFromLoad(_dataSave);
        

        Assert.AreEqual(expectedSudoku, sudokuBoardCurrent);
    }

    [Test]
    public void UndoRedo2() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);



        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent); //On sauvegarde l'état du GameObject

        //On applique les modifications
        gameController.NumberEntry(2, gameController.GetSudokuBoard().board.ElementAt(1));
        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(16));


        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Undo();



        SudokuBoard expectedSudoku = BoardFromLoad(_dataSave);


        Assert.AreEqual(expectedSudoku, sudokuBoardCurrent);
    }


    [Test]
    public void UndoRedo3() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);



        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();

        

        //On applique les modifications
        gameController.NumberEntry(2, gameController.GetSudokuBoard().board.ElementAt(1));
        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(7));
        gameController.NumberEntry(3, gameController.GetSudokuBoard().board.ElementAt(61));
        gameController.NumberEntry(4, gameController.GetSudokuBoard().board.ElementAt(74));
        gameController.NumberEntry(5, gameController.GetSudokuBoard().board.ElementAt(12));
        gameController.NumberEntry(6, gameController.GetSudokuBoard().board.ElementAt(3));
        gameController.NumberEntry(7, gameController.GetSudokuBoard().board.ElementAt(6));
        gameController.NumberEntry(8, gameController.GetSudokuBoard().board.ElementAt(18));
        gameController.NumberEntry(8, gameController.GetSudokuBoard().board.ElementAt(24));

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent); //On sauvegarde l'état du GameObject

        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Redo();



        SudokuBoard expectedSudoku = BoardFromLoad(_dataSave);


        Assert.AreEqual(expectedSudoku, sudokuBoardCurrent);
    }


    [Test]
    public void UndoRedo4() {

        int[] caseID = { 1, 16, 2, 17, 24, 66, 30 };
        int[] value = { 8, 1, 13, 123456789, 9, 7, 5 };


        SudokuBoard sudokuBoardCurrent = Board(caseID, value);



        GameController gameController = GameController.Instance2;

        gameController.WakeMeUp();

        

        //On applique les modifications
        gameController.NumberEntry(2, gameController.GetSudokuBoard().board.ElementAt(1));

        SudokuData _dataSave = new SudokuData(sudokuBoardCurrent); //On sauvegarde l'état du GameObject

        gameController.NumberEntry(1, gameController.GetSudokuBoard().board.ElementAt(7));
        gameController.NumberEntry(3, gameController.GetSudokuBoard().board.ElementAt(61));
        gameController.NumberEntry(4, gameController.GetSudokuBoard().board.ElementAt(74));
        gameController.NumberEntry(5, gameController.GetSudokuBoard().board.ElementAt(12));
        gameController.NumberEntry(6, gameController.GetSudokuBoard().board.ElementAt(3));
        gameController.NumberEntry(7, gameController.GetSudokuBoard().board.ElementAt(6));
        gameController.NumberEntry(8, gameController.GetSudokuBoard().board.ElementAt(18));
        gameController.NumberEntry(8, gameController.GetSudokuBoard().board.ElementAt(24));

        

        gameController.Undo();
        gameController.Undo();
        gameController.Undo();
        gameController.Redo();
        gameController.Redo();
        gameController.Undo();
        gameController.Redo();
        gameController.Undo();
        gameController.Undo();
        gameController.Undo();
        gameController.Undo();
        gameController.Undo();
        gameController.Undo();
        gameController.Undo();



        SudokuBoard expectedSudoku = BoardFromLoad(_dataSave);


        Assert.AreEqual(expectedSudoku, sudokuBoardCurrent);
    }
}
