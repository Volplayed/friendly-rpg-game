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

    //new item recieve panel
    public GameObject new_item_panel;

    //new item recieve panel texts and images
    public TMP_Text new_item_name_text, new_item_description_text, new_item_stats_text;
    public Image new_item_image, new_item_background_image;

    //current new item
    private Item current_new_item;

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

            //set inventory items
            set_items();
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
        //get player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();

        //set stats
        health_text.SetText("Health: " + stats.get_health());
        damage_text.SetText("Damage: " + stats.get_damage());
        armor_text.SetText("Armor: " + stats.get_armor());
        strength_text.SetText("Strength: " + stats.get_strength());
        agility_text.SetText("Agility: " + stats.get_agility());
        intelligence_text.SetText("Intelligence: " + stats.get_intelligence());
        exp_text.SetText("Experience: " + stats.get_exp() + "/" + stats.get_needed_exp());
        level_text.SetText("Level: " + stats.get_level());
    }

    //get current new item
    public Item get_current_new_item() {
        return current_new_item;
    }

    //find item image component
    private Image get_item_image(GameObject slot) {
        //get all image components
        Image[] images = slot.GetComponentsInChildren<Image>();

        //check if image is item image
        foreach (Image image in images) {
            if (image.gameObject.tag == "item_image") {
                return image;
            }
        }
        return null;
    }

    //find item background image component
    private Image get_item_background_image(GameObject slot) {
        //get all image components
        Image[] images = slot.GetComponentsInChildren<Image>();

        //check if image is item background image
        foreach (Image image in images) {
            if (image.gameObject.tag == "item_background_image") {
                return image;
            }
        }
        return null;
    }

    //set item background image color based on item rarity
    private void set_item_background_image_color(GameObject slot, int rarity) {
        //get item background image
        Image image = get_item_background_image(slot);

        //common
        if (rarity == 0) {
            //grey
            image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        //uncommon
        else if (rarity == 1) {
            //green
            image.color = new Color(0.5f, 0.9f, 0.3f, 1f);
        }
        //rare
        else if (rarity == 2) {
            //blue
            image.color = new Color(0.5f, 0.5f, 0.9f, 1f);
        }
        //epic
        else if (rarity == 3) {
            //purple
            image.color = new Color(0.6f, 0f, 0.9f, 1f);
        }
        //legendary
        else if (rarity == 4) {
            //yellow
            image.color = new Color(1f, 1f, 0.1f, 1f);
        }
        //mythical
        else if (rarity == 5) {
            //orange
            image.color = new Color(1f, 0.6f, 0f, 1f);
        }
    }

    //set player items
    private void set_items() {
        //get player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();
        //get player items
        List<Item> items = stats.get_items();
       
        //get slots
        GameObject head_slot = GameObject.FindGameObjectsWithTag("head_slot")[0];
        GameObject body_slot = GameObject.FindGameObjectsWithTag("body_slot")[0];
        GameObject leg_slot = GameObject.FindGameObjectsWithTag("leg_slot")[0];
        GameObject feet_slot = GameObject.FindGameObjectsWithTag("feet_slot")[0];
        GameObject hands_slot = GameObject.FindGameObjectsWithTag("hands_slot")[0];
        GameObject weapon_slot = GameObject.FindGameObjectsWithTag("weapon_slot")[0];
        GameObject ring_slot = GameObject.FindGameObjectsWithTag("ring_slot")[0];

        //set item to each slot
        foreach (Item item in items) {
            if (item.itemType == "head") {
                get_item_image(head_slot).sprite = item.itemIcon;
                set_item_background_image_color(head_slot, item.itemRarity);
            }
            else if (item.itemType == "body") {
                get_item_image(body_slot).sprite = item.itemIcon;
                set_item_background_image_color(body_slot, item.itemRarity);
            }
            else if (item.itemType == "leg") {
                get_item_image(leg_slot).sprite = item.itemIcon;
                set_item_background_image_color(leg_slot, item.itemRarity);
            }
            else if (item.itemType == "feet") {
                get_item_image(feet_slot).sprite = item.itemIcon;
                set_item_background_image_color(feet_slot, item.itemRarity);
            }
            else if (item.itemType == "hands") {
                get_item_image(hands_slot).sprite = item.itemIcon;
                set_item_background_image_color(hands_slot, item.itemRarity);
            }
            else if (item.itemType == "weapon") {
                get_item_image(weapon_slot).sprite = item.itemIcon;
                set_item_background_image_color(weapon_slot, item.itemRarity);
            }
            else if (item.itemType == "ring") {
                get_item_image(ring_slot).sprite = item.itemIcon;
                set_item_background_image_color(ring_slot, item.itemRarity);
            }
        }

        
        
    }


    //get value
    public bool getIsOpened() {
        return opened;
    }

    //create string with item stats
    private string create_item_stats_text(Item item) {
        string text = "";

        //check each stat if it is not 0 and add it to text
        if (item.damage != 0) {
            text += "Damage: " + item.damage + "\n";
        }
        if (item.armor != 0) {
            text += "Armor: " + item.armor + "\n";
        }
        if (item.health != 0) {
            text += "Health: " + item.health + "\n";
        }
        if (item.strength != 0) {
            text += "Strength: " + item.strength + "\n";
        }
        if (item.agility != 0) {
            text += "Agility: " + item.agility + "\n";
        }
        if (item.intelligence != 0) {
            text += "Intelligence: " + item.intelligence + "\n";
        }
        if (item.crit_chance != 0) {
            text += "Crit chance: " + item.crit_chance + "\n";
        }
        if (item.moves != 0) {
            text += "Moves: " + item.moves + "\n";
        }

        return text;
    }

    //open new item recieve window
    public void open_new_item_panel(Item new_item) {
        //set new item name text
        new_item_name_text.SetText(new_item.itemName);
        //set new item image
        new_item_image.sprite = new_item.itemIcon;
        //set new item background image color
        set_item_background_image_color(new_item_image.gameObject.transform.parent.gameObject, new_item.itemRarity);
        //set new item description text
        new_item_description_text.SetText(new_item.itemDescription);
        //set new item stats text
        new_item_stats_text.SetText(create_item_stats_text(new_item));

        //set current new item
        current_new_item = new_item;

        activateNewItemPanel(true);
    }

    //close new item recieve window
    public void close_new_item_panel() {
        activateNewItemPanel(false);
    }

    //activate/disactivate new item panel
    public void activateNewItemPanel(bool value) {
        new_item_panel.SetActive(value);

        //get inventory button
        Button inventoryButton = GameObject.FindGameObjectsWithTag("inventory_button")[0].GetComponent<Button>();

        //get finish turn button
        Button finishTurnButton = GameObject.FindGameObjectsWithTag("finish_turn_button")[0].GetComponent<Button>();

        //set buttons interactable to opposite value
        inventoryButton.interactable = !value;
        finishTurnButton.interactable = !value;

        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //set enable movement to opposite value
        playerTurns.enableMovement(!value);
    }

    //get old item
    public Item get_old_item_of_type(string new_item_type, GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //check if there is already item of this type equipped
        foreach (Item item in playerStats.get_items()) {
            if (item.itemType == new_item_type) {
                return item;
            }
        }

        return null;

    }

    //equip item from new item panel
    public void equip_new_item() {
        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get current player object
        GameObject player = playerTurns.getCurrentPlayer();

        //get current new item
        Item item = get_current_new_item();

        //unequip old item if it exists
        if (get_old_item_of_type(item.itemType, player) != null) {
            get_old_item_of_type(item.itemType, player).unequipItem(player);
        }

        //equip item
        item.equipItem(player);


        //close new item panel
        close_new_item_panel();
    }

}
