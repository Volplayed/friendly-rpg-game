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
    private PlayerTurns turns;


    public void openInventory() {
        player = turns.getCurrentPlayer();
        if (!opened) {
            panel.SetActive(true);
            opened = true;
            enableMovement(!opened);
            set_stats();
        }
        else {
            panel.SetActive(false);
            opened = false;
            enableMovement(!opened);
            player.transform.position = player.transform.position;
        }

    }

    private void enableMovement(bool what) {
        hexes = player.GetComponentsInChildren<Transform>();
        hexes = hexes.Where(child => child.tag == "move_hex").ToArray();

        foreach (Transform hex in hexes) {
            HexClickHandler script = hex.gameObject.GetComponent<HexClickHandler>();
            script.setInInventory(!what);
        }
    }

    private void set_stats() {
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

    public bool getIsOpened() {
        return opened;
    }

    // Start is called before the first frame update
    void Start()
    {
        turns = gameObject.GetComponent<PlayerTurns>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
