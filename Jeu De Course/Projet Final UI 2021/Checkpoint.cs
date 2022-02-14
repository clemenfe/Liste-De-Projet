using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour { 


    [SerializeField] private int _id;
    [SerializeField] private bool _activeOnReverse = true; 
    [SerializeField] private bool _activeOnForward = true;
    private bool _lastBeforeFinish = false;
    private GameMechanics _gameMechanics;

    


// Start is called before the first frame update
void Start() { 
    
        _gameMechanics = FindObjectOfType<GameMechanics>();
    }

    // Update is called once per frame
    void Update() { 
    
        
    }

    public Checkpoint() {
        _id = 0;
        _activeOnForward = true;
        _activeOnReverse = true;
    }

    public int Next(int max) {

        if (!_gameMechanics.GetRaceDirection()) {

            //Si l'Id suivant ne dépasse pas le maximum on le retourne.
            if (_id + 1 <= max) {

                return _id + 1;
            }

            //Le prochaine Id sera au départ.
            else {

                return 0;
            }
        }

        else {
            //Si l'Id suivant ne dépasse pas le maximum on le retourne.
            if (_id - 1 >= 0) {

                return _id - 1;
            }

            //Le prochaine Id sera au départ.
            else {

                return max;
            }
        }

    }

    public void SetId(int _id) {

        this._id = _id; 
    }

    public int GetID() {
        return _id;
    }

    public bool IsForward() {
        return _activeOnForward;
    }
    
    public bool IsReverse() {
        return _activeOnReverse;
    }

    public Checkpoint Myself() {
        return this;
    }

    public void SetBeforeFinish(bool value) {

        _lastBeforeFinish = value;

    }

    public bool isLast() {
        return _lastBeforeFinish;
    }
    
}
