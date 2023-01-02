using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public string enemyName;

    public int level;
    //stats
    public int max_health;
    private int health;
    public int damage;
    public double armor;
    public int heal_value;

    [Range(0, 1)]
    public double crit_chance;
    
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
        if (UnityEngine.Random.Range(0f, 1) <= crit_chance) {
            value = Convert.ToInt32(value * 1.7);

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
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * 10)/100);
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;

        return reduced_damage;
    }

    //enemy death
    public bool check_death(GameObject player) {
        if (health <= 0) {
            die(player);
            return true;
        }
        return false;
    }

    private void die(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        //finish fight
        stats.finish_fight();

        //if enemy is not a boss
        if (!is_boss) {
            //drop item if drop chance is met
            if (UnityEngine.Random.Range(0f, 1) <= drop_chance) {
                //drop item and open new item window
                stats.open_new_item_panel(drop_item());
            }
            //give exp to player based on level and some additional random value
            stats.give_exp(level*3 + UnityEngine.Random.Range(0, 5));
        }
        //if enemy is a boss
        else if (is_boss) {
            //make player win
            stats.win();
        }
    }

    //get functions
    public int get_health() {
        return health;
    }
    public int get_damage() {
        return damage;
    }
    public double get_armor() {
        return armor;
    }
    public double get_crit_chance() {
        return crit_chance;
    }
    public int get_max_health() {
        return max_health;
    }
    public int get_level() {
        return level;
    }
    public string get_enemy_name() {
        return enemyName;
    }
    public bool get_healed() {
        return healed;
    }
    public int get_heal() {
        return heal_value;
    }
    public double get_drop_chance() {
        return drop_chance;
    }
    public Item[] get_drop_items() {
        return drop_items;
    }
    public bool get_is_boss() {
        return is_boss;
    }

    //set functions
    public void set_starting_health() {
        health = max_health;
    }
    public void set_max_health(int value) {
        max_health = value;
    }
    public void set_damage(int value) {
        damage = value;
    }
    public void set_armor(double value) {
        armor = value;
    }
    public void set_crit_chance(double value) {
        crit_chance = value;
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
