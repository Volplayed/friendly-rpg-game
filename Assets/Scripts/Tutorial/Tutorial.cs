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
    
    //fight buttons
    public Button attackButton;
    public Button healButton;
    public Button retreatButton;
    public Button skipTurnButton;

    public GameObject firstMoveHex;
    //all tutorial panels
    public GameObject[] tutorialPanels;
    
    //turotial panel index
    private int tutorialPanelIndex = 0;

    //colors
    private Color highlightHexColor = new Color(0, 1, 0, 0.4f);
    private Color defaultHexColor = new Color(0.6126094f, 0.60534f, 0.9811321f, 0.3529412f);

    //bools
    private bool firstTurn = true;
    private bool firstInventory = true;
    private bool firstCloseInventory = true;
    private bool firstMove = true;
    private bool firstEndTurn = true;
    private bool firstFight = true;
    private bool firstAttack = true;
    private bool firstFightEndTurn = true;
    private bool firstFightFinish = true;

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

        //give player 9 exp
        playerStats.set_exp(9);
    }

    //enable hex click handlers
    public void enableHexClickHandlers(bool value) {
        //set avaliable to true for all hexClickHandlers
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            hexClickHandler.set_avaliable(value);
        }
    }

    //enable hex click handlers except
    public void enableHexClickHandlersExcept(bool value, HexClickHandler hexClickHandler) {
        //set avaliable to true for all hexClickHandlers
        foreach (HexClickHandler hexClickHandler_ in hexClickHandlers) {
            if (hexClickHandler_ != hexClickHandler) {
                hexClickHandler_.set_avaliable(value);
            }
        }
    }

    //show hexes
    public void showHexes() {
        //show each hex
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            //get sprite renderer
            SpriteRenderer spriteRenderer = hexClickHandler.GetComponent<SpriteRenderer>();

            //enable sprite renderer
            spriteRenderer.enabled = true;
        }
    }

    //hide hexes
    public void hideHexes() {
        //hide each hex
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            //get sprite renderer
            SpriteRenderer spriteRenderer = hexClickHandler.GetComponent<SpriteRenderer>();

            //disable sprite renderer
            spriteRenderer.enabled = false;
        }
    }

    //enable hex colliders
    public void enableHexColliders(bool value) {
        //enable each hex collider
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            //get collider
            Collider2D collider = hexClickHandler.gameObject.GetComponent<Collider2D>();

            //enable collider
            collider.enabled = value;
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
        //if first end turn
        else if (firstEndTurn) {
            //set first end turn to false
            firstEndTurn = false;

            //open tutorial panel
            openTutorialPanel();

            //disable all hexes
            enableHexClickHandlers(false);

            //show all hexes
            showHexes();

            //get sprite renderer
            SpriteRenderer spriteRenderer = firstMoveHex.GetComponent<SpriteRenderer>();

            //set color to default color
            spriteRenderer.color = defaultHexColor;
        }
        //if first fight end turn
        else if (firstFightEndTurn) {
            //set first fight end turn to false
            firstFightEndTurn = false;

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

    //jump to next tutorial panel and enable all hexes
    public void jumpToNextTutorialPanelEnableOneHex(GameObject hex) {
        //jump to next tutorial panel
        jumpToNextTutorialPanel();

        //show all hexes
        showHexes();
    }

    //close tutorial panel and recolor hex
    public void closeTutorialPanelRecolorHex(GameObject hex) {
        //jump to next tutorial panel
        closeTutorialPanel();

        //enable all hexes
        enableHexClickHandlers(true);

        //get hex sprite renderer
        SpriteRenderer hexSpriteRenderer = hex.GetComponent<SpriteRenderer>();

        //set hex color to highlight color
        hexSpriteRenderer.color = highlightHexColor;

        //get hex click handler
        HexClickHandler hexClickHandler = hex.GetComponent<HexClickHandler>();

        //disable all hexes except one
        enableHexClickHandlersExcept(false, hexClickHandler);

        //set hex fight chacne to 0
        hexClickHandler.setFightChance(0);
    }

    void Update()
    {
        //if first move
        if (firstMove) {
            //if player moved to appropriate position
            if (playerMovement.transform.position == new Vector3(-1.5f,1.73195314f,0)) {
                //set first move to false
                firstMove = false;

                //open next tutorial panel
                openTutorialPanel();

                //enable end turn button
                endTurnButton.interactable = true;
            }
        }
        //if first fight and in fight
        else if (firstFight && playerStats.get_in_fight()) {
            //set first fight to false
            firstFight = false;

            //open next tutorial panel
            openTutorialPanel();
        }
        //if first fight finish and exp changed
        else if (firstFightFinish && playerStats.get_exp() != 9) {
            //set first fight finish to false
            firstFightFinish = false;

            //open next tutorial panel
            openTutorialPanel();
        }
    }

    //end turn and open tutorial panel if first end turn
    public void endTurnAndSkipOtherPlayerTurn() {
        //end turn
        playerTurns.next_turn();

        //end turn for second player
        playerTurns.next_turn(); 
    }

    //close tutorial panel and enable all hexes and set fight chance to 100
    public void closeTutorialPanelSetFightChanceTo100() {
        //close tutorial panel
        closeTutorialPanel();

        //set fight chance to 100
        foreach (HexClickHandler hexClickHandler in hexClickHandlers) {
            hexClickHandler.setFightChance(100);
        }

        //enable all hexes
        enableHexClickHandlers(true);
    }

    //close tutorial panel disable all fught buttons except attack button
    public void closeTutorialPanelDisableAllFightButtonsExceptAttack() {
        //close tutorial panel
        closeTutorialPanel();

        //disable all fight buttons
        healButton.interactable = false;
        retreatButton.interactable = false;
        skipTurnButton.interactable = false;

        //enable attack button
        attackButton.interactable = true;
    }

    //attack enemy and show tutorial panel if first attack
    public void attackEnemyAndShowTutorialPanel() {
        //attack enemy
        playerFight.player_attack();

        //if first attack
        if (firstAttack) {
            //set first attack to false
            firstAttack = false;

            //open tutorial panel
            openTutorialPanel();
        }
    }

    //equip new item and open tutorial panel
    public void equipNewItemAndOpenTutorialPanel() {
        //equip new item
        playerInventory.equip_new_item();

        //open tutorial panel
        openTutorialPanel();
    }

    //open tutorial panel and gain stat
    public void openTutorialPanelAndGainStat(string attribute) {
        //open tutorial panel
        openTutorialPanel();

        //gain attribute
        playerTurns.gain_attributes(attribute);
    }
}
