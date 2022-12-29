using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AfterGameStats : MonoBehaviour
{
    //text with stats
    public TMP_Text statsText;

    //main menu canvas
    public GameObject mainMenuCanvas;

    void Start()
    {
        //set stats text to winners
        statsText.SetText(getWinners());
    }

    //get winners and create stats text
    public string getWinners() {
        //get winners
        List<string> winners = StaticValuesController.winners;

        //get amount of players
        int playerAmount = StaticValuesController.playerAmount;

        //create text
        string text = "";

        //if no winners
        if (winners.Count == 0) {
            text = "No one won today!";
        }
        //if everyone won
        else if (winners.Count == playerAmount) {
            text = "Everyone won today! Hooray!";
        }
        //if one player won
        else if (winners.Count == 1) {
            text = "The one and only winner of today is " + winners[0] + "!";
        }
        //if more than one player won
        else {
            text = "The winners of today are: ";

            //add all winners
            for (int i = 0; i < winners.Count; i++) {
                text += winners[i];

                //if not last winner
                if (i != winners.Count - 1) {
                    text += ", ";
                }
            }
        }

        return text;
    }

    //close stats and open main menu
    public void toMainMenu() {
        //set main menu canvas to active
        mainMenuCanvas.SetActive(true);

        //reset static values
        resetStaticValues();

        //set this canvas to inactive
        gameObject.SetActive(false);
    }

    //make all static values default
    public void resetStaticValues() {
        //reset game started
        StaticValuesController.gameStarted = false;

        //reset player amount
        StaticValuesController.playerAmount = 0;

        //reset final boss turn
        StaticValuesController.finalBossTurn = 10;

        //reset final boss
        StaticValuesController.finalBoss = null;

        //reset did last attack crit
        StaticValuesController.lastAttackCrit = false;

        //reset winners
        StaticValuesController.winners = new List<string>();
    }
}
