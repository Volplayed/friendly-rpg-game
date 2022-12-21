using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class PlayerTurns : MonoBehaviour
{

    public GameObject turnChangePanel;
    public GameObject fightPanel;

    //player related
    private GameObject[] players;
    private List<PlayerStats> playerStats = new List<PlayerStats>();
    private GameObject[] cameras;
    private int players_count;

    private TMP_Text playerTurnTitleText;
    
    //turns
    private int current_turn = 1;
    private int current_player_turn = 0;

    //moves 
    private int current_player_moves;

    // Start is called before the first frame update
    void Start()
    {

        //get all players
        players = GameObject.FindGameObjectsWithTag("Player");

        players_count = players.Length;

        

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

    private void openTurnChangePanel() {
        //close fight panel
        fightPanel.SetActive(false);

        //get and change turn change panel title depending on a players turn
        playerTurnTitleText = turnChangePanel.GetComponentsInChildren<TMP_Text>()[0];
        playerTurnTitleText.SetText("Player " + (current_player_turn + 1) + " turn");

        turnChangePanel.SetActive(true);

    }

    public void closeTurnChangePanel() {
        //close turn change panel
        turnChangePanel.SetActive(false);
       
        //set player turn
        playerStats[current_player_turn].setHasTurn(true);

       //if player is in fight
        if (playerStats[current_player_turn].get_in_fight()) {
            fightPanel.SetActive(true);

            //disable movement for player
            enableMovement(false);

            //get player fight component
            GameObject playerUI = fightPanel.transform.parent.gameObject;
            PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

            playerFight.setValues();

            //set player's max amout of moves
            set_starting_moves();
        }
        else {
            //close fight panel
            fightPanel.SetActive(false);

            //set player's max amout of moves
            set_starting_moves();

            //enable movement
            enableMovement(true);
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
        //set current player turn to false
        playerStats[current_player_turn].setHasTurn(false);

        //set turn to next
        current_player_turn++;
        if (current_player_turn >= players_count) {
            current_player_turn = 0;
            current_turn++;
        }
        set_cameras();
        //close inventory if was opened
        PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();
        if (inventory.getIsOpened()) {
            inventory.openInventory();
        }
        openTurnChangePanel();
        
        //after clicking on button in panel, player will gain his turn
        
    }

}
