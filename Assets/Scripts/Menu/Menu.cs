using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //player amount panel
    public GameObject playerAmountPanel;

    //game length panel
    public GameObject gameLengthPanel;

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

    //open game length panel
    public void openGameLength() {
        //set game length panel active
        gameLengthPanel.SetActive(true);

        //set player amount panel inactive
        playerAmountPanel.SetActive(false);
    }

    //close game length panel
    public void closeGameLength() {
        //set game length panel inactive
        gameLengthPanel.SetActive(false);

        //set player amount panel active
        playerAmountPanel.SetActive(true);
    }

    //set player amount
    public void setPlayerAmount(int amount) {
        //set player amount
        StaticValuesController.playerAmount = amount;

        //set game started to true
        StaticValuesController.gameStarted = true;
            
        //start game
        openGameLength();
    }

    //set game length
    public void setGameLength(int length) {
        //set game length
        StaticValuesController.finalBossTurn = length;

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
