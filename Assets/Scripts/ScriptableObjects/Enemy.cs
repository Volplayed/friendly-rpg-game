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
    public int heal_value;

    [Range(0, 1)]
    public double critChance;
    
    //chance to drop item
    [Range(0, 1)]
    public double drop_chance;

    //drop items list
    public Item[] drop_items;

    //is a boss?
    public bool is_boss = false;

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
        int value = heal_value;
        health += value;
        
        return value;
    }
    //damage recieved
    public int damageSelf(int value) {
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * Coefficient.armor)/100);
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;

        return reduced_damage;
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
        if (!is_boss) {
            //drop item if drop chance is met
            if (UnityEngine.Random.Range(0f, 1) <= drop_chance) {
                //drop item and open new item window
                stats.openNewItemPanel(drop_item());
            }
            //give exp to player based on level and some additional random value
            stats.giveExp(level * Coefficient.expPerEnemyLevel + UnityEngine.Random.Range(0, 3));
        }
        //if enemy is a boss
        else if (is_boss) {
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
    public string getEnemy_name() {
        return enemyName;
    }
    public bool getHealed() {
        return healed;
    }
    public int getHeal() {
        return heal_value;
    }
    public double get_drop_chance() {
        return drop_chance;
    }
    public Item[] get_drop_items() {
        return drop_items;
    }
    public bool getIsBoss() {
        return is_boss;
    }

    //set functions
    public void set_starting_health() {
        health = maxHealth;
    }
    public void set_maxHealth(int value) {
        maxHealth = value;
    }
    public void set_damage(int value) {
        damage = value;
    }
    public void set_armor(double value) {
        armor = value;
    }
    public void set_critChance(double value) {
        critChance = value;
    }
    public void set_level(int value) {
        level = value;
    }
    public void set_name(string value) {
        enemyName = value;
    }
    public void set_healed(bool value) {
        healed = value;
    }
    public void set_heal(int value) {
        heal_value = value;
    }
    public void set_drop_chance(double value) {
        drop_chance = value;
    }
    public void set_drop_items(Item[] value) {
        drop_items = value;
    }
    public void set_is_boss(bool value) {
        is_boss = value;
    }

    //drop random item
    public Item drop_item() {
        //get length of list
        int n = drop_items.Length;
        //get random item
        Item item = drop_items[UnityEngine.Random.Range(0, n)];

        return item;
    }
}
