using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
public class PlayerInventory : MonoBehaviour
{

    private bool opened = false;
    private Transform[] hexes;

    public TMP_Text health_text, damage_text, armor_text, strength_text, agility_text, intelligence_text, level_text, exp_text;

    public GameObject panel;
    private GameObject player;
    private PlayerTurns playerTurns;

     void Start()
    {
        playerTurns = gameObject.GetComponent<PlayerTurns>();
    }

    //start on inventory button click
    public void openInventory() {
        //get player
        player = playerTurns.getCurrentPlayer();

        //if inventory not opened
        if (!opened) {
            //open inventory window
            panel.SetActive(true);
            opened = true;

            //disable movement
            playerTurns.enableMovement(!opened);

            //set inventory stats
            set_stats();
        }
        //if inventory is opened
        else {
            //close inventory window
            panel.SetActive(false);
            opened = false;

            player.transform.position = player.transform.position;
            //enable movement
            playerTurns.enableMovement(!opened);
        }

    }

    private void set_stats() {
        //set player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();
        health_text.SetText("Health: " + stats.get_health());
        damage_text.SetText("Damage: " + stats.get_damage());
        armor_text.SetText("Armor: " + stats.get_armor());
        strength_text.SetText("Strength: " + stats.get_strength());
        agility_text.SetText("Agility: " + stats.get_agility());
        intelligence_text.SetText("Intelligence: " + stats.get_intelligence());
        exp_text.SetText("Experience: " + stats.get_exp() + "/" + stats.get_needed_exp());
        level_text.SetText("Level: " + stats.get_level());
    }


    //get value
    public bool getIsOpened() {
        return opened;
    }

}
