using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HexClickHandler : MonoBehaviour
{
    private const double fightChance = 0.1;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

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
        
    }

    public void move_player(Vector3 vector)
    {
        player.transform.position = vector;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if wall disable and hide
        if (col.gameObject.tag == "walls")
        {

            spriteRenderer.enabled = false;
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
            spriteRenderer.enabled = true;
            avaliable = true;
            
        }
    }

    //is inventory open

    public void setInInventory(bool what) {

        in_inventory = what;

    }

    public void setHasTurn(bool what) {
        //find hex renderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        has_turn = what;
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        //show if not a wall and own turn
        if (what){
            collider.enabled = true;
            if (avaliable) {
                spriteRenderer.enabled = true;
            } 
        }
        //else hide
        else {
            collider.enabled = false;
            spriteRenderer.enabled = false;
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
        Debug.Log(has_turn);
        if (avaliable && !in_inventory && has_turn)
        {
            go_to_hex();
        }
            
    }
    //start fight with some chance
    private void random_fight_check(double chance) {
        if (chance >= Random.Range(0f, 1)) {
            start_fight();
        }
    }

    //random fight with enemy start
    public void start_fight() {
        playerStats.start_fight(enemyList.get_random_enemy());
           
        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get playerFigth component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //open fight panel and set values
        fightPanel.SetActive(true);
        playerFight.setValuesStart();
    }
}




