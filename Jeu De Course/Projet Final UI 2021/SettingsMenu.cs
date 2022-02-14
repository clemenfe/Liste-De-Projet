using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    [SerializeField] private AudioMixer[] audioMixer;
    private float lastLevelVolume = 0;
    private bool[] toggle = { false, false };

    public void SetVolumeFX (float volume) {

        if (!toggle[0]) {
            lastLevelVolume = volume;
            audioMixer[0].SetFloat("Volume", volume);

        }

    }



    public void MusicVolume(float volume) {

        if (!toggle[1]) { 
            lastLevelVolume = volume;
            audioMixer[1].SetFloat("Volume", volume);
        }
    }

    private void Mute(int i) {

        
        audioMixer[i].SetFloat("Volume", -80);
            
    }

    private void UnMute(int i) {

        audioMixer[i].SetFloat("Volume", lastLevelVolume);  
        
    }

    public void toggleButton(int i) { 

        if(toggle[i]) {
            UnMute(i);
            toggle[i] = false;
        }

        else if (!toggle[i]) {
            Mute(i);
            toggle[i] = true;
        }

    }

}
