using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMechanics : MonoBehaviour {


    private Checkpoint[] _checkpoints;
    [SerializeField] private Checkpoint[] _checkpointsForward;
    [SerializeField] private Checkpoint[] _checkpointsReverse;
    private Player _player;
    private bool _reverseDirection = false;
    private int _laps = 3;
    private int _minPosition = 1;
    [SerializeField] TextMeshProUGUI _txt_Laps;


    private IA[] _ia;

    // Start is called before the first frame update
    void Start()
    {
        _checkpoints = FindObjectsOfType<Checkpoint>();
        _player = FindObjectOfType<Player>();
        _checkpointsForward[_checkpointsForward.Length - 1].SetBeforeFinish(true);
        _ia = FindObjectsOfType<IA>();

        //On initialise des tableaux de checkpoint selon l'orientation.



        /*for (int i = 0, k = 0, l = 0; i < _checkpoints.Length; i++) {

            if(_checkpoints[i].IsForward()) {
                
                _checkpointsForward[k] = _checkpoints[i];
                k++;
            }

            if(_checkpoints[i].IsReverse()) {
                _checkpointsReverse[l] = _checkpoints[i];
                l++;
            }

        }*/




    }

    // Update is called once per frame
    void Update()
    {
        if (_txt_Laps.text != _laps.ToString()) {
            _txt_Laps.text = _laps.ToString();
        }
        
    }

    public bool GetRaceDirection() {
        return _reverseDirection;
    }

    public void SwitchRaceDirection() {
        _reverseDirection = !_reverseDirection;
    }

    public void switchDirection() {

        if (!_reverseDirection) {
            _checkpointsForward[_checkpointsForward.Length - 1].SetBeforeFinish(true);
            _checkpointsReverse[_checkpointsForward.Length - 1].SetBeforeFinish(false);
            _reverseDirection = true;
        }

        else if (_reverseDirection) {
            _checkpointsReverse[_checkpointsForward.Length - 1].SetBeforeFinish(true);
            _checkpointsForward[_checkpointsForward.Length - 1].SetBeforeFinish(false);
            _reverseDirection = false;
        }
    }

    public Vector3 lastCheckpoint() {

        if (!_reverseDirection) {
           return _checkpointsForward[_checkpointsForward.Length - 1].transform.position;
            
        }

        else  {
            return _checkpointsReverse[_checkpointsForward.Length - 1].transform.position;
            
        }
    }

    public void AddLaps() {
        _laps++;
    }

    public void RemoveLaps() {
        _laps--;
    }

    public int GetLaps() {
        return _laps;
    }

    public Checkpoint[] _CheckPointInScene(bool reverseMap) {

        if (!reverseMap) {
            return _checkpointsForward;
        }

        else {
            return _checkpointsReverse;
        }
    }

    //Augmente La position minimal que le joueur peut avoir en finissant la course. Augmente lorsqu'un IA franchit la ligne d'arrivée.
    public void _minPositionAugmente() {
        _minPosition++;
    }

    public void DeterminePlayerPosition() {

        
        

    }
}
