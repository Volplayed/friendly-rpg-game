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
        
        //start game
        openGame();
    }

    //open main game scene
    private void openGame() {
        SceneManager.LoadScene("MainGame");
    }
    
}
