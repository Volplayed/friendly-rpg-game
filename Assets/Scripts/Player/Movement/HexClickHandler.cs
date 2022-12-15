using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HexClickHandler : MonoBehaviour
{
    private const double fightChance = 0.1;

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
    private bool in_inventory = false;
    private bool has_turn = false;

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

    public void move_player(Vector3 vector)
    {
        player.transform.position = vector;

        //decrease player current moves by 1
        playerTurns.change_current_moves(-1);
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
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if walls enable and show
     
        if (col.gameObject.tag == "walls")
        {   
            //if there are still moves left
            if (playerTurns.moves_left() >= 1) {
            //show hex
            spriteRenderer.enabled = true;
            }
            
            //enable hex
            avaliable = true;
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
        Debug.Log("-----" + avaliable);
        //if is avaliable
        if (avaliable) {
            //find hex renderer
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.enabled = true;
        }
    }

    //is inventory open

    public void setInInventory(bool what) {

        in_inventory = what;

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
    public void set_avaliable(bool what) {
        //enable hex
        avaliable = what;
    }

    public void setHasTurn(bool what) {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        has_turn = what;
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


    public void go_to_hex()
    {
        //hex grid
        Grid grid = player.transform.parent.GetComponent<Grid>();

        //clicked hex coords
        Vector3Int destination = grid.WorldToCell(transform.position);

        move_player(grid.CellToWorld(destination));


        random_fight_check(fightChance);

    }

    //click handler
    void OnMouseDown()
    {
        Debug.Log(!in_inventory);
        Debug.Log(avaliable);
        if (avaliable && !in_inventory && has_turn)
        {
            go_to_hex();
        }
            
    }
    //start fight with some chance
    private void random_fight_check(double chance) {
        if (chance >= Random.Range(0f, 1)) {
            playerStats.start_fight(enemyList.get_random_enemy());
        }
    }

}




