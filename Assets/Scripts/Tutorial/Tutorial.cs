using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button endTurnButton;
    public Button inventoryButton;

    //all tutorial panels
    public GameObject[] tutorialPanels;
    
    //turotial panel index
    private int tutorialPanelIndex = 0;

    //colors
    private Color highlightHexColor = new Color(0, 1, 0, 1);

    //bools
    private bool firstTurn = true;
    private bool firstInventory = true;
    private bool firstCloseInventory = true;

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

    //enable hex click handlers
    public void enableHexClickHandlers(bool value) {
        //set avaliable to true for all hexClickHandlers
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            hexClickHandler.set_avaliable(value);
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

    //close tutorial panel and disable all buttons except inventory button
    public void closeTutorialPanelDisableButtonsNotInventory() {
        //disable end turn button
        endTurnButton.interactable = false;

        //enable inventory button
        inventoryButton.interactable = true;

        //close tutorial panel
        closeTutorialPanel();
    }

    //open invetory and open tutorial panel
    public void openInventoryAndTutorialPanel() {
        //open inventory
        playerInventory.openInventory();
        //if first time opening inventory
        if (firstInventory) {
            //set first inventory to false
            firstInventory = false;

            //open tutorial panel
            openTutorialPanel();
        }
        //if first time closing inventory
        else if (firstCloseInventory) {
            //set first close inventory to false
            firstCloseInventory = false;

            //open tutorial panel
            openTutorialPanel();
        }
    }

}
