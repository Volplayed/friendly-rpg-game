using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsApplier : MonoBehaviour
{
    //background music player
    public AudioSource backgroundMusic;

    private void Start()
    {
        //if the background music is on
        if (PlayerPrefs.GetInt("backgroundMusic") == 0)
        {
            //unmute the background music
            backgroundMusic.mute = false;
        }
        else
        {
            //mute the background music
            backgroundMusic.mute = true;
        }
    }
}
