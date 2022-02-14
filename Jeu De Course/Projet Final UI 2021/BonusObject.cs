using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusObject : MonoBehaviour {

    private Player _player;
    private IA[] _IA;
    private GameMechanics _gameMechanics;
    private int _funcSelection;
    [SerializeField] private GameObject _warningTxt;
    [SerializeField] private GameObject[] _imgObject;

    void Start() {

        _player = FindObjectOfType<Player>();
        _gameMechanics = FindObjectOfType<GameMechanics>();
        _IA = FindObjectsOfType<IA>();
    }

    public void ChooseBonus() {
        
        
        
        _funcSelection = Random.Range(0, 1001);

        if (_funcSelection < 977) {

            BasicBonus();
        }

        else if (_funcSelection >= 977 && _funcSelection < 1000) {

            MegaBonus();
        }

        else if (_funcSelection == 1000) {

            SupraBonus();
        }
        
    }

    //La fonction pour choisir les Bonus des IA.
    public void ChooseBonus(IA thisIA) {



        _funcSelection = Random.Range(0, 1001);

        if (_funcSelection < 977) {

            BasicBonus(thisIA);
        }

        else if (_funcSelection >= 977 && _funcSelection <= 1000) {

            MegaBonusIA();
        }

        //Le SupraBonus n'est pas disponnible pour les IA.
        /*else if (_funcSelection == 1000) {

            SupraBonus();
        }*/

    }

    private void BasicBonus() {
        
        _funcSelection = Random.Range(0, 4);
        
        if (_funcSelection == 0) {

            _imgObject[0].SetActive(true);
            StartCoroutine(WaitToUseObject(AddSpeed, 0));
            

        }

        else if (_funcSelection == 1) {

            _imgObject[1].SetActive(true);
            StartCoroutine(WaitToUseObject(LooseSpeed, 1));
            

        }

        else if (_funcSelection == 2) {

            _imgObject[2].SetActive(true);
            StartCoroutine(WaitToUseObject(IncreaseAcceleration, 2));
            

        }

        else if (_funcSelection == 3) {

            _imgObject[3].SetActive(true);
            StartCoroutine(WaitToUseObject(DecreaseAcceleration, 3));
            

        }
    }

    //Les BasicBonus de l'IA
    private void BasicBonus(IA thisIA) {
        
        _funcSelection = Random.Range(0, 4);
        
        if (_funcSelection == 0) {

            AddSpeed(thisIA);

        }

        else if (_funcSelection == 1) {

            LooseSpeed(thisIA);

        }

        else if (_funcSelection == 2) {

            IncreaseAcceleration(thisIA);

        }

        else if (_funcSelection == 3) {

            DecreaseAcceleration(thisIA);

        }
    }

    private void MegaBonus() {
        /*if (_player||IA finish) {
        _funcSelection = Random.Range(0, 1);
        }*/
        //else {

            _funcSelection = Random.Range(0, 3);

        //}

        if (_funcSelection == 0) {

            _imgObject[4].SetActive(true);
            StartCoroutine(WaitToUseObject(GoReverse, 4));
            

        }

        else if (_funcSelection == 1) {

            _imgObject[5].SetActive(true);
            StartCoroutine(WaitToUseObject(Add_a_Laps, 5));
            

        }

        else if (_funcSelection == 2) {

            _imgObject[6].SetActive(true);
            StartCoroutine(WaitToUseObject(Remove_A_Laps, 6));
            

        }

    }

    //MegaBonus activer par l'IA (m^me chose que joueur sauf activé lorsque ramassé.
    private void MegaBonusIA() {
        /*if (_player||IA finish) {
        _funcSelection = Random.Range(0, 1);
        }*/
        //else {

        _funcSelection = Random.Range(0, 3);

        //}

        if (_funcSelection == 0) {

            GoReverse();

        }

        else if (_funcSelection == 1) {

            Add_a_Laps();

        }

        else if (_funcSelection == 2) {

            Remove_A_Laps();

        }

    }

    private void SupraBonus() {
        _imgObject[7].SetActive(true);
        StartCoroutine(WaitToUseObject(PlayerWin, 7));
        
    }

    
    private void AddSpeed() {
        _player.AddToMaxSpeed(5f);
        
    }

    private void AddSpeed(IA thisIA) {
        int i = IndexOfThisIA(thisIA);
        _IA[i].AddToMaxSpeed(5f);
    }

    private void LooseSpeed() {
        for (int i = 0; i < _IA.Length; i++) {

            _IA[i].RemoveToMaxSpeed(2.5f);
        }
    }

    private void LooseSpeed(IA thisIA) {
        int j = IndexOfThisIA(thisIA);

        for (int i = 0; i < _IA.Length; i++) {

            if (i != j) {

                _IA[i].RemoveToMaxSpeed(2.5f);
            }
        }

        _player.RemoveToMaxSpeed(2.5f);
    }

    
    private void IncreaseAcceleration() {
        _player.AddAcceleration(0.02f);
    }

    private void IncreaseAcceleration(IA thisIA) {
        int i = IndexOfThisIA(thisIA);
        _IA[i].AddAcceleration(0.02f);
    }

    private void DecreaseAcceleration() {
        for (int i = 0; i < _IA.Length; i++) {
            _IA[i].RemoveAcceleration(0.02f);
        }
    }

    private void DecreaseAcceleration(IA thisIA) {
        int j = IndexOfThisIA(thisIA);

        for (int i = 0; i < _IA.Length; i++) {

            if (i != j) {

                _IA[i].RemoveAcceleration(0.02f);
            }
        }

        _player.RemoveAcceleration(0.02f);
    }

    private void Add_a_Laps() {
        _gameMechanics.AddLaps();
    }

    private void Remove_A_Laps() {
        _gameMechanics.RemoveLaps();
    }
    
    private void GoReverse() {
        _gameMechanics.switchDirection();
        StartCoroutine(WarningFlash());
    }

    private void PlayerWin() {
        _player.SetCurrentPosition(1);
        _player.SwitchScene();
    }

    private IEnumerator WarningFlash() {

        for (int i = 0; i < 3; i++) {

            _warningTxt.SetActive(true);

            yield return new WaitForSeconds(0.3f);

            _warningTxt.SetActive(false);

            yield return new WaitForSeconds(0.3f);
        }

        _warningTxt.SetActive(true);
        yield return new WaitForSeconds(3);

        for (int i = 0; i < 4; i++) {

            _warningTxt.SetActive(true);

            yield return new WaitForSeconds(0.3f);

            _warningTxt.SetActive(false);

            yield return new WaitForSeconds(0.3f);
        }
    }

    private int IndexOfThisIA(IA thisIA) {

        for (int i = 0; i < _IA.Length; i++) {

            if (_IA[i] == thisIA) {
                return i;
            }

            
        }

        //Ne devrait pas arriver... 
        Debug.Log("Une Erreur est survenue!");
        return 0;

    }

    IEnumerator WaitToUseObject(function myFunction, int i) {
        
        yield return new WaitUntil(() => _player.UseObjectValue() == true);

        _imgObject[i].SetActive(false);
        myFunction();
    }

    private delegate void function();
}

