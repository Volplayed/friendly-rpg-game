using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HexClickHandler : MonoBehaviour
{
    private double fightChance = Coefficient.defaultFightChance;

    //other objects
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private PlayerTurns playerTurns;

    //fight objects
    private GameObject enemyListObject;
    private EnemyList enemyList;

    private PlayerStats playerStats;

    //is moving avaliable?
    private bool avaliable = true;
    private bool inInventory = false;
    private bool hasTurn = false;

    //for starting fight
    private bool otherPlayer = false;
    private GameObject otherPlayerObject;
    private PlayerStats otherPlayerStats;

    void Start()
    {
        //find player
        player = gameObject.transform.parent.gameObject;

        //find enemyListObject
        enemyListObject = GameObject.FindGameObjectsWithTag("enemy_list")[0];

        //get EnemyList component
        enemyList = enemyListObject.GetComponent<EnemyList>();

        //get playerStats
        playerStats = player.GetComponent<PlayerStats>();

        //get playerUI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerTurns
        playerTurns = playerUI.GetComponent<PlayerTurns>();
        
    }

    public void movePlayer(Vector3 vector)
    {
        player.transform.position = vector;

        //decrease player current moves by 1
        playerTurns.changeCurrentMoves(-1);

        //reset all hex colliders
        playerStats.resetHexColliders();

        //add move to game statistics
        PlayerGameStatistics.addMovesMade(playerTurns.getCurrentPlayerTurn() + 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if wall disable and hide
        if (col.gameObject.tag == "walls")
        {
            //hide hex
            spriteRenderer.enabled = false;
            //disable hex
            avaliable = false;
            
        }
        //if other player
        else if (col.gameObject.tag == "player_collider")
        {
            Debug.Log("other player");
            //get other player object from collider
            GameObject other = col.gameObject.transform.parent.gameObject;

            //if other player is not in fight
            if (!other.GetComponent<PlayerStats>().getInFight())
            {
                //set other player to true
                otherPlayer = true;

                //get collided object parent
                otherPlayerObject = other;

                //get other player stats
                otherPlayerStats = other.GetComponent<PlayerStats>();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if walls enable and show
     
        if (col.gameObject.tag == "walls")
        {   
            //if there are still moves left
            if (playerTurns.movesLeft() >= 1) {
                //show hex
                spriteRenderer.enabled = true;
            }
            
            //enable hex
            avaliable = true;
        }
        //if other player
        else if (col.gameObject.tag == "player_collider")
        {
            //set other player to false
            otherPlayer = false;

            //set other player object to null
            otherPlayerObject = null;

            //set other player stats to null
            otherPlayerStats = null;
        }
    }

    //hide hex 
    public void hide() {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
    }
    //show hex if is avaliable
    public void show() {
        //if is avaliable
        if (avaliable) {
            //find hex renderer
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.enabled = true;
        }
    }

    //is inventory open

    public void setInInventory(bool what) {

        inInventory = what;

        //if what is false - show
        if (!what) {
            //show hex if it is avaliable
            show();
        }
        //if what is true - hide
        else {
            //hide hex
            hide();
        }
    }

    //set avaliable 
    public void setAvaliable(bool what) {
        //enable hex
        avaliable = what;
    }

    //set fight chance
    public void setFightChance(double chance) {
        fightChance = chance;
    }

    public void setHasTurn(bool what) {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hasTurn = what;
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        //show if not a wall and own turn
        if (what){
            collider.enabled = true;
            //show hex if avaliable
            show();
        }
        //else hide
        else {
            collider.enabled = false;
            //hide hex
            hide();
        }
    }


    public void goToHex()
    {
        //hex grid
        Grid grid = player.transform.parent.GetComponent<Grid>();

        //clicked hex coords
        Vector3Int destination = grid.WorldToCell(transform.position);

        movePlayer(grid.CellToWorld(destination));


        randomFightCheck(fightChance);

    }

    //click handler
    void OnMouseDown()
    {
        Debug.Log(otherPlayer);
        Debug.Log(!inInventory);
        Debug.Log(avaliable);
        
        //if is avaliable and inventory is not open move player
        if (avaliable && !inInventory && hasTurn && !otherPlayer)
        {
            goToHex();
        }
        //if other player is in hex and enemy is not in fight or can be attacked and inventory is not open start fight
        else if (otherPlayer && otherPlayerStats.getCanBeAttacked() && !inInventory && hasTurn)
        {
            //decrease player current moves by 1
            playerTurns.changeCurrentMoves(-1);
            //start fight with other player
            playerStats.startFight(otherPlayerObject);
        }
            
    }
    //start fight with some chance
    private void randomFightCheck(double chance) {
        if (chance >= Random.Range(0f, 1)) {
            playerStats.startFight(enemyList.getRandomEnemy());
        }
    }

    //reset hex collider coroutine
    private IEnumerator resetColliderCotoutine() {
        //get hex collider
        Collider2D collider = gameObject.GetComponent<Collider2D>();

        //disable collider
        collider.enabled = false;

        //wait for 0.01 seconds
        yield return new WaitForSeconds(0.01f);

        //enable collider
        collider.enabled = true;
    }

    //reset hex collider
    public void resetCollider() {
        StartCoroutine(resetColliderCotoutine());
    }
}




