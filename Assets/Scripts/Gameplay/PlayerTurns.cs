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
    private int players_count;

    private TMP_Text playerTurnTitleText;
    
    //final boss list
    private Enemy[] finalBosses;

    //turns
    private int current_turn = 1;
    private int current_player_turn = 0;
    //current turn text
    private TMP_Text currentTurnText;

    //moves 
    private int current_player_moves;

    // Start is called before the first frame update
    void Start()
    {
        //get curent turn text
        currentTurnText = GameObject.FindGameObjectsWithTag("current_turn")[0].GetComponent<TMP_Text>();

        //get all players
        players = GameObject.FindGameObjectsWithTag("Player");

        //count all players
        int all_players_count = players.Length;

        //get players count from static values controller
        players_count = StaticValuesController.playerAmount;

        //destroy extra players
        for (int i = players_count; i < all_players_count; i++) {
            Destroy(players[i]);
        }

        //get all players stats and set turn to false
        for (int i = 0; i < players_count; i++) {

            playerStats.Insert(i, players[i].gameObject.GetComponent<PlayerStats>());
            playerStats[i].setHasTurn(false);
        }

        //start with opening turnChangePanel
        openTurnChangePanel();

        //get all cameras
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        initCameras();

        //set starting camera
        set_cameras();

        //get enemy list
        EnemyList enemyList = GameObject.FindGameObjectsWithTag("enemy_list")[0].GetComponent<EnemyList>();

        //get final boss list
        finalBosses = enemyList.get_random_boss_list(players_count);

    }

    //set current player's starting moves
    private void set_starting_moves() {
        current_player_moves = playerStats[current_player_turn].get_moves();
    }

    //change cuurent player moves value
    public void change_current_moves(int value) {
        current_player_moves += value;

        //if current player moves reach zero disable movement for player
        if (current_player_moves <= 0) {
            enableMovement(false);
        }
        
    }

    //get amout of moves left
    public int moves_left() {
        return current_player_moves;
    }

    //return a player whose turn is now
    public GameObject getCurrentPlayer() {
        return players[current_player_turn];
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
        if (!playerStats[current_player_turn].get_did_win() && !playerStats[current_player_turn].get_did_lose()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (current_player_turn + 1) + "'s turn";
        }
        //if player lost
        else if (playerStats[current_player_turn].get_did_lose()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (current_player_turn + 1) + " lost";
        }
        //if player won
        else if (playerStats[current_player_turn].get_did_win()) {
            //set player turn title
            playerTurnTitleText.text = "Player " + (current_player_turn + 1) + " won!";
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
        playerStats[current_player_turn].setHasTurn(true);

        //make in-fight buttons interactable
        playerFight.set_button_interactable(true);

       //if player is in fight
        if (playerStats[current_player_turn].get_in_fight()) {
            fightPanel.SetActive(true);

            //disable movement for player
            enableMovement(false);

            //set fight values
            playerFight.setValues();

            //set player's max amout of moves
            set_starting_moves();
        }
        //if player is not in fight and didn't lose or win yet
        else if (!playerStats[current_player_turn].get_did_win() && !playerStats[current_player_turn].get_did_lose()) {
            //close fight panel
            fightPanel.SetActive(false);

            //set player's max amout of moves
            set_starting_moves();

            //enable movement after delay
            enableMovementAfterDelay();
        }
        //if player lost or won
        else if (playerStats[current_player_turn].get_did_lose() || playerStats[current_player_turn].get_did_win()) {
            //close fight panel
            fightPanel.SetActive(false);

            next_turn();
        }
   
    }


    //enable or disable movement for current player
    public void enableMovement(bool what) {
        //get hexes array
        Transform[] hexes = players[current_player_turn].GetComponentsInChildren<Transform>();
        hexes = hexes.Where(child => child.tag == "move_hex").ToArray();

        //set in inventory for each hex
        foreach (Transform hex in hexes) {
            HexClickHandler script = hex.gameObject.GetComponent<HexClickHandler>();
            //if what is true
            if (what) {
                //if there are some moves left
                if (current_player_moves >= 1) {
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
        for (int i = 0; i < players_count; i++) {
            cameras[i] = players[i].GetComponentsInChildren<Camera>()[0].gameObject;
        }
    }

    //switches to camera of player you has turn and disables other
    private void set_cameras() {
        for (int i = 0; i < players_count; i++) {
            if (i == current_player_turn) {
                cameras[i].SetActive(true);
            } else {

                cameras[i].SetActive(false);
            }
        }

    }

    //go to next turn
    public void next_turn() {
        //check if every player lost or won
        check_finished();

        //set current player turn to false
        playerStats[current_player_turn].setHasTurn(false);

        //set turn to next
        current_player_turn++;
        //if current player turn is bigger than players count, set it to 0 and increase current turn
        if (current_player_turn >= players_count) {
            current_player_turn = 0;
            current_turn++;

            //update current game stage if needed
            enemyList.next_stage(current_turn);

            //update current turn text
            currentTurnText.SetText("Turn: " + current_turn);
        }
        set_cameras();
        //close inventory if was opened
        PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
        if (inventory.getIsOpened()) {
            inventory.openInventory();
        }
        
        //check if it is final boss turn
        is_final_boss_turn();

        openTurnChangePanel();
        
        //after clicking on button in panel, player will gain his turn
        
    }

    //level up buttons actions
    public void gain_attributes(string attribute) {
        //if strength player gain strength
        if (attribute == "strength") {
            playerStats[current_player_turn].gain_strength();
        }
        //if agility player gain agility
        else if (attribute == "agility") {
            playerStats[current_player_turn].gain_agility();
        }
        //if intelligence player gain intelligence
        else if (attribute == "intelligence") {
            playerStats[current_player_turn].gain_intelligence();
        }

        //close level up panel
        playerStats[current_player_turn].activateLevelUpPanel(false);

    }

    //check if next turn is final boss turn
    public void is_final_boss_turn() {
        //if next turn is final boss turn
        if (current_turn == StaticValuesController.finalBossTurn) {
            //start final boss fight for current player
            
            playerStats[current_player_turn].start_fight(finalBosses[current_player_turn]);
 
        }
    }

    //check if every player lost or won
    public void check_finished() {
        //check every player
        for (int i = 0; i < players_count; i++) {
            //if player didn't lose or win yet
            if (!playerStats[i].get_did_lose() && !playerStats[i].get_did_win()) {
                //return
                return;
            }
        }
        //if every player lost or won open main menu scene and show stats
        SceneManager.LoadScene("MainMenu");
        
    }

}
