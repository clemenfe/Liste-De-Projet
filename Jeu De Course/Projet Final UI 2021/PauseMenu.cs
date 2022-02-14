using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour {



     private bool _gameIsPaused = false;
     [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionUI;



    // Start is called before the first frame update
    void Start() {

         
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        optionUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void _WantToPause() {

        if (_gameIsPaused) {
            Resume();
        }

        else {
            Pause();
        }

    }

    public bool IsPause() {

        return _gameIsPaused;

    }
}
