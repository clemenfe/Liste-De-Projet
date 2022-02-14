using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ProjetDeSession; 


public class SudokuBoard : MonoBehaviour
{

    private VisualCase[] _cases;
    private InteractableCase[] _interactableCase;
    private cmCoinInteractableCase[] _cmCoin;
    private cmCentreInteractableCase[] _cmCentre;

    public List<ICase> board { get; }

    public SudokuBoard() {
            
        board = initBoard();
        _cases = FindObjectsOfType<VisualCase>();
        _interactableCase = FindObjectsOfType<InteractableCase>();
        _cmCoin = FindObjectsOfType<cmCoinInteractableCase>();
        _cmCentre = FindObjectsOfType<cmCentreInteractableCase>();
    }


    private List<ICase> initBoard() //Utile seulement à la méthode du constructeur ----- Permet d'alléger le constructeur
    {

        List<ICase> initBoard = new List<ICase>();
        Point position;
        Point region;
        var gameController = GameController.Instance;

        for (int x = 1; x < 10; x++)
        {
            for (int y = 1; y < 10; y++)
            {

                position = new Point(x, y);
                region = new Point(ceilingAThird(x), ceilingAThird(y)); //On obtient la 'région' (matrice 3x3) d'une case en divisant la matrice 9x9 par 3 sur les colonnes et lignes
                var caseValue = new CaseValue(position, region);
                var caseCmCoin = new CaseCmCoin(position, region);
                var caseCmCentre = new CaseCmMilieu(position, region);

                VisualCase.createVisualCase(caseValue, caseCmCoin, caseCmCentre, position.X, position.Y);

                initBoard.Add(caseValue);
                initBoard.Add(caseCmCoin);
                initBoard.Add(caseCmCentre);
            }
        }


        return initBoard;
    }



    private int ceilingAThird(int nb) //Utile seulement à la méthode initialiseBoard ------- Malheureusement, c# ne semble pas être capable de retourner Ceiling(nb/3) comme un int, d'où cette fonction permettant de retourner un int.
    {
        var ceilingResult = (nb / 3) + (nb % 3 == 0 ? 0 : 1);
        return ceilingResult;
    }










    public void verifyDuplicate()
    {
        foreach (CaseValue caseSudoku in board.OfType<CaseValue>())
        {
            caseSudoku.isRed = false;
        }


        for (int i = 1; i <= 9; i++)
        {
            verifyLine(i);
            verifyCol(i);
        }
        for (int j = 1; j <= 3; j++)
            for (int k = 1; k <= 3; k++)
                verifyRegion(j, k);
    }






    private void verifyLine(int line)
    {

        List<CaseValue> tempCValue = new List<CaseValue>();
        foreach (CaseValue c in board.OfType<CaseValue>()) { tempCValue.Add(c); } //Filtre les caseValue de la liste de ICase ----- Possiblement à optimiser

        var temp = tempCValue.Where(c => c.position.Y == line && c.value.HasValue).GroupBy(c => c.value).Where(g => g.Count() >= 2).ToList(); //Deuxième filtre pour les doublons

        if (temp.Any()) //Cible les cases qui sont dans une certaine ligne ('line'), les regroupes ensuite par valeur et regarde s'il y a un groupe qui contient plus d'un élément (i.e. il y a 2 cases ou plus qui ont la même valeur)
        {
            //Debug.Log("Duplicate in line");
                
            foreach (var g in temp)
            {
                foreach (var c in g)
                {
                    //Debug.Log("Putting " + c.position.X + " " + c.position.Y + " in red");
                    // Debug.Log("Putting " + c.region.X + " " + c.region.Y + " in red");
                    c.isRed = true;
                }
            }

        }

    }

    private void verifyCol(int col)
    {

        List<CaseValue> tempCValue = new List<CaseValue>();
        foreach (CaseValue c in board.OfType<CaseValue>()) { tempCValue.Add(c); } //Filtre les caseValue de la liste de ICase ----- Possiblement à optimiser

        var temp = tempCValue.Where(c => c.position.X == col && c.value.HasValue).GroupBy(c => c.value).Where(g => g.Count() >= 2).ToList(); //Deuxième filtre pour les doublons

        if (temp.Any()) //Cible les cases qui sont dans une certaine colonne ('col'), les regroupes ensuite par valeur et regarde s'il y a un groupe qui contient plus d'un élément (i.e. il y a 2 cases ou plus qui ont la même valeur)
        {
            //Debug.Log("Duplicate in col");
            foreach (var g in temp)
            {
                foreach (var c in g)
                {
                    //Debug.Log("Putting " + c.position.X + " " + c.position.Y + " in red");
                    //Debug.Log("Putting " + c.region.X + " " + c.region.Y + " in red");
                    c.isRed = true;
                }
            }

        }

    }

    private void verifyRegion(int regX, int regY)
    {
        List<CaseValue> tempCValue = new List<CaseValue>();
        foreach (CaseValue c in board.OfType<CaseValue>()) { tempCValue.Add(c); } //Filtre les caseValue de la liste de ICase ----- Possiblement à optimiser


        var temp = tempCValue.Where(c => c.region.X == regX && c.region.Y == regY && c.value.HasValue).GroupBy(c => c.value).Where(g => g.Count() >= 2).ToList(); //Deuxième filtre pour les doublons

        if (temp.Any()) //Cible les cases qui sont dans une certaine région ('regX + regY'), les regroupes ensuite par valeur et regarde s'il y a un groupe qui contient plus d'un élément (i.e. il y a 2 cases ou plus qui ont la même valeur)
        {
            //Debug.Log("Duplicate in region");
            foreach (var g in temp)
            {
                foreach (var c in g)
                {
                    //Debug.Log("Putting " + c.region.X + " " + c.region.Y +  " in red");
                    c.isRed = true;
                }
            }

        }

    }

    //On obtient la liste des Case (Utile pour la sauvegarde)
    public VisualCase[] GetBoard() {

        return _cases;

    }

    public void ResetData() {
        
        for (int i = 0, k = 0; k < _cmCoin.Length; i++, k++) {
            

            if (i < _cases.Length) {
                
                //On supprime les données au niveau logique
                _cases.ElementAt(i).GetcaseValue().Reset();
                _cases.ElementAt(i).GetcaseCmCentre().Reset();
                _cases.ElementAt(i).GetcaseCmCoin().Reset();

                //On supprime les données au niveau visuel
                _interactableCase.ElementAt(i).SetValue(null);
                _interactableCase.ElementAt(i).SetColor(UnityEngine.Color.white);
                _cmCentre.ElementAt(i).SetColor(UnityEngine.Color.clear);
                _cmCentre.ElementAt(i).ClearInput();
                
            }

            _cmCoin.ElementAt(k).SetColor(UnityEngine.Color.clear);
            _cmCoin.ElementAt(k).SetValue(null);
            
        }

        verifyDuplicate(); //Va permettre d'enlever les cases rouges s'il y a lieu

    }


    //On charge les données
    public void LoadData(SudokuData data) {

        ResetData(); //Empêche un phénomène qui fait que l'affichage disparaît lorsqu'on clic 2 fois de suites sur Load
        int indexCase = 0;
        int indexCaseCoin = 0;
        foreach (var cases in _cases) {

                        
            try {
                
                cases.GetcaseValue().changeValue((int)data.GetChiffreFinal(indexCase)); //On met les nouvelles valeurs
                _interactableCase.ElementAt(indexCase).SetValue(cases.GetcaseValue().value);
                
            }
            catch {

                //Valeur null, on reset
                cases.GetcaseValue().Reset();
                _interactableCase.ElementAt(indexCase).SetValue(null);

            }

            _interactableCase.ElementAt(indexCase).SetColor(data.GetColor(indexCase));
            //Pour chaque note en coin, on les initialises.
            for (int i = 0, j = cases.GetcaseCmCoin().HowManyMaxCorner() - 1; i < cases.GetcaseCmCoin().HowManyMaxCorner(); i++, j--) {

                //Pour l'orientation visuel. (Sinon, tous les chiffres en sens horaire.
                if (j == 0)
                    j = 1;
                else if (j == 1)
                    j = 0;

                try {
                    //On ajoute les commentaires en coin
                    //cases.GetcaseCmCoin().changeValue((int)data.GetCommentaireCoin(indexCase, i));
                    _cmCoin.ElementAt(indexCaseCoin + j).SetValue(data.GetCommentaireCoin(indexCase, i));

                }

                catch {
                    //Valeur null, on reset
                    cases.GetcaseCmCoin().Reset();
                    _cmCoin.ElementAt(indexCaseCoin + j).SetValue(null);
                }

                //_cmCoin.ElementAt(indexCaseCoin + j).SetColor(data.GetColorCoin(indexCase, j));
                //Pour permettre à J de se désincrémenter comme il faut
                if (j == 0)
                    j = 1;
                else if (j == 1)
                    j = 0;
            }

            for (int i = 0; i < cases.GetcaseCmCentre().MaxCommentInCenter(); i++) {

                try {

                    //On ajoute les commentaires au centre
                    cases.GetcaseCmCentre().changeValue((int)data.GetCommentaireMilieu(indexCase, i));
                    _cmCentre.ElementAt(indexCase).SetValue((int)data.GetCommentaireMilieu(indexCase, i));
                    

                }

                catch {

                    //Valeur null, on reset
                    cases.GetcaseCmCentre().Reset();
                    //_cmCentre.ElementAt(indexCase).ClearInput();
                }

                _cmCentre.ElementAt(indexCase).SetColor(data.GetColorCommentaire(indexCase));

            }

            indexCase++;
            indexCaseCoin += _cases.ElementAt(0).GetcaseCmCoin().HowManyMaxCorner(); //Car il y a 4 coins
        }

        verifyDuplicate(); //On vérifie s'il y a des valeurs illégales

    }

    //On Load sans les couleurs pour les tests car on manque de temps
    public void LoadDataNoColor(SudokuData data) {

        ResetData(); //Empêche un phénomène qui fait que l'affichage disparaît lorsqu'on clic 2 fois de suites sur Load
        int indexCase = 0;
        int indexCaseCoin = 0;
        foreach (var cases in _cases) {


            try {

                cases.GetcaseValue().changeValue((int)data.GetChiffreFinal(indexCase)); //On met les nouvelles valeurs
                _interactableCase.ElementAt(indexCase).SetValue(cases.GetcaseValue().value);

            }
            catch {

                //Valeur null, on reset
                cases.GetcaseValue().Reset();
                _interactableCase.ElementAt(indexCase).SetValue(null);

            }

            //_interactableCase.ElementAt(indexCase).SetColor(data.GetColor(indexCase));
            //Pour chaque note en coin, on les initialises.
            for (int i = 0, j = cases.GetcaseCmCoin().HowManyMaxCorner() - 1; i < cases.GetcaseCmCoin().HowManyMaxCorner(); i++, j--) {

                //Pour l'orientation visuel. (Sinon, tous les chiffres en sens horaire.
                if (j == 0)
                    j = 1;
                else if (j == 1)
                    j = 0;

                try {
                    //On ajoute les commentaires en coin
                    //cases.GetcaseCmCoin().changeValue((int)data.GetCommentaireCoin(indexCase, i));
                    _cmCoin.ElementAt(indexCaseCoin + j).SetValue(data.GetCommentaireCoin(indexCase, i));

                }

                catch {
                    //Valeur null, on reset
                    cases.GetcaseCmCoin().Reset();
                    _cmCoin.ElementAt(indexCaseCoin + j).SetValue(null);
                }

                //_cmCoin.ElementAt(indexCaseCoin + j).SetColor(data.GetColorCoin(indexCase, j));
                //Pour permettre à J de se désincrémenter comme il faut
                if (j == 0)
                    j = 1;
                else if (j == 1)
                    j = 0;
            }

            for (int i = 0; i < cases.GetcaseCmCentre().MaxCommentInCenter(); i++) {

                try {

                    //On ajoute les commentaires au centre
                    cases.GetcaseCmCentre().changeValue((int)data.GetCommentaireMilieu(indexCase, i));
                    _cmCentre.ElementAt(indexCase).SetValue((int)data.GetCommentaireMilieu(indexCase, i));


                }

                catch {

                    //Valeur null, on reset
                    cases.GetcaseCmCentre().Reset();
                    //_cmCentre.ElementAt(indexCase).ClearInput();
                }

                //_cmCentre.ElementAt(indexCase).SetColor(data.GetColorCommentaire(indexCase));

            }

            indexCase++;
            indexCaseCoin += _cases.ElementAt(0).GetcaseCmCoin().HowManyMaxCorner(); //Car il y a 4 coins
        }

        verifyDuplicate(); //On vérifie s'il y a des valeurs illégales

    }

    public float[] GetCaseColor(int index)
    {

        float[] colorFloat = new float[4];

        for (int i = 0; i < colorFloat.Length; i++)
            colorFloat[i] = _interactableCase.ElementAt(index).currentColor[i];

        return colorFloat;
    }

    public float[] GetCaseColor(int index, AbstractInteractableCase[] typeCase) {

        float[] colorFloat = new float[4];

        for (int i = 0; i < colorFloat.Length; i++)
            colorFloat[i] = typeCase.ElementAt(index).currentColor[i];

        return colorFloat;
    }


    /*public InteractableCase[] GetInteractableCases() {

        return _interactableCase;

    }*/

    public cmCentreInteractableCase[] GetCentreInteractableCases() {

        return _cmCentre;
    }

    public cmCoinInteractableCase[] GetCoinInteractableCases() {

        return _cmCoin;
    }

    public override bool Equals(object obj) {

        if (obj is SudokuBoard) {

            SudokuBoard objSud = (SudokuBoard)obj;

            for (int i = 0; i < _interactableCase.Count(); i++) {
                if (this._interactableCase.GetValue(i) != objSud._interactableCase.GetValue(i))
                    return false;
            }

            for (int i = 0; i < _cmCoin.Count(); i++) {
                if (this._cmCoin.GetValue(i) != objSud._cmCoin.GetValue(i))
                    return false;
            }

            for (int i = 0; i < _cmCentre.Count(); i++) {
                if (this._cmCentre.GetValue(i) != objSud._cmCentre.GetValue(i))
                    return false;
            }

            return true; //Ici on est certain que les données sont pareil.


        }

        return false;
        
               
               
    }

    public override int GetHashCode() {
        int hashCode = 654024778;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + hideFlags.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Transform>.Default.GetHashCode(transform);
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(gameObject);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(tag);
        //hashCode = hashCode * -1521134295 + EqualityComparer<Component>.Default.GetHashCode(rigidbody);
        return hashCode;
    }
}

