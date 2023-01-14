using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject menuCanvas;
   
    public void openOptions()
    {
        optionsCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void closeOptions()
    {
        optionsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}
