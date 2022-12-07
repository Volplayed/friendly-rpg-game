using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private string player_name = "Player";

    //how to multiply exp needed each level
    public int exp_multiplayer = 10;


    //default attributes
    public int default_strength = 5, default_agility = 5, default_intelligence = 5;

    //main values
    private int health, damage, level = 1;
    private double armor;

    private int exp, needed_exp;
    private int strength, agility, intelligence;

    //additional values
    private double crit_chance;
    private int moves = 1;

    //bonus stats
    //main values
    private int bonus_health = 0, bonus_damage = 0;
    private double bonus_armor = 0;
    private int bonus_strength = 0, bonus_agility = 0, bonus_intelligence = 0;

    //additional values
    private double bonus_crit_chance = 0;
    private int bonus_moves = 0;

    //for turn management
    private bool has_turn = false;
    private List<HexClickHandler> hexHandlers = new List<HexClickHandler>();

    void Start()
    {
        
        //starting values
        strength = default_strength;
        agility = default_agility;
        intelligence = default_intelligence;
        level = 1;
        calculateStats();
        calculateExp();

    }

    void calculateExp() {
        needed_exp = level * exp_multiplayer;
    }

    //get functions
    public string get_player_name() {
        return player_name;
    }
    public int get_health() {
        return health;
    }
    public int get_damage() {
        return damage;
    }
    public double get_armor() {
        return armor;
    }
    public int get_strength() {
        return strength;
    }
    public int get_agility() {
        return agility;
    }
    public int get_intelligence() {
        return intelligence;
    }
    public int get_exp() {
        return exp;
    }
    public int get_needed_exp() {
        return needed_exp;
    }
    public int get_level() {
        return level;
    }
    public double get_crit() {
        return crit_chance;
    }
    public int get_moves() {
        return moves;
    }


    //add|remove bonus values
    public void add_bonus_strength(int bonus) {

        bonus_strength += bonus;

    } 
    public void remove_bonus_strength(int bonus) {

        bonus_strength -= bonus;

    } 
    public void add_bonus_agility(int bonus) {

        bonus_agility += bonus;

    } 
    public void remove_bonus_agility(int bonus) {

        bonus_agility -= bonus;

    } 
    public void add_bonus_intelligence(int bonus) {

        bonus_intelligence += bonus;

    } 
    public void remove_bonus_intelligence(int bonus) {

        bonus_intelligence -= bonus;

    } 
    public void add_bonus_health(int bonus) {

        bonus_health += bonus;

    } 
    public void remove_bonus_health(int bonus) {

        bonus_health -= bonus;

    } 
    public void add_bonus_damage(int bonus) {

        bonus_damage += bonus;

    } 
    public void remove_bonus_damage(int bonus) {

        bonus_damage -= bonus;

    } 
    public void add_bonus_armor(double bonus) {

        bonus_armor += bonus;

    } 
    public void remove_bonus_armor(double bonus) {

        bonus_armor -= bonus;

    } 
    public void add_bonus_crit_chance(double bonus) {

        bonus_crit_chance += bonus;

    } 
    public void remove_bonus_crit_chance(double bonus) {

        bonus_crit_chance -= bonus;

    } 
    public void add_bonus_moves(int bonus) {

        bonus_moves += bonus;

    } 
    public void remove_bonus_moves(int bonus) {

        bonus_moves -= bonus;

    }

    //give exp
    public void give_exp(int value) {
        exp += value;
        checkExp();
    }
    
    //set player turn
    public void setHasTurn(bool what) {
        hexHandlers = gameObject.GetComponent<PlayerMovement>().getHexesClickHandlers();
        //set turn for every hex
        for (int i = 0; i < 6; i++) {
            hexHandlers[i].setHasTurn(what);
            
        }
        has_turn = what;
    }

    void calculateStats() {
        //callculate attributes
        strength = default_strength + bonus_strength;
        agility = default_agility + bonus_agility;
        intelligence = default_intelligence + bonus_intelligence;

        //calculate other
        health = strength * 2 + bonus_health;
        armor = agility * 0.2 + bonus_armor;
        crit_chance = intelligence * 0.01 + bonus_crit_chance;
        moves = intelligence / 5 + bonus_moves;
        damage = level * 2 + bonus_damage;

    }

    //levelUp
    void checkExp() {
        if (exp >= needed_exp) {
            level++;
            calculateExp();
            exp = 0;
        }
    }


    //fight functions
    public int damageSelf(int value) {
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * 10));
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;

        return reduced_damage;
    }

    public int heal() {
        //health before healing
        int health_before = health;
        int max_health = strength * 2 + bonus_health;
        int heal = max_health * intelligence / 100;
        if (max_health - health >= heal) {
            health += heal;
        }
        else {
            health = max_health;
        }
        //difference of new health after heal and health before heal
        return health - health_before;
    }

    //vs enemy
    public void attack(Enemy enemy) {
        int value = damage;
        if (Random.Range(0f, 1) <= crit_chance) {
            value = System.Convert.ToInt32(value * 1.7);
        }
        enemy.damageSelf(value);
    }
    
    public bool escape() {
        //chance depends only on player intelligence or agility
        double escape_chance;
        int k; //or intelligence or agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        escape_chance = k*0.04;
        //if to high chance
        if (escape_chance > 0.85) {
            escape_chance = 0.85;
        } 
        //result
        bool result;
        if (Random.Range(0f, 1) <= escape_chance) {
            result = true;
        }
        else {
            result = false;
        }
        return result;
    }
    
    //vs player
    public void attack(GameObject player) {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        int value = damage;
        if (Random.Range(0f, 1) <= crit_chance) {
            value = System.Convert.ToInt32(value * 1.7);
        }
        stats.damageSelf(value);
    }
    
    public bool escape(GameObject player) {
        //chance depends on player intelligence or agility and other player agility
        PlayerStats stats = player.GetComponent<PlayerStats>();

        double escape_chance;
        int k; //or intelligence or agility
        int other_agility = stats.get_agility(); //other player agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        escape_chance = k*0.04 - other_agility*0.01;
        //if to high chance
        if (escape_chance > 0.85) {
            escape_chance = 0.85;
        } 
        //result
        bool result;
        if (Random.Range(0f, 1) <= escape_chance) {
            result = true;
        }
        else {
            result = false;
        }
        return result;
    }
}
