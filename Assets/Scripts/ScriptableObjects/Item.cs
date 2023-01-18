using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject {
    
    //item name
    public string itemName;
    //item description
    public string itemDescription;
    //item icon
    public Sprite itemIcon;
    //item type
    public string itemType;
    //item rarity
    [Range(0, 5)] //0 = common, 1 = uncommon, 2 = rare, 3 = epic, 4 = legendary, 5 = Mythic
    public int itemRarity;

    //item stats
    public int strength;
    public int agility;
    public int intelligence;

    public int health;
    public int damage;
    public double armor;
    public double critChance;
    public int moves;

    //equip item
    public void equipItem(GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //add item stats to player bonus stats
        playerStats.addBonusStrength(strength);
        playerStats.addBonusAgility(agility);
        playerStats.addBonusIntelligence(intelligence);
        playerStats.addBonusHealth(health);
        playerStats.addBonusDamage(damage);
        playerStats.addBonusArmor(armor);
        playerStats.addBonusCritChance(critChance);
        playerStats.addBonusMoves(moves);

        //add item to player inventory
        playerStats.addItem(this);
    }

    //unequip item
    public void unequipItem(GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //remove item stats from player bonus stats
        playerStats.removeBonusStrength(strength);
        playerStats.removeBonusAgility(agility);
        playerStats.removeBonusIntelligence(intelligence);
        playerStats.removeBonusHealth(health);
        playerStats.removeBonusDamage(damage);
        playerStats.removeBonusArmor(armor);
        playerStats.removeBonusCritChance(critChance);
        playerStats.removeBonusMoves(moves);

        //remove item from player inventory
        playerStats.removeItem(this);

        //destroy item
        Destroy(this);
    }
}
