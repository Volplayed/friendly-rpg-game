using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //all needed scripts
    private PlayerFight playerFight;
    private PlayerInventory playerInventory;
    private PlayerTurns playerTurns;
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;
    private List<HexClickHandler> hexClickHandlers = new List<HexClickHandler>();

    //all needed objects
    public GameObject player;

    public GameObject[] tutorialPanels;
    
    //turotial panel index
    private int tutorialPanelIndex = 0;

    //needed colors
    private Color highlightHexColor = new Color(0, 1, 0, 1);

    //bools
    private bool firstTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        //get all needed scripts
        playerFight = gameObject.GetComponent<PlayerFight>();
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        playerTurns = gameObject.GetComponent<PlayerTurns>();

        //get playerStats
        playerStats = player.GetComponent<PlayerStats>();

        //get playerMovement
        playerMovement = player.GetComponent<PlayerMovement>();

        //get all hexClickHandlers
        hexClickHandlers = playerMovement.getHexesClickHandlers();

        //set avaliable to false for all hexClickHandlers
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            hexClickHandler.set_avaliable(false);
        }
    }

    //close tutorial panel
    public void closeTutorialPanel() {
        //deactivate current tutorial panel
        tutorialPanels[tutorialPanelIndex].SetActive(false);

        //increase tutorial panel index
        tutorialPanelIndex++;
    }

    //open tutorial panel
    public void openTutorialPanel() {
        //set tutorial panel active
        tutorialPanels[tutorialPanelIndex].SetActive(true);
    }

    //jump to next tutorial panel
    public void jumpToNextTutorialPanel() {
        //close current tutorial panel
        closeTutorialPanel();

        //open next tutorial panel
        openTutorialPanel();
    } 

    //turn change window custom close function
    public void closeTurnChangeWindow() {
        //close turn change window
        playerTurns.closeTurnChangePanel();

        //if first turn
        if (firstTurn) {
            //set first turn to false
            firstTurn = false;

            //open tutorial panel
            openTutorialPanel();
        }
    }

}
