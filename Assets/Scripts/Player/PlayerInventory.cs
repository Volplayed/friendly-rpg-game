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

    public TMP_Text healthText, damageText, armorText, strengthText, agilityText, intelligenceText,
    levelText, expText, movesText, healText, critChanceText;

    public GameObject panel;
    private GameObject player;
    private PlayerTurns playerTurns;
    private GameObject levelUpPanel;
    //item data panel
    public GameObject itemDataPanel;

    //item data panel texts
    public TMP_Text itemNameText, itemDescriptionText, itemStatsText, itemRarityText;

    //new item recieve panel
    public GameObject newItemPanel;

    //new item recieve panel texts and images
    public TMP_Text newItemNameText, newItemDescriptionText, newItemStatsText;
    public Image newItemImage, newItemBackgroundImage;

    //new item recieve panel old item panel texts and images
    public TMP_Text oldItemNameText, oldItemDescriptionText, oldItemStatsText;
    public Image oldItemImage, oldItemBackgroundImage;

    //current new item
    private Item currentNewItem;

     void Start()
    {
        playerTurns = gameObject.GetComponent<PlayerTurns>();

        //get level up panel
        levelUpPanel = playerTurns.getLevelUpPanel();
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
            playerTurns.enableMovement(false);

            //set inventory stats
            setStats();

            //set inventory items
            setItems();
        }
        //if inventory is opened
        else {
            //clear all item slots
            clearItemSlots();

            //close inventory window
            panel.SetActive(false);
            opened = false;

            //deactivate item data panel
            deactivateItemDataPanel();

            //enable movement after delay
            playerTurns.enableMovementAfterDelay();

        }

    }

    private void setStats() {
        //get player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();

        //set stats
        healthText.SetText("Health: " + stats.getHealth());
        damageText.SetText("Damage: " + stats.getDamage());
        armorText.SetText("Armor: " + stats.getArmor() * Coefficient.armor);
        strengthText.SetText("Strength: " + stats.getStrength());
        agilityText.SetText("Agility: " + stats.getAgility());
        intelligenceText.SetText("Intelligence: " + stats.getIntelligence());
        expText.SetText("Experience: " + stats.getExp() + "/" + stats.getNeededExp());
        critChanceText.SetText("Crit chance: " + stats.getCrit() * 100 + "%");
        movesText.SetText("Moves: " + stats.getMoves());
        healText.SetText("Heal: " + stats.getHeal());
        levelText.SetText("Level: " + stats.getLevel());
    }

    //get current new item
    public Item getCurrentNewItem() {
        return currentNewItem;
    }

    //find item image component
    private Image getItemImage(GameObject slot) {
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
    private Image getItemBackgroundImage(GameObject slot) {
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
    private void setItemBackgroundImageColor(GameObject slot, int rarity) {
        //get item background image
        Image image = getItemBackgroundImage(slot);

        //common
        if (rarity == 0) {
            //light grey
            image.color = new Color(0.85f, 0.85f, 0.85f, 1f);
        }
        //uncommon
        else if (rarity == 1) {
            //green
            image.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        }
        //rare
        else if (rarity == 2) {
            //blue
            image.color = new Color(0f, 0.75f, 1f, 1f);
        }
        //epic
        else if (rarity == 3) {
            //purple
            image.color = new Color(0.58f, 0f, 0.82f, 1f);
        }
        //legendary
        else if (rarity == 4) {
            //yellow
            image.color = new Color(1f, 1f, 0.1f, 1f);
        }
        //mythical
        else if (rarity == 5) {
            //orange
            image.color = new Color(1f, 0.55f, 0f, 1f);
        }
        //none
        else {
            //white
            image.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    //item rarity to string
    private string itemRarityToString(Item item) {
        //get item rarity
        int rarity = item.itemRarity;

        //common
        if (rarity == 0) {
            return "Common";
        }
        //uncommon
        else if (rarity == 1) {
            return "Uncommon";
        }
        //rare
        else if (rarity == 2) {
            return "Rare";
        }
        //epic
        else if (rarity == 3) {
            return "Epic";
        }
        //legendary
        else if (rarity == 4) {
            return "Legendary";
        }
        //mythical
        else if (rarity == 5) {
            return "Mythical";
        }
        return "";
    }

    //clear item slots
    private void clearItemSlots() {
        //get slots
        GameObject headSlot = GameObject.FindGameObjectsWithTag("head_slot")[0];
        GameObject bodySlot = GameObject.FindGameObjectsWithTag("body_slot")[0];
        GameObject legSlot = GameObject.FindGameObjectsWithTag("leg_slot")[0];
        GameObject feetSlot = GameObject.FindGameObjectsWithTag("feet_slot")[0];
        GameObject handsSlot = GameObject.FindGameObjectsWithTag("hands_slot")[0];
        GameObject weaponSlot = GameObject.FindGameObjectsWithTag("weapon_slot")[0];
        GameObject ringSlot = GameObject.FindGameObjectsWithTag("ring_slot")[0];

        //get item images
        Image headImage = getItemImage(headSlot);
        Image bodyImage = getItemImage(bodySlot);
        Image legImage = getItemImage(legSlot);
        Image feetImage = getItemImage(feetSlot);
        Image handsImage = getItemImage(handsSlot);
        Image weaponImage = getItemImage(weaponSlot);
        Image ringImage = getItemImage(ringSlot);

        //clear item images
        headImage.sprite = null;
        bodyImage.sprite = null;
        legImage.sprite = null;
        feetImage.sprite = null;
        handsImage.sprite = null;
        weaponImage.sprite = null;
        ringImage.sprite = null;

        //clear item background images
        setItemBackgroundImageColor(headSlot, -1);
        setItemBackgroundImageColor(bodySlot, -1);
        setItemBackgroundImageColor(legSlot, -1);
        setItemBackgroundImageColor(feetSlot, -1);
        setItemBackgroundImageColor(handsSlot, -1);
        setItemBackgroundImageColor(weaponSlot, -1);
        setItemBackgroundImageColor(ringSlot, -1);

    }

    //set player items
    private void setItems() {
        //get player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();
        //get player items
        List<Item> items = stats.getItems();
       
        //get slots
        GameObject headSlot = GameObject.FindGameObjectsWithTag("head_slot")[0];
        GameObject bodySlot = GameObject.FindGameObjectsWithTag("body_slot")[0];
        GameObject legSlot = GameObject.FindGameObjectsWithTag("leg_slot")[0];
        GameObject feetSlot = GameObject.FindGameObjectsWithTag("feet_slot")[0];
        GameObject handsSlot = GameObject.FindGameObjectsWithTag("hands_slot")[0];
        GameObject weaponSlot = GameObject.FindGameObjectsWithTag("weapon_slot")[0];
        GameObject ringSlot = GameObject.FindGameObjectsWithTag("ring_slot")[0];

        //set item to each slot
        foreach (Item item in items) {
            if (item.itemType == "head") {
                getItemImage(headSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(headSlot, item.itemRarity);
            }
            else if (item.itemType == "body") {
                getItemImage(bodySlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(bodySlot, item.itemRarity);
            }
            else if (item.itemType == "leg") {
                getItemImage(legSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(legSlot, item.itemRarity);
            }
            else if (item.itemType == "feet") {
                getItemImage(feetSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(feetSlot, item.itemRarity);
            }
            else if (item.itemType == "hands") {
                getItemImage(handsSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(handsSlot, item.itemRarity);
            }
            else if (item.itemType == "weapon") {
                getItemImage(weaponSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(weaponSlot, item.itemRarity);
            }
            else if (item.itemType == "ring") {
                getItemImage(ringSlot).sprite = item.itemIcon;
                setItemBackgroundImageColor(ringSlot, item.itemRarity);
            }
        }

        
        
    }


    //get value
    public bool getIsOpened() {
        return opened;
    }

    //create string with item stats
    private string createItemStatsText(Item item) {
        string text = "";

        //check each stat if it is not 0 and add it to text
        if (item.damage != 0) {
            text += "Damage: " + item.damage + "\n";
        }
        if (item.armor != 0) {
            text += "Armor: " + item.armor * Coefficient.armor + "\n";
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
        if (item.critChance != 0) {
            text += "Crit chance: " + item.critChance * 100 + "%\n";
        }
        if (item.moves != 0) {
            text += "Moves: " + item.moves + "\n";
        }

        return text;
    }

    //open new item recieve window
    public void openNewItemPanel(Item newItem) {
        //set new item name text
        newItemNameText.SetText(newItem.itemName);
        //set new item image
        newItemImage.sprite = newItem.itemIcon;
        //set new item background image color
        setItemBackgroundImageColor(newItemImage.gameObject.transform.parent.gameObject, newItem.itemRarity);
        //set new item description text
        newItemDescriptionText.SetText(newItem.itemDescription);
        //set new item stats text
        newItemStatsText.SetText(createItemStatsText(newItem));

        //set current new item
        currentNewItem = newItem;

        activateNewItemPanel(true);
    }

    //close new item recieve window
    public void closeNewItemPanel() {
        activateNewItemPanel(false);
    }

    //discard new item
    public void discardNewItem() {
        //close new item panel
        closeNewItemPanel();

        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();


        //add items discarded to game statistics
        PlayerGameStatistics.addItemsDiscarded(playerTurns.getCurrentPlayerTurn() + 1);
    }

    //activate/disactivate new item panel
    public void activateNewItemPanel(bool value) {
        newItemPanel.SetActive(value);

        //get inventory button
        Button inventoryButton = GameObject.FindGameObjectsWithTag("inventory_button")[0].GetComponent<Button>();

        //get finish turn button
        Button finishTurnButton = GameObject.FindGameObjectsWithTag("finish_turn_button")[0].GetComponent<Button>();

        //set buttons interactable to opposite value
        //check if level up window is active
        if (levelUpPanel.activeSelf) {
            inventoryButton.interactable = false;
            finishTurnButton.interactable = false;
        }
        else {
            inventoryButton.interactable = !value;
            finishTurnButton.interactable = !value;
        }

        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //set enable movement to false if value is true
        if (value) {
            playerTurns.enableMovement(false);
        }
        //set enable movement to true after delay if value is false
        else {
            playerTurns.enableMovementAfterDelay();
        }

        //activate/deactivate old item panel if value is true
        if (value) {
            //get type of new item and current player and find appropriate old item
            activateOldItemPanel(getOldItemOfType(getCurrentNewItem().itemType, playerTurns.getCurrentPlayer()));
        }
    }

    //activate/disactivate and set old item panel depending on if there is old item equipped
    public void activateOldItemPanel(Item oldItem) {
        //get old item panel by getting parent of old item text
        GameObject oldItemPanel = oldItemNameText.gameObject.transform.parent.gameObject;
        //if there is old item activate and set old item panel
        if (oldItem != null) {
            //activate old item panel
            oldItemPanel.SetActive(true);
            //set old item name text
            oldItemNameText.SetText(oldItem.itemName);
            //set old item image
            oldItemImage.sprite = oldItem.itemIcon;
            //set old item background image color
            setItemBackgroundImageColor(oldItemImage.gameObject.transform.parent.gameObject, oldItem.itemRarity);
            //set old item description text
            oldItemDescriptionText.SetText(oldItem.itemDescription);
            //set old item stats text
            oldItemStatsText.SetText(createItemStatsText(oldItem));
        }
        //if there is no old item deactivate old item panel
        else {
            oldItemPanel.SetActive(false);
        }
    }

    //get old item
    public Item getOldItemOfType(string newItemType, GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //check if there is already item of this type equipped
        foreach (Item item in playerStats.getItems()) {
            if (item.itemType == newItemType) {
                return item;
            }
        }

        return null;

    }

    //get item by type
    public Item getItemOfType(string itemType, GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //check if there is already item of this type equipped
        foreach (Item item in playerStats.getItems()) {
            if (item.itemType == itemType) {
                return item;
            }
        }

        return null;

    }

    //equip item from new item panel
    public void equipNewItem() {
        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get current player object
        GameObject player = playerTurns.getCurrentPlayer();

        //get current new item
        Item item = getCurrentNewItem();

        //unequip old item if it exists
        if (getOldItemOfType(item.itemType, player) != null) {
            getOldItemOfType(item.itemType, player).unequipItem(player);
        }

        //add item equipped to game statistics
        PlayerGameStatistics.addItemsEquipped(playerTurns.getCurrentPlayerTurn() + 1);


        //equip item
        item.equipItem(player);


        //close new item panel
        closeNewItemPanel();
    }

    //activate item data panel
    private void activateItemDataPanel(Item item) {
        //set item name text
        itemNameText.SetText(item.itemName);
        //set item description text
        itemDescriptionText.SetText(item.itemDescription);
        //set item stats text
        itemStatsText.SetText(createItemStatsText(item));
        //set item rarity text
        itemRarityText.SetText(itemRarityToString(item));

        //activate item data panel
        itemDataPanel.SetActive(true);
    }

    //deactivate item data panel
    private void deactivateItemDataPanel() {
        itemDataPanel.SetActive(false);
    }

    //open item data panel on click on item in inventory and set item data panel
    public void openItemData(string type) {
        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];
        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //search for item in inventory with type
        if (getItemOfType(type, playerTurns.getCurrentPlayer()) != null) {
            //get item
            Item item = getItemOfType(type, playerTurns.getCurrentPlayer());

            //activate item data panel
            activateItemDataPanel(item);
        }
    }

}
