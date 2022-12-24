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
    public double crit_chance;
    public int moves;

    //equip item
    public void equipItem(GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //add item stats to player bonus stats
        playerStats.add_bonus_strength(strength);
        playerStats.add_bonus_agility(agility);
        playerStats.add_bonus_intelligence(intelligence);
        playerStats.add_bonus_health(health);
        playerStats.add_bonus_damage(damage);
        playerStats.add_bonus_armor(armor);
        playerStats.add_bonus_crit_chance(crit_chance);
        playerStats.add_bonus_moves(moves);
    }

    //dequip item
    public void dequipItem(GameObject player) {
        //get player stats
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //remove item stats from player bonus stats
        playerStats.remove_bonus_strength(strength);
        playerStats.remove_bonus_agility(agility);
        playerStats.remove_bonus_intelligence(intelligence);
        playerStats.remove_bonus_health(health);
        playerStats.remove_bonus_damage(damage);
        playerStats.remove_bonus_armor(armor);
        playerStats.remove_bonus_crit_chance(crit_chance);
        playerStats.remove_bonus_moves(moves);

        //destroy item
        Destroy(this);
    }
}
