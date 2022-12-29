using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //player amount panel
    public GameObject playerAmountPanel;

    //menu panel
    public GameObject menuPanel;

    //after game stats canvas
    public GameObject afterGameStatsCanvas;

    void Start() {
        //if game started
        if (StaticValuesController.gameStarted) {
            //set after game stats canvas active
            afterGameStatsCanvas.SetActive(true);

            //set menu canvas inactive
            gameObject.SetActive(false);
        }
    }

    //open player amount
    public void openPlayerAmount() {
        playerAmountPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    //close player amount
    public void closePlayerAmount() {
        playerAmountPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    //set player amount
    public void setPlayerAmount(int amount) {
        //set player amount
        StaticValuesController.playerAmount = amount;

        //set game started to true
        StaticValuesController.gameStarted = true;
            
        //start game
        openGame();
    }

    //open main game scene
    private void openGame() {
        SceneManager.LoadScene("MainGame");
    }
    
}
