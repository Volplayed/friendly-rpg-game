using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerTurns : MonoBehaviour
{
    //panels
    public GameObject turnChangePanel;
    public GameObject fightPanel;
    public GameObject levelUpPanel;
    public GameObject recieveNewItemPanel;

    //enemy related
    public EnemyList enemyList;

    //player related
    private GameObject[] players;
    private List<PlayerStats> playerStats = new List<PlayerStats>();
    private GameObject[] cameras;
    private int playersCount;

    //players
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private TMP_Text playerTurnTitleText;
    
    //final boss list
    private Enemy[] finalBosses;

    //turns
    private int currentTurn = 1;
    private int currentPlayerTurn = 0;
    //current turn text
    private TMP_Text currentTurnText;

    //moves 
    private int currentPlayerMoves;

    // Start is called before the first frame update
    void Start()
    {
        //get curent turn text
        currentTurnText = GameObject.FindGameObjectsWithTag("currentTurn")[0].GetComponent<TMP_Text>();

        //add all players to the list
        players = new GameObject[] { player1, player2, player3, player4 };

        //count all players
        int allPlayersCount = players.Length;

        //get players count from static values controller
        playersCount = StaticValuesController.playerAmount;

        //destroy extra players
        for (int i = playersCount; i < allPlayersCount; i++) {
            Destroy(players[i]);
        }

        //get all players stats and set turn to false
        for (int i = 0; i < playersCount; i++) {

            playerStats.Insert(i, players[i].gameObject.GetComponent<PlayerStats>());
            playerStats[i].setHasTurn(false);
        }

        //start with opening turnChangePanel
        openTurnChangePanel();

        //get all cameras
        cameras = new GameObject[playersCount];

        //add all cameras to the list
        for (int i = 0; i < playersCount; i++) {
            cameras[i] = players[i].GetComponentsInChildren<Camera>()[0].gameObject;
        }

        initCameras();

        //set starting camera
        setCameras();

        //get enemy list
        EnemyList enemyList = GameObject.FindGameObjectsWithTag("enemy_list")[0].GetComponent<EnemyList>();

        //get final boss list
        finalBosses = enemyList.getRandomBossList(playersCount);

    }

    //set current player's starting moves
    private void setStartingMoves() {
        currentPlayerMoves = playerStats[currentPlayerTurn].getMoves();
    }

    //change cuurent player moves value
    public void changeCurrentMoves(int value) {
        currentPlayerMoves += value;

        //if current player moves reach zero disable movement for player
        if (currentPlayerMoves <= 0) {
            enableMovement(false);
        }
        
    }

    //get amout of moves left
    public int movesLeft() {
        return currentPlayerMoves;
    }

    //return a player whose turn is now
    public GameObject getCurrentPlayer() {
        return players[currentPlayerTurn];
    }

    //return level up panel
    public GameObject getLevelUpPanel() {
        return levelUpPanel;
    }

    //make button in turn change panel interactable
    public void turnChangePanelButtonSetInteractable(bool value) {
        //set button interactable
        turnChangePanel.GetComponentsInChildren<Button>()[0].interactable = value;
    }

    //turn change panel set button interactable couroutine
    public IEnumerator turnChangePanelButtonSetInteractableCouroutine(float delay) { 
        //wait for a delay
        yield return new WaitForSeconds(delay);

        //set button interactable
        turnChangePanelButtonSetInteractable(true);
    }

    //turn change panel set button interactable after delay
    public void turnChangePanelButtonSetInteractableAfterDelay(float delay = 0.3f) {
        StartCoroutine(turnChangePanelButtonSetInteractableCouroutine(delay));
    }

    private void openTurnChangePanel() {
        //disable turn change button
        turnChangePanelButtonSetInteractable(false);

        //close fight panel
        fightPanel.SetActive(false);

        //get and change turn change panel title depending on a players turn
        playerTurnTitleText = turnChangePanel.GetComponentsInChildren<TMP_Text>()[0];

        //if player didn't lose or win yet
        if (!playerStats[currentPlayerTurn].getDidWin() && !playerStats[currentPlayerTurn].getDidLose()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (currentPlayerTurn + 1) + "'s turn";
        }
        //if player lost
        else if (playerStats[currentPlayerTurn].getDidLose()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (currentPlayerTurn + 1) + " lost";
        }
        //if player won
        else if (playerStats[currentPlayerTurn].getDidWin()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (currentPlayerTurn + 1) + " won!";
        }
        
        //activate turn change panel
        turnChangePanel.SetActive(true);

        //enable turn change panel button after delay
        turnChangePanelButtonSetInteractableAfterDelay(0.5f);

    }

    public void closeTurnChangePanel() {
        //close turn change panel
        turnChangePanel.SetActive(false);
        
        //get player fight component
        GameObject playerUI = fightPanel.transform.parent.gameObject;
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //set player turn
        playerStats[currentPlayerTurn].setHasTurn(true);

        //make in-fight buttons interactable
        playerFight.setButtonIteractable(true);

       //if player is in fight
        if (playerStats[currentPlayerTurn].getInFight()) {
            fightPanel.SetActive(true);

            //disable movement for player
            enableMovement(false);

            //set fight values
            playerFight.setValues();

            //set player's max amout of moves
            setStartingMoves();
        }
        //if player is not in fight and didn't lose or win yet
        else if (!playerStats[currentPlayerTurn].getDidWin() && !playerStats[currentPlayerTurn].getDidLose()) {
            //close fight panel
            fightPanel.SetActive(false);

            //set player's max amout of moves
            setStartingMoves();

            //enable movement after delay
            enableMovementAfterDelay();
        }
        //if player lost or won
        else if (playerStats[currentPlayerTurn].getDidLose() || playerStats[currentPlayerTurn].getDidWin()) {
            //close fight panel
            fightPanel.SetActive(false);

            nextTurn();
        }
   
    }


    //enable or disable movement for current player
    public void enableMovement(bool what) {
        //get hexes array
        Transform[] hexes = players[currentPlayerTurn].GetComponentsInChildren<Transform>();
        hexes = hexes.Where(child => child.tag == "move_hex").ToArray();

        //set in inventory for each hex
        foreach (Transform hex in hexes) {
            HexClickHandler script = hex.gameObject.GetComponent<HexClickHandler>();
            //if what is true
            if (what) {
                //if there are some moves left
                if (currentPlayerMoves >= 1) {
                    //set in inventory to false
                    script.setInInventory(false);

                    //show hex if it is avaliable
                    script.show();
                }
            }
            //if what is false
            else {
                //set in inventory to true
                script.setInInventory(true);

                //hide hex
                script.hide();
            }
        }
    }

    //enable movement after n seconds delay
    public void enableMovementAfterDelay(float delay = 0.3f) {
        StartCoroutine(enableMovementAfterDelayCoroutine(delay));
    }
    //enable movement after n seconds delay coroutine
    private IEnumerator enableMovementAfterDelayCoroutine(float delay) {
        yield return new WaitForSeconds(delay);
        //check if no other windows are active (like level up panel or recieve new item panel) 
        if (!levelUpPanel.activeSelf && !recieveNewItemPanel.activeSelf) {
            enableMovement(true);
        }
    }


    //initialize cameras (find them sorted for each player)
    private void initCameras() {
        for (int i = 0; i < playersCount; i++) {
            cameras[i] = players[i].GetComponentsInChildren<Camera>()[0].gameObject;
        }
    }

    //switches to camera of player you has turn and disables other
    private void setCameras() {
        for (int i = 0; i < playersCount; i++) {
            if (i == currentPlayerTurn) {
                cameras[i].SetActive(true);
            } else {

                cameras[i].SetActive(false);
            }
        }

    }

    //go to next turn
    public void nextTurn() {
        //destroy all popup texts
        destroyAllPopups();

        //check if every player lost or won
        checkFinished();

        //set current player turn to false
        playerStats[currentPlayerTurn].setHasTurn(false);

        //set turn to next
        currentPlayerTurn++;
        //if current player turn is bigger than players count, set it to 0 and increase current turn
        if (currentPlayerTurn >= playersCount) {
            currentPlayerTurn = 0;
            currentTurn++;

            //update current game stage if needed
            enemyList.nextStage(currentTurn);

            //update current turn text
            currentTurnText.SetText("Turn: " + currentTurn);
        }
        setCameras();
        //close inventory if was opened
        PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
        if (inventory.getIsOpened()) {
            inventory.openInventory();
        }
        
        //check if it is final boss turn
        isFinalBossTurn();

        openTurnChangePanel();
        
        //after clicking on button in panel, player will gain his turn
        
    }

    //level up buttons actions
    public void gainAttributes(string attribute) {
        //if strength player gain strength
        if (attribute == "strength") {
            playerStats[currentPlayerTurn].gainStrength();
        }
        //if agility player gain agility
        else if (attribute == "agility") {
            playerStats[currentPlayerTurn].gainAgility();
        }
        //if intelligence player gain intelligence
        else if (attribute == "intelligence") {
            playerStats[currentPlayerTurn].gainIntelligence();
        }

        //close level up panel
        playerStats[currentPlayerTurn].activateLevelUpPanel(false);

    }

    //check if next turn is final boss turn
    public void isFinalBossTurn() {
        //if next turn is final boss turn
        if (currentTurn == StaticValuesController.finalBossTurn) {
            //start final boss fight for current player
            
            playerStats[currentPlayerTurn].startFight(finalBosses[currentPlayerTurn]);
 
        }
    }

    //check if every player lost or won
    public void checkFinished() {
        //check every player
        for (int i = 0; i < playersCount; i++) {
            //if player didn't lose or win yet
            if (!playerStats[i].getDidLose() && !playerStats[i].getDidWin()) {
                //return
                return;
            }
        }
        //if every player lost or won open main menu scene and show stats
        SceneManager.LoadScene("MainMenu");
        
    }

    //destroy all popup texts
    public void destroyAllPopups() {
        //get all popup texts
        GameObject[] popups = GameObject.FindGameObjectsWithTag("pop_up_text");

        //destroy all popup texts
        foreach (GameObject popup in popups) {
            Destroy(popup);
        }
    }

}
