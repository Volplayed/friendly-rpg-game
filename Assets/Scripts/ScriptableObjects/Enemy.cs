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

    public double crit_chance;
    
    //actions
    public void attack(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        int value = damage;

        //crit
        if (UnityEngine.Random.Range(0f, 1) <= crit_chance) {
            value = Convert.ToInt32(value * 1.7);
        }
        stats.damageSelf(value);
    }

    public int heal() {
        int value = System.Convert.ToInt32(max_health*0.1);
        health += value;

        return value;
    }
    //damage recieved
    public int damageSelf(int value) {
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * 10));
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;

        return reduced_damage;
    }

    //enemy death
    public void check_death(GameObject player) {
        if (health <= 0) {
            die(player);
        }
    }

    private void die(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
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

    //set functions
    public void set_starting_health() {
        health = max_health;
    }



}
