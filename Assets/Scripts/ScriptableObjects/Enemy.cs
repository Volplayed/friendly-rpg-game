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

    public double crit_chance;
    
    //temp stats
    private bool healed = false; //had the enemy healed last turn?

    //actions
    public void attack(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        int value = damage;

        //crit
        if (UnityEngine.Random.Range(0f, 1) <= crit_chance) {
            value = Convert.ToInt32(value * 1.7);
        }
        //player damages
        stats.damageSelf(value);
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

        //give exp to player based on level and some additional random value
        stats.give_exp(level*2 + UnityEngine.Random.Range(0, 3));
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
}
