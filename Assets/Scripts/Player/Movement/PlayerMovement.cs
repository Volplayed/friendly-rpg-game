using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //all surrounding hexes
    public GameObject hex_top, hex_bot, hex_top_left, hex_bot_left, hex_top_right, hex_bot_right;



    void GetCells(GameObject hex_top, GameObject hex_bot, GameObject hex_top_left, GameObject hex_bot_left, GameObject hex_top_right, GameObject hex_bot_right)
    {
        //main grid
        Grid grid = transform.parent.GetComponent<Grid>();

        //current player hex position
        Vector3Int current = grid.WorldToCell(transform.position);

        //other hexes
        Vector3Int top = new Vector3Int(current.x + 1, current.y, 0);
        Vector3Int bottom = new Vector3Int(current.x - 1, current.y, 0);
        Vector3Int top_left = new Vector3Int(current.x, current.y - 1, 0);
        Vector3Int top_right = new Vector3Int(current.x, current.y + 1, 0);
        Vector3Int bottom_left = new Vector3Int(current.x - 1, current.y - 1, 0);
        Vector3Int bottom_rigth = new Vector3Int(current.x - 1, current.y + 1, 0);


        //place hexes in right positions
        hex_top.transform.position = grid.CellToWorld(top);
        hex_bot.transform.position = grid.CellToWorld(bottom);
        hex_top_left.transform.position = grid.CellToWorld(top_left);
        hex_top_right.transform.position = grid.CellToWorld(top_right);
        hex_bot_left.transform.position = grid.CellToWorld(bottom_left);
        hex_bot_right.transform.position = grid.CellToWorld(bottom_rigth);
    }

    public List<HexClickHandler> getHexesClickHandlers() {
        List<HexClickHandler> hexHandlers = new List<HexClickHandler>();
        //insert to the list
        hexHandlers.Insert(0 ,hex_top.GetComponent<HexClickHandler>());
        hexHandlers.Insert(1 ,hex_bot.GetComponent<HexClickHandler>());
        hexHandlers.Insert(2 ,hex_top_left.GetComponent<HexClickHandler>());
        hexHandlers.Insert(3 ,hex_top_right.GetComponent<HexClickHandler>());
        hexHandlers.Insert(4 ,hex_bot_left.GetComponent<HexClickHandler>());
        hexHandlers.Insert(5 ,hex_bot_right.GetComponent<HexClickHandler>());


        return hexHandlers;

    }
    // Start is called before the first frame update
    void Start()
    {
        //NOT NEEDED NOW BECAUSE PREFAB WITH CORRECTLY PLACED HEXES IS SAVED
        //GetCells(hex_top, hex_bot, hex_top_left, hex_bot_left, hex_top_right, hex_bot_right);
    }

}
