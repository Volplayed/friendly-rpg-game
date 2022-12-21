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

    //fight related values
    private bool in_fight = false;
    private Enemy enemy = null;
    private GameObject enemyPlayer = null;

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
    public bool get_in_fight() {
        return in_fight;
    }
    //calculates max health with formula
    public int get_max_health() {
        return strength * 2 + bonus_health;
    }

    public Enemy get_enemy() {
        return enemy;
    }

    public GameObject get_enemy_player() {
        return enemyPlayer;
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

    private void calculateStats() {
        //callculate attributes
        strength = default_strength + bonus_strength;
        agility = default_agility + bonus_agility;
        intelligence = default_intelligence + bonus_intelligence;

        //calculate other
        health = get_max_health();
        armor = agility * 0.2 + bonus_armor;
        crit_chance = intelligence * 0.01 + bonus_crit_chance;
        moves = intelligence / 5 + bonus_moves;
        //min moves value is 1, set 1 if less than 1
        if (moves < 1) {
            moves = 1;
        }

        damage = level * 2 + bonus_damage;

    }

    //levelUp
    private void checkExp() {
        if (exp >= needed_exp) {
            level++;
            calculateExp();
            exp = 0;
        }
    }


    //fight functions
    public int damageSelf(int value) {
        //overall damage
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * 10)/100);

        //minimal damage
        if (reduced_damage <= 0) {
            reduced_damage = 1;
        }
        health -= reduced_damage;
        
        //check if player is dead
        check_death();

        return reduced_damage;
    }

    public int heal() {
        //health before healing
        int health_before = health;
        //max health
        int max_health = get_max_health();

        //heal amount
        int heal = System.Convert.ToInt32(intelligence / 3);

        //set min health to 1
        if (heal < 1) {
            heal = 1;
        }
        Debug.Log("healed " + heal);
        if (max_health - health >= heal) {
            health += heal;
        }
        else {
            health = max_health;
        }
        //difference of new health after heal and health before heal
        return health - health_before;
    }

    public void finish_fight() {
        //set fight values
        enemy = null;
        enemyPlayer = null;
        in_fight = false;

        //fully heal player
        health = get_max_health();

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //close fight panel
        fightPanel.SetActive(false);

        //enable player movement if there are moves still left
        if (playerTurns.moves_left() >= 1) {
            playerTurns.enableMovement(true);
        }
        
    }

    /////////////////////////////////////////////////////
    //vs enemy

    public void start_fight(Enemy enemy_) {
        //set fight values
        in_fight = true;
        enemy = enemy_;

         //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get playerFigth component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //open fight panel and set values
        fightPanel.SetActive(true);
        playerFight.setValuesStart();

        //disable player movement
        playerTurns.enableMovement(false);
    }

    public void attack(Enemy enemy) {
        int value = damage;
        //crit
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
    ///////////////////////////////////////////////////////////////////
    //vs player
    public void set_figth_vs_player(GameObject other_player) {
        //set fight values
        enemyPlayer = other_player;
        in_fight = true;
    }

    public void start_fight(GameObject other_player) {
        //set fight values
        in_fight = true;
        enemyPlayer = other_player;

        //get other player stats
        PlayerStats other_player_stats = other_player.GetComponent<PlayerStats>();

        //set fight values for other player
        other_player_stats.set_figth_vs_player(gameObject);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get playerFigth component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //open fight panel and set values
        fightPanel.SetActive(true);
        playerFight.setValuesStart();

        //disable player movement
        playerTurns.enableMovement(false);
    }

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

    //player lost fight
    public void check_death() {
        //if health less then zero
        if (health <= 0) {
            die();
        }
    }

    //die and lose 1 level and one of each stat
    private void die() {
        level -= 1;
        default_strength -= 1;
        default_agility -= 1;
        default_intelligence -= 1;

        //minimal values
        if (level < 1) {
            level = 1;
        }
        if (default_strength < 1) {
            default_strength = 1;
        }
        if (default_agility < 1) {
            default_agility = 1;
        }
        if (default_intelligence < 1) {
            default_intelligence = 1;
        }
        //calculate new stats
        calculateStats();

        //finish fight
        finish_fight();

        //if fighting vs player give other player exp
        if (enemyPlayer != null) {
            //get enemy player stats
            PlayerStats enemyPlayerStats = enemyPlayer.GetComponent<PlayerStats>();

            //give exp to other player
            enemyPlayerStats.give_exp(level*3);
        }
    }
}
