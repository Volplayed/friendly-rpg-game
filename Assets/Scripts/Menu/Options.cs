using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    //sound buttons sprite
    public Sprite soundOn;
    public Sprite soundOff;

    //menu music player
    public AudioSource menuMusic;

    //sound buttons
    public GameObject menuMusicButton;
    public GameObject backgroundMusicButton;

    private void Start() {
        //check if sounds is on or off
        //menu music
        if (PlayerPrefs.GetInt("menuMusic", 0) == 0) {
            //change button sprite
            menuMusicButton.GetComponent<Image>().sprite = soundOn;
            menuMusic.mute = false;
        } else {
            //change button sprite
            menuMusicButton.GetComponent<Image>().sprite = soundOff;
            menuMusic.mute = true;
        }
        //background music
        if (PlayerPrefs.GetInt("backgroundMusic", 0) == 0) {
            //change button sprite
            backgroundMusicButton.GetComponent<Image>().sprite = soundOn;
        } else {
            //change button sprite
            backgroundMusicButton.GetComponent<Image>().sprite = soundOff;
        }
    }

    //click handlers
    public void changeMenuMusic() {
        //if sound is on
        if (PlayerPrefs.GetInt("menuMusic", 0) == 0) {
            //change button sprite
            menuMusicButton.GetComponent<Image>().sprite = soundOff;
            //turn sound off
            menuMusic.mute = true;
            //change player prefs value
            PlayerPrefs.SetInt("menuMusic", 1);
        } else {
            //change button sprite
            menuMusicButton.GetComponent<Image>().sprite = soundOn;
            //turn sound on
            menuMusic.mute = false;
            //change player prefs value
            PlayerPrefs.SetInt("menuMusic", 0);
        }
    }

    public void changeBackgroundMusic() {
        //if sound is on
        if (PlayerPrefs.GetInt("backgroundMusic", 0) == 0) {
            //change button sprite
            backgroundMusicButton.GetComponent<Image>().sprite = soundOff;
            //change player prefs value
            PlayerPrefs.SetInt("backgroundMusic", 1);
        } else {
            //change button sprite
            backgroundMusicButton.GetComponent<Image>().sprite = soundOn;
            //change player prefs value
            PlayerPrefs.SetInt("backgroundMusic", 0);
        }
    }
}
