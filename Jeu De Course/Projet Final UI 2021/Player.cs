using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Player : MonoBehaviour
{

    PlayerControls controls;

    [SerializeField] private float _vitesse = 5f;
    [SerializeField] private float _vitesseMax = 20f;
    [SerializeField] private float _vitesseReculonsMax = -5f;
    [SerializeField] private float _acceleration = 1.5f;  
    [SerializeField] private float _vitesseRotation = 20f;
    [SerializeField] private float _brakeSpeed = 8f;
    private bool _accBool, _brakeBool, _reculonsBool, _waitToUseObject;
    Vector3 tourner = new Vector3(0, 0, 0);
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _mainCam, _rearCam;
    [SerializeField] private GameObject _brakeLight, _reverseLight;
    private Vector3 _checkpoint = new Vector3(0, 0, 0);
    private float _checkPointOrientation;
    [SerializeField] private GameObject _frontlight;

    private AudioSource[] _carSound;
    private GameMechanics _gameMechanics;
    private int _currentLap = 1;
    private bool _okayToFinish = false;

    [SerializeField]private PauseMenu _pauseMenuUI;
    private BonusObject _bonus;
    private GestionScene _gstScene;
    private SharingData _data;

    [SerializeField] private TextMeshProUGUI _Laps;
    [SerializeField] private TextMeshProUGUI _txtPosition;
    [SerializeField] private TextMeshProUGUI _txtPos2;
    private int _currentPosition = 1;

    

    void Awake() {

        controls = new PlayerControls();

        
        
        controls.Gameplay.Accelerer.started += ctx => _BoolOperation(ref _accBool, true);
        controls.Gameplay.Accelerer.canceled += ctx => _BoolOperation(ref _accBool, false);

        controls.Gameplay.Brake.started += ctx => _BoolOperation(ref _brakeBool, true);
        controls.Gameplay.Brake.canceled += ctx => _BoolOperation(ref _brakeBool, false);

        controls.Gameplay.Tourner.performed += ctx => tourner.y = ctx.ReadValue<float>();

        controls.Gameplay.Tourner.canceled += ctx => tourner = Vector3.zero;

        controls.Gameplay.RegarderArriere.started += ctx => _LookBehind();
        controls.Gameplay.RegarderArriere.canceled += ctx => _LookForward();

        controls.Gameplay.ChangeCameraRotation.performed += ctx => _CameraSwitchRotation();
        
        controls.Gameplay.Pause.performed += ctx => _pauseMenuUI._WantToPause();

        controls.Gameplay.UseObject.started += ctx => _BoolOperation(ref _waitToUseObject, true);
        controls.Gameplay.UseObject.canceled += ctx => _BoolOperation(ref _waitToUseObject, false);


    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _carSound = GetComponentsInChildren<AudioSource>();
        _gameMechanics = FindObjectOfType<GameMechanics>();
        _bonus = FindObjectOfType<BonusObject>();
        _gstScene = FindObjectOfType<GestionScene>();
        _data = FindObjectOfType<SharingData>();

        _noSound();
       _carSound[4].Play();
        
        

        ////////////////////////////////////////////////
        //_MuteSound();
    }

    // Update is called once per frame
    void Update()
    {

        if (!_pauseMenuUI.IsPause()) {
            _Mouvement();
        }
        
       /* for(int i=0; i <_carSound.Length; i++) {
            Debug.Log(i);
            if  (_carSound[i].isPlaying == true)
            Debug.Break();
        }*/
    }



    /*private void _Mouvement() {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A et D
        float vertical = Input.GetAxisRaw("Vertical"); // W et S
        Vector3 dirEnCours = new Vector3(0, 0, 1);

        //Il doit appuyer et être en mouvement pour pouvoir tourner.
        //Gauche / Droite
        if (horizontal != 0 && _vitesse != 0) {
            Vector3 direction = new Vector3(0, horizontal, 0).normalized;
            this.transform.Rotate(direction * _vitesseRotation * Time.deltaTime);
        }

        //Avancer / Reculer
        if (vertical != 0) {
            Vector3 direction = new Vector3(0, 0f, vertical).normalized;
            this.transform.Translate(direction * _vitesse * Time.deltaTime);
            

            if (direction.z == 1) {
                _Acceleration(_vitesseMax);
                
            }

            else if (direction.z == -1) {

                
                _Deceleration(8f);
                
                //_Acceleration(_vitesseReculonsMax);
                
            }
        }

        //On ralentit
        else {
            this.transform.Translate(dirEnCours * _vitesse * Time.deltaTime);
            _Deceleration(1f);
        }

        
    }*/

    private void _Mouvement() {
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
    }

        private void _Acceleration(float maxSpeed) {
        /*if (_vitesse == 0) {
            _vitesse = 0.001f; //Car 0 * acceleration = 0.
        }*/

        //Note : Une fonction logarithme pourrait être intéressante.
       
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

    

    private void _BoolOperation(ref bool entry, bool result) {
        entry = result;
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Jump") {

            _rigidbody.freezeRotation = false;
        }

        else if (other.gameObject.tag == "Terrain") {

            _vitesseMax -= 10;
        }

        else if (other.gameObject.tag == "Respawn") {


            StartCoroutine(_Respawn(1.75f));
            
        }

        else if (other.gameObject.tag == "Checkpoint" || other.gameObject.tag == "Finish") {
            _checkpoint = other.transform.position;
            _checkPointOrientation = other.transform.eulerAngles.y;
            
            

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

                    SwitchScene();
                }

                _Laps.text = _currentLap.ToString(); //On affiche le tour actuelle
                

            }
        }

        else if (other.gameObject.tag == "Bonus") {

            _bonus.ChooseBonus();

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
        }
    }

    private void _LookBehind() {

        if (!_pauseMenuUI.IsPause()) {

            _mainCam.SetActive(false);

            _rearCam.SetActive(true);
        }
    }

    private void _LookForward() {

        //Si on met le if, le jeu "bug" et reste sur la caméra du devant lorsqu'il n'est plus en pause.
        //if (!_pauseMenuUI.IsPause()) {
            _rearCam.SetActive(false);

            _mainCam.SetActive(true);
        //}
    }

    private void _noSound() {
        
        for (int i = 0; i < _carSound.Length; i++) {
            _carSound[i].Stop();
        }
    }

    private void _MuteSound() {
        for (int i = 0; i < _carSound.Length; i++) {
            _carSound[i].mute = true;
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

    private void _CameraSwitchRotation() {

        if (!_pauseMenuUI.IsPause()) {

            if (_mainCam.transform.localRotation.eulerAngles.x == 3) {
                //_mainCam.transform.Rotate(-4, 0, 0);
                _mainCam.transform.localRotation = Quaternion.Euler(8, 0, 0);
            }

            else if (_mainCam.transform.localRotation.eulerAngles.x == 4) {
                // _mainCam.transform.Rotate(-1, 0, 0);
                _mainCam.transform.localRotation = Quaternion.Euler(3, 0, 0);
            }

            else if (_mainCam.transform.localRotation.eulerAngles.x == 8) {
                // _mainCam.transform.Rotate(5, 0, 0);
                _mainCam.transform.localRotation = Quaternion.Euler(4, 0, 0);
            }


            else {
                _mainCam.transform.localRotation = Quaternion.Euler(8, 0, 0);
            }

        }
    }

    public void AddToMaxSpeed(float value) {

        if (_vitesseMax + value <= 30) {
            _vitesseMax += value;
        }
    }

    public void RemoveToMaxSpeed(float value) {

        //Empêche une vitesse de 0 sur le gazon. 
        if (_vitesseMax - value > 10) {
            _vitesseMax -= value;
        }

    }

    public void AddAcceleration(float value) {

        if (value + _acceleration <= 0.15) {
            _acceleration += value;
        }

    }

    public void RemoveAcceleration(float value) {

        //Empêhe d'avoir une accélération de 0. 
        if (_acceleration - value > 0) {
            _acceleration -= value;
        }

    }

    public int GetCurrentPosition() {
        return _currentPosition;
    }

    public void SetCurrentPosition(int value) {
        _currentPosition = value;
    }

    public void SwitchScene() {
        _data.SetPositionString(_currentPosition.ToString());
        _gstScene.EndScene();
    }

    public bool UseObjectValue() {

        return _waitToUseObject;

    }

    public int GetCurrentLaps() {
        return _currentLap;
    }
}
