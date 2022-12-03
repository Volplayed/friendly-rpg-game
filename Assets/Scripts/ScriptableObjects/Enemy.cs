using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public string enemyName;

    public int level;
    //stats
    public int max_health
    private int health;
    public int damage;
    public double armor;

    public double crit_chance;
    
    //actions
    public void attack(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        int value = damage;
        if (Random.Range(0f, 1) <= crit_chance) {
            value *= 1.7;
        }
        stats.damage(value);
    }

    public void heal() {
        int value = max_health*0.1;
        health += value;
    }

    public void damage(int value) {
        int reduced_damage = value * (100 - armor * 10);
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;
    }
    public void check_death() {
        if (health <= 0) {
            die();
        }
    }

    private void die(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.give_exp(level*2 + Random.Range(0, 3));
    }
}
