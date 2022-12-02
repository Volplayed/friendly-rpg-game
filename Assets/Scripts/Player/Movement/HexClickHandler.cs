using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HexClickHandler : MonoBehaviour
{


    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool avaliable = true;
    private bool in_inventory = false;
    private bool has_turn = false;

    void Start()
    {
        //find player
        player = gameObject.transform.parent.gameObject;

        

    }

    void move_player(Vector3 vector)
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

    }



    //clicl handler
    void OnMouseDown()
    {
        Debug.Log(!in_inventory);
        Debug.Log(has_turn);
        if (avaliable && !in_inventory && has_turn)
        {
            go_to_hex();
        }
            
    }
}




