using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour {

   /* private void Awake() {
        DontDestroyOnLoad(this);
    }*/


    public void GoMenuScene() {

        SceneManager.LoadScene("Menu");

    }

    public void PlayScene() {

        
        SceneManager.LoadScene("Course 1");



    }

    public void EndScene() {
        SceneManager.LoadScene(2);
    }

    public void Quit() {

        Application.Quit();

    }

    
}
