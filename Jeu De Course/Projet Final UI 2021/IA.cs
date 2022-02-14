using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;




public class IA : Agent
{


    [SerializeField] private float _vitesse = 5f;
    [SerializeField] private float _vitesseMax = 15f;
    [SerializeField] private float _vitesseReculonsMax = -5f;
    [SerializeField] private float _acceleration = 0.05f;
    [SerializeField] private float _vitesseRotation = 20f;
    [SerializeField] private float _brakeSpeed = 8f;
    //private bool _accBool, _brakeBool, _reculonsBool;
    Vector3 tourner = new Vector3(0, 0, 0);
    [SerializeField] private GameObject _brakeLight, _reverseLight;
    [SerializeField] private GameObject _frontlight;
    private AudioSource[] _carSound;
    private GameMechanics _gameMechanics;
    private Rigidbody _rigidbody;
    private BonusObject _bonus;
    int compteur = 0;
    int compteurReverse;
    private int _currentLap = 1;
    private Vector3 _checkpoint = new Vector3(0, 0, 0);
    private float _checkPointOrientation;
    private bool _okayToFinish = false;
    
    private Transform target;
    private float lastDistanceTarget, currentDistanceTarget;
    private float _mouvement;
    //private float _boolFloatAvancer, _boolFloatReculer;

   // private RayPerceptionSensorComponent3D _ray;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _carSound = GetComponentsInChildren<AudioSource>();
        _gameMechanics = FindObjectOfType<GameMechanics>();
        _bonus = FindObjectOfType<BonusObject>();
        target = new GameObject().transform; //a essayer dans Awake.

      //  _ray = GetComponent<RayPerceptionSensorComponent3D>();

        compteurReverse = _gameMechanics._CheckPointInScene(true).Length - 1;

        _checkpoint = this.transform.position;
        _checkPointOrientation = 90;


    }

    private void Update() {
        currentDistanceTarget =  Vector3.Distance(this.transform.position, target.position);
        _Mouvement();
    }


    /// /////////////////////////////////////////////////////////
    public override void Heuristic(float[] actionsOut) {
        //actionsOut = new float[2];
        actionsOut[0] = 1;
        actionsOut[1] = 0;
        //actionsOut[2] = 0;

    }

    public override void OnEpisodeBegin() {
        
        if (!_gameMechanics.GetRaceDirection()) {

           target = _gameMechanics._CheckPointInScene(false)[compteur].transform;
            
            
        }

        else if (_gameMechanics.GetRaceDirection()) {

            target = _gameMechanics._CheckPointInScene(true)[compteurReverse].transform;

        }

        lastDistanceTarget = Vector3.Distance(this.transform.position, target.position);
        
        

    }

    
    
    public override void CollectObservations(VectorSensor sensor) {
        // Target and Agent positions
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent 
        sensor.AddObservation(_vitesse);
        sensor.AddObservation(_mouvement);
        
        
        /*sensor.AddObservation(_boolFloatAvancer);
        sensor.AddObservation(_boolFloatReculer);
        sensor.AddObservation(_accBool);
        sensor.AddObservation(_brakeBool);*/

        sensor.AddObservation(tourner.y);

        
        
    }

    public override void OnActionReceived(float[] boolAction) {


        _mouvement = boolAction[0];
        tourner.y = boolAction[1];

       if (currentDistanceTarget < lastDistanceTarget) {
            lastDistanceTarget = currentDistanceTarget;
            AddReward(0.001f);
        }

        else {
            AddReward(-0.001f);
        }
       
        if (_mouvement > 0f) {
            AddReward(_mouvement * 0.01f);
        }

        //Il est puni lorsqu'il va à reculons
        else if (_mouvement < 0f) {
            AddReward(_mouvement * 0.01f);
        }

        if (_vitesse == -5) {
            SetReward(-1);
        }
        /*
        else {
            AddReward(-0.1f);
        }*/



        /*_ChooseBool(boolAction[0], ref _accBool);
        _ChooseBool(boolAction[1], ref _brakeBool);*/


        /*_boolFloatAvancer = boolAction[0];
        _boolFloatReculer = boolAction[1];
        tourner.y = boolAction[2];


        _ChooseBool(_boolFloatAvancer, ref _accBool);
        _ChooseBool(_boolFloatReculer, ref _brakeBool);*/
    }
    /// /////////////////////////////////////////////////////////
    private void _ChooseBool(float action, ref bool myBool) {
        if (action <= 0f) {
            myBool = false;
        }

        else {
            myBool = true;
        }

    }



    private void _CompteurVerif() {
        compteur++;
        compteurReverse--;

        if (compteur == _gameMechanics._CheckPointInScene(false).Length) {
            compteur = 0;
        }

        if (compteurReverse < 0) {
            compteurReverse = _gameMechanics._CheckPointInScene(true).Length -1;
        }
    }
    private void _Mouvement() {
        Vector3 direction = new Vector3(0, 0f, 1).normalized;
        this.transform.Translate(direction * _vitesse * Time.deltaTime);

        //Il doit être en mouvement pour tourner.
        //if (_vitesse != 0) {
        if (_vitesse > 0) {
                if (_mouvement < 0) {
                Vector3 tournerReculons = new Vector3(0, -tourner.y, 0); //On inverse les côtés lors du reculons
                this.transform.Rotate(tournerReculons * _vitesseRotation * Time.deltaTime);

            }
            else {
                this.transform.Rotate(tourner * _vitesseRotation * Time.deltaTime);

            }
        }
        //Pour permettre l'appuis des deux bouttons en même temps
        if (_mouvement != 0) {
            if (_mouvement > 0 ) {

                _reverseLight.SetActive(false);
                _brakeLight.SetActive(false); //On éteint les lumières arrière
                _frontlight.SetActive(true); //On allume les lumières de devant.

                
                //Si le son ne joue pas, on le fait jouer.
                if (!_carSound[6].isPlaying) {
                    _carSound[6].Play(); //On joue le 5e son de la liste
                    _carSound[5].Stop();
                }
                _Acceleration(_vitesseMax);
            }


            else if (_mouvement < 0) {

                _brakeLight.SetActive(true); //On allume les lumières de frein

                if (_vitesse > 0) {
                    _Deceleration(_brakeSpeed);
                }

                else if (_vitesse == 0) {

                    _vitesse = -0.001f;
                }
                else if (_vitesse <= 0) {
                    
                    _reverseLight.SetActive(true); //On allume les lumières de reculons
                    _Reculons();
                }
            }

        }
        else {

            _reverseLight.SetActive(false);
            _brakeLight.SetActive(false); //On éteint les lumières arrière
            _frontlight.SetActive(true); //On allume les lumières de devant.

            //On arrête certains sons
            _carSound[6].Stop();
            _carSound[5].Stop();


            if (!(_mouvement < 0)) {
                _Deceleration(1f);
            }

            else {
                //On accélère jusqu'à atteindre l'arrêt.
                _Acceleration(0);
            }
            

        }
    }
    /*private void _Mouvement() {
        Vector3 direction = new Vector3(0, 0f, 1).normalized;
        this.transform.Translate(direction * _vitesse * Time.deltaTime);

        //Il doit être en mouvement pour tourner.
        if (_vitesse != 0) {

            if (_reculonsBool) {
                Vector3 tournerReculons = new Vector3(0, -tourner.y, 0); //On inverse les côtés lors du reculons
                this.transform.Rotate(tournerReculons * _vitesseRotation * Time.deltaTime);
                
            }
            else {
                this.transform.Rotate(tourner * _vitesseRotation * Time.deltaTime);
                
            }
        }
        //Pour permettre l'appuis des deux bouttons en même temps
        if (_accBool || _brakeBool) {
            if (_accBool) {

                _reverseLight.SetActive(false);
                _brakeLight.SetActive(false); //On éteint les lumières arrière
                _frontlight.SetActive(true); //On allume les lumières de devant.

                _reculonsBool = false;
                //Si le son ne joue pas, on le fait jouer.
                if (!_carSound[6].isPlaying) {
                    _carSound[6].Play(); //On joue le 5e son de la liste
                    _carSound[5].Stop();
                }
                _Acceleration(_vitesseMax);
            }


            if (_brakeBool) {

                _brakeLight.SetActive(true); //On allume les lumières de frein

                if (_vitesse > 0) {
                    _Deceleration(_brakeSpeed);
                }

                else if (_vitesse == 0) {

                    _vitesse = -0.001f;
                }
                else if (_vitesse <= 0) {
                    _reculonsBool = true;
                    _reverseLight.SetActive(true); //On allume les lumières de reculons
                    _Reculons();
                }
            }

        }
        else {

            _reverseLight.SetActive(false);
            _brakeLight.SetActive(false); //On éteint les lumières arrière
            _frontlight.SetActive(true); //On allume les lumières de devant.

            //On arrête certains sons
            _carSound[6].Stop();
            _carSound[5].Stop();


            if (!_reculonsBool) {
                _Deceleration(1f);
            }

            else {
                //On accélère jusqu'à atteindre l'arrêt.
                _Acceleration(0);
            }


        }
    }*/

    private void _Acceleration(float maxSpeed) {
        

        if (_vitesse < maxSpeed) {
            _vitesse += _acceleration;
            //_vitesse *= _acceleration;
        }

        else if (_vitesse > maxSpeed) {
            _vitesse = maxSpeed; //Pour empêcher que le joueur trouve un moyen d'être plus rapide que cette vitesse.
        }

    }

    private void _Deceleration(float decelerationSpeed) {


        _vitesse -= (_acceleration * decelerationSpeed);
        //_vitesse /= (_acceleration * decelerationSpeed);

        //Pour empêcher un reculons infini.
        if (_vitesse < 0.001f) {
            _vitesse = 0;
        }
    }

    private void _Reculons() {

        //Si le son ne joue pas, on le fait jouer.
        if (!_carSound[5].isPlaying) {
            _carSound[5].Play(); //On joue le 5e son de la liste
            _carSound[6].Stop();
        }

        _frontlight.SetActive(false); //On éteint les lumières de devant, car elles illuminent sous la voiture lors du reculons


        if (_vitesse > _vitesseReculonsMax) {
            _vitesse -= _acceleration;
            //_vitesse *= _acceleration;
        }

        else if (_vitesse < _vitesseReculonsMax) {
            _vitesse = _vitesseReculonsMax; //Pour empêcher que le joueur trouve un moyen d'être plus rapide que cette vitesse.
        }

    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Jump") {

            _rigidbody.freezeRotation = false;
        }

        else if (other.gameObject.tag == "Terrain") {

            _vitesseMax -= 10;
            SetReward(-1f);
            EndEpisode();

        }

        else if (other.gameObject.tag == "Respawn") {

            //AddReward(-0.1f);
            SetReward(-1f);
            EndEpisode();
            StartCoroutine(_Respawn(1.75f));

        }

        else if (other.gameObject.tag == "Checkpoint" || other.gameObject.tag == "Finish") {
            _checkpoint = other.transform.position;
            _checkPointOrientation = other.transform.eulerAngles.y;

            if (_checkpoint == target.transform.position) {
                _CompteurVerif();
                SetReward(1.0f);
                EndEpisode();
            }

            //Regarder le sens de la course, si sens inverse : _checkpointOrientation = -_checkpointOrientation;
            if (_gameMechanics.GetRaceDirection()) {
                _checkPointOrientation = -_checkPointOrientation;
            }

            if (_gameMechanics.lastCheckpoint() == _checkpoint) {
                _okayToFinish = true;

            }

            if (other.gameObject.tag == "Finish" && _okayToFinish) {
                _okayToFinish = false;
                _currentLap++;


                if (_gameMechanics.GetLaps() < _currentLap) {

                    //On augmente la position minimal (empêche le joueur d'être en première position si quelqu'un a fini avant lui.
                    _gameMechanics._minPositionAugmente();
                    
                    //Va détruire cet IA lorsqu'il aura terminé la course.
                    //Destroy(this); 
                }

                


            }
        }

        else if (other.gameObject.tag == "CheckpointIA") {

            AddReward(0.1f);
            other.gameObject.SetActive(false);
            EndEpisode();

        }

        else if (other.gameObject.tag == "Bonus") {

            _bonus.ChooseBonus(this);

            StartCoroutine(_BonusDisable(other.gameObject));

        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "Jump") {

            _rigidbody.freezeRotation = true;

            float posY = transform.eulerAngles.y;

            this.transform.rotation = Quaternion.Euler(0, posY, 0);
        }

        else if (other.gameObject.tag == "Terrain") {

            _vitesseMax += 10;
            //AddReward(0.05f);
        }

        else if (other.gameObject.tag == "Circuit") {

            
            AddReward(-0.01f);
        }
    }

    private IEnumerator _Respawn(float seconds) {


        yield return new WaitForSeconds(seconds);
        this._rigidbody.velocity = new Vector3(0, 0, 0); //Cela Empêche un mouvement lors de la réapparition
        this._vitesse = 0;
        this.transform.position = _checkpoint;
        this.transform.rotation = Quaternion.Euler(0, _checkPointOrientation, 0);
    }

    private IEnumerator _BonusDisable(GameObject bonus) {

        //_bonus.gameObject.SetActive(false); //(Mauvais, car va désactiver TOUS les bonus de la carte.
        bonus.SetActive(false);

        yield return new WaitForSeconds(23f);

        //_bonus.gameObject.SetActive(true);
        bonus.SetActive(true);

    }

    public void AddToMaxSpeed(float value) {

        if (_vitesseMax + value <= 30) {
            _vitesseMax += value;
            AddReward(0.1f);
        }

    }

    public void RemoveToMaxSpeed(float value) {

        //Empêche une vitesse de 0 sur le gazon. 
        if (_vitesseMax - value > 10) {
            _vitesseMax -= value;
            //AddReward(-0.1f);
        }

    }

    public void AddAcceleration(float value) {

        if (value + _acceleration <= 0.15) {
            _acceleration += value;
            AddReward(0.1f);
        }
    }

    public void RemoveAcceleration(float value) {

        //Empêhe d'avoir une accélération de 0. 
        if (_acceleration - value > 0) {
            _acceleration -= value;
            //AddReward(-0.1f);
        }

    }

    public int GetCurrentLaps() {
        return _currentLap;
    }
}
