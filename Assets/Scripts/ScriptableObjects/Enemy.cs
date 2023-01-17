using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public string enemyName;

    public int level;
    //stats
    public int maxHealth;
    private int health;
    public int damage;
    public double armor;
    public int healValue;

    [Range(0, 1)]
    public double critChance;
    
    //chance to drop item
    [Range(0, 1)]
    public double dropChance;

    //drop items list
    public Item[] dropItems;

    //is a boss?
    public bool isBoss = false;

    //temp stats
    private bool healed = false; //had the enemy healed last turn?

    //actions
    public int attack(GameObject player) {
        //get player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();

        //set did last attack crit to false
        StaticValuesController.lastAttackCrit = false;

        int value = damage;

        //crit
        if (UnityEngine.Random.Range(0f, 1) <= critChance) {
            value = Convert.ToInt32(value * 1.6);

            //set did last attack crit to true
            StaticValuesController.lastAttackCrit = true;
        }
        //player damages
        return stats.damageSelf(value);
    }

    public int heal() {
        int value = healValue;
        health += value;
        
        return value;
    }
    //damage recieved
    public int damageSelf(int value) {
        int reducedDamage = System.Convert.ToInt32(value * (100 - armor * Coefficient.armor)/100);
        if (reducedDamage <= 0) {
            reducedDamage = 1;
        }
        health -= reducedDamage;

        return reducedDamage;
    }

    //enemy death
    public bool checkDeath(GameObject player) {
        if (health <= 0) {
            die(player);
            return true;
        }
        return false;
    }

    private void die(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        //finish fight
        stats.finishFight();

        //if enemy is not a boss
        if (!isBoss) {
            //drop item if drop chance is met
            if (UnityEngine.Random.Range(0f, 1) <= dropChance) {
                //drop item and open new item window
                stats.openNewItemPanel(dropItem());
            }
            //give exp to player based on level and some additional random value
            stats.giveExp(level * Coefficient.expPerEnemyLevel + UnityEngine.Random.Range(0, 3));
        }
        //if enemy is a boss
        else if (isBoss) {
            //make player win
            stats.win();
        }
    }

    //get functions
    public int getHealth() {
        return health;
    }
    public int getDamage() {
        return damage;
    }
    public double getArmor() {
        return armor;
    }
    public double getCritChance() {
        return critChance;
    }
    public int getMaxHealth() {
        return maxHealth;
    }
    public int getLevel() {
        return level;
    }
    public string getEnemyName() {
        return enemyName;
    }
    public bool getHealed() {
        return healed;
    }
    public int getHeal() {
        return healValue;
    }
    public double getDropChance() {
        return dropChance;
    }
    public Item[] getDropItems() {
        return dropItems;
    }
    public bool getIsBoss() {
        return isBoss;
    }

    //set functions
    public void setStartingHealth() {
        health = maxHealth;
    }
    public void setMaxHealth(int value) {
        maxHealth = value;
    }
    public void setDamage(int value) {
        damage = value;
    }
    public void setArmor(double value) {
        armor = value;
    }
    public void setCritChance(double value) {
        critChance = value;
    }
    public void setLevel(int value) {
        level = value;
    }
    public void setName(string value) {
        enemyName = value;
    }
    public void setHealed(bool value) {
        healed = value;
    }
    public void setHeal(int value) {
        healValue = value;
    }
    public void setDropChance(double value) {
        dropChance = value;
    }
    public void setDropItems(Item[] value) {
        dropItems = value;
    }
    public void setIsBoss(bool value) {
        isBoss = value;
    }

    //drop random item
    public Item dropItem() {
        //get length of list
        int n = dropItems.Length;
        //get random item
        Item item = dropItems[UnityEngine.Random.Range(0, n)];

        return item;
    }
}
