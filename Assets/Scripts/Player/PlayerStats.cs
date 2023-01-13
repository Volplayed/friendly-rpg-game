using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //items
    private List<Item> items = new List<Item>();
    //starting item
    public Item starting_item;

    //for turn management
    private bool has_turn = false;
    private List<HexClickHandler> hexHandlers = new List<HexClickHandler>();

    //fight related values
    private bool in_fight = false;
    private Enemy enemy = null;
    private GameObject enemyPlayer = null;

    //can player be attacked
    private bool can_be_attacked = false;

    //can be attacked marker
    public GameObject can_be_attacked_marker;

    //fight marker
    public GameObject fight_marker;

    //level up panel
    private GameObject levelUpPanel;
    //levelup panel text
    private TMP_Text levelUpText;

    //final player data
    private bool did_win = false;
    private bool did_lose = false;

    void Start()
    {       
        //starting values
        strength = default_strength;
        agility = default_agility;
        intelligence = default_intelligence;
        level = 1;
        calculateStats();
        calculateExp();

        //add starting item
        starting_item.equipItem(gameObject);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get level up panel
        levelUpPanel = playerTurns.getLevelUpPanel();

        //get level up text list
        TMP_Text[] levelUpTexts = levelUpPanel.GetComponentsInChildren<TMP_Text>();

        //search for level up text
        foreach (TMP_Text text in levelUpTexts) {
            if (text.gameObject.tag == "level_text") {
                levelUpText = text;
            }
        }
    }

    //reset all colliders of hexes
    public void resetHexColliders() {
        foreach (HexClickHandler hex in hexHandlers) {
            hex.resetCollider();
        }
    }

    void calculateExp() {
        needed_exp = level * exp_multiplayer;
    }

    //set health
    public void set_health(int health_) {
        health = health_;
    }

    //set exp
    public void set_exp(int exp_) {
        exp = exp_;
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
    public int get_heal() {
        return System.Convert.ToInt32(intelligence / Coefficient.intelligencePerHeal) * Coefficient.healApplication + level;
    }
    public int get_moves() {
        return moves;
    }
    public bool get_in_fight() {
        return in_fight;
    }
    public bool get_can_be_attacked() {
        return can_be_attacked;
    }
    public List<Item> get_items() {
        return items;
    }
    public bool get_did_win() {
        return did_win;
    }
    public bool get_did_lose() {
        return did_lose;
    }

    //calculates max health with formula
    public int get_max_health() {
        return strength * Coefficient.healthPerStrength + bonus_health;
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

    //add to attributes with level up
    public void gain_strength() {
        default_strength += 1;

        //calculate new stats
        calculateStats();
    }
    public void gain_agility() {
        default_agility += 1;

        //calculate new stats
        calculateStats();
    }
    public void gain_intelligence() {
        default_intelligence += 1;

        //calculate new stats
        calculateStats();
    }

    //give exp
    public void give_exp(int value) {
        exp += value;

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player fight component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //create exp popup text in the middle of the screen
        playerFight.createExpGainPopUpText(playerUI.transform.position, value);

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
        armor = agility * Coefficient.armorPerAgility + bonus_armor;
        crit_chance = intelligence * Coefficient.critChancePerIntelligence + bonus_crit_chance;
        moves = agility / Coefficient.agilityPerMove + bonus_moves;
        //min moves value is 1, set 1 if less than 1
        if (moves < 1) {
            moves = 1;
        }
        //if moves is 1 and there are bonus moves, set moves to 1 plus bonus moves
        if (moves == 1 && bonus_moves > 0) {
            moves = 1 + bonus_moves;
        }

        damage = level * Coefficient.damagePerLevel + bonus_damage;

        //calculate exp
        calculateExp();

    }

    //levelUp
    private void checkExp() {
        if (exp >= needed_exp) {
            levelUp();
        }
    }

    //change player level + 1, reset exp and open levelUp menu
    public void levelUp() {
        //+level and reset exp
        level++;
        calculateExp();
        exp = 0;
        //set can_be_attacked to true
        can_be_attacked = true;

        //hide can be attacked maeker
        show_can_be_attacked_marker(false);

        //activate levelUp menu
        activateLevelUpPanel(true);

        //set level up text
        levelUpText.SetText("You achived level " + level);

    }

    //activate/disactivate levelUp panel
    public void activateLevelUpPanel(bool value) {
        levelUpPanel.SetActive(value);

        //get inventory button
        Button inventoryButton = GameObject.FindGameObjectsWithTag("inventory_button")[0].GetComponent<Button>();

        //get finish turn button
        Button finishTurnButton = GameObject.FindGameObjectsWithTag("finish_turn_button")[0].GetComponent<Button>();

        //set buttons interactable to opposite value
        inventoryButton.interactable = !value;
        finishTurnButton.interactable = !value;

        //get player ui
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //set enable movement to false if value is true
        if (value) {
            playerTurns.enableMovement(false);
        } 
        //set enable movement to true after delay if value is false
        else {
            playerTurns.enableMovementAfterDelay();
        }

    }



    //fight functions
    public int damageSelf(int value) {
        //overall damage
        int reduced_damage = System.Convert.ToInt32(value * (100 - armor * Coefficient.armor)/100);

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
        int heal = get_heal();

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

        //get playerFight
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //close fight panel
        fightPanel.SetActive(false);

        //set in-fight buttons iteractable to make them work when the other player turn starts
        playerFight.set_button_interactable(true);

        //enable player movement after delay if there are moves still left
        playerTurns.enableMovementAfterDelay();

        //hide fight marker
        show_fight_marker(false);
    }

    /////////////////////////////////////////////////////
    //show/hide fight marker
    public void show_fight_marker(bool value) {
        //get fight marker sprite renderer
        SpriteRenderer fight_marker_sprite = fight_marker.GetComponent<SpriteRenderer>();

        //hide or show fight marker
        fight_marker_sprite.enabled = value;

    }

    //show/hide can be attacked marker
    public void show_can_be_attacked_marker(bool value) {
        //get can be attacked marker sprite renderer
        SpriteRenderer can_be_attacked_marker_sprite = can_be_attacked_marker.GetComponent<SpriteRenderer>();

        //hide or show can be attacked marker
        can_be_attacked_marker_sprite.enabled = value;

    }

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

        //make fight buttons interactable after delay
        playerFight.set_button_interactable_after_delay();

        //disable player movement
        playerTurns.enableMovement(false);

        //show fight marker
        show_fight_marker(true);
    }

    public int attack(Enemy enemy) {
        //damage
        int value = damage;

        //set did last attack crit to false
        StaticValuesController.lastAttackCrit = false;

        //crit
        if (Random.Range(0f, 1) <= crit_chance) {
            value = System.Convert.ToInt32(value * 1.6);

            //set did last attack crit to true
            StaticValuesController.lastAttackCrit = true;
        }
        return enemy.damageSelf(value);
    }
    
    //get escape chance vs enemy
    public double get_escape_chance_vs_enemy() {
        //chance depends only on player intelligence or agility
        double escape_chance;
        int k; //or intelligence or agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        //k agility or intelligence * coefficient - enemy level and player level divercity * coefficient
        escape_chance = k*Coefficient.enemyStatsEscapeChance - (level - enemy.get_level()) * Coefficient.enemyLevelEscapeChance;
        //if to high chance
        if (escape_chance > 0.9) {
            escape_chance = 0.9;
        }
        //if to low chance
        else if (escape_chance < 0.01) {
            escape_chance = 0.01;
        }
        return escape_chance;
    }

    public bool escape() {
        //chance depends only on player intelligence or agility
        double escape_chance = get_escape_chance_vs_enemy();

        //result
        bool result;
        //success
        if (Random.Range(0f, 1) <= escape_chance) {
            result = true;
            
            //give player exp for escaping depending on enemy level plus random value
            give_exp(enemy.get_level() + Random.Range(0, 3));

            //finish fight
            finish_fight();
        }
        //fail
        else {
            result = false;

            //get player UI
            GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];
            
            //get playerFight component
            PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

            //enemy ai act 
            playerFight.act();
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

        //show other player fight marker
        other_player_stats.show_fight_marker(true);

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

        //make fight buttons interactable after delay
        playerFight.set_button_interactable_after_delay();

        //disable player movement
        playerTurns.enableMovement(false);

        //show fight marker
        show_fight_marker(true);
    }

    public int attack(GameObject player) {
        //get other player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();

        //set did last attack crit to false
        StaticValuesController.lastAttackCrit = false;

        int value = damage;
        if (Random.Range(0f, 1) <= crit_chance) {
            value = System.Convert.ToInt32(value * 1.7);

            //set did last attack crit to true
            StaticValuesController.lastAttackCrit = true;
        }
        return stats.damageSelf(value);
    }
    
    //get escape chance vs player
    public double get_escape_chance_vs_player(GameObject other_player) {
        //chance depends on player intelligence or agility and other player agility
        PlayerStats stats = other_player.GetComponent<PlayerStats>();

        double escape_chance;
        int k; //or intelligence or agility
        int other_agility = stats.get_agility(); //other player agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        escape_chance = k*Coefficient.playerStatsEscapeChance - other_agility* Coefficient.playerEnemyStatsEscapeChance;
        //if to high chance
        if (escape_chance > 0.85) {
            escape_chance = 0.85;
        }
        //if to low chance
        else if (escape_chance < 0.01) {
            escape_chance = 0.01;
        }
        return escape_chance;
    }

    public bool escape(GameObject other_player) {
        //chance depends on player intelligence or agility and other player agility
        double escape_chance = get_escape_chance_vs_player(other_player);
        
        //result
        bool result;

        //success
        if (Random.Range(0f, 1) <= escape_chance) {
            result = true;

            //get enemy player stats
            PlayerStats other_player_stats = other_player.GetComponent<PlayerStats>();

            //finish fight for enemy player
            other_player_stats.finish_fight();

            //give player exp for escaping depending on other player level plus random value
            give_exp(other_player_stats.get_level() + Random.Range(0, 3));  

            //finish fight for player
            finish_fight();  
        }
        //fail
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

    //die and lose 1 level and one max stat
    private void die() {
        level -= 1;
        
        //set exp to 0
        exp = 0;

        //decrease max stat
        //strength
        if (default_strength > default_agility && default_strength > default_intelligence) {
            default_strength -= 1;
        }
        //agility
        else if (default_agility > default_strength && default_agility > default_intelligence) {
            default_agility -= 1;
        }
        //intelligence
        else if (default_intelligence > default_strength && default_intelligence > default_agility) {
            default_intelligence -= 1;
        }
        //if all are equal decrease random stat
        else {
            int random_stat = Random.Range(0, 3);
            if (random_stat == 0) {
                default_strength -= 1;
            }
            else if (random_stat == 1) {
                default_agility -= 1;
            }
            else {
                default_intelligence -= 1;
            }
        }

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

        //finish fight if vs enemy that is not boss
        if (enemy != null && !enemy.get_is_boss()) {
            finish_fight();

            //set dead player can be attacked to false
            can_be_attacked = false;

            //show can be attacked marker
            show_can_be_attacked_marker(true);
        } 
        //if fighting vs enemy that is boss
        else if (enemy != null && enemy.get_is_boss()) {
            //set did player win to false and did lose to true and finish turn
            lose();

            //finish fight
            finish_fight();
        }
        //if fighting vs player give other player exp
        else if (enemyPlayer != null) {

            //get enemy player stats
            PlayerStats enemyPlayerStats = enemyPlayer.GetComponent<PlayerStats>();
            
            finish_fight();

            //enemy player finish fight
            enemyPlayerStats.finish_fight();

            //give exp to other player
            enemyPlayerStats.give_exp((level + 1) * Coefficient.expPerPlayerLevel);

            //set dead player can be attacked to false
            can_be_attacked = false;

            //show can be attacked marker
            show_can_be_attacked_marker(true);
        }
    }

    //items management
    //add item to inventory
    public void add_item(Item item) {
        //create item instance
        Item item_ = ScriptableObject.CreateInstance<Item>();

        //set values of instance based on example values
        item_.itemName = item.itemName;
        item_.itemDescription = item.itemDescription;
        item_.itemIcon = item.itemIcon;
        item_.itemType = item.itemType;
        item_.itemRarity = item.itemRarity;
        item_.strength = item.strength;
        item_.agility = item.agility;
        item_.intelligence = item.intelligence;
        item_.health = item.health;
        item_.damage = item.damage;
        item_.crit_chance = item.crit_chance;
        item_.armor = item.armor;
        item_.moves = item.moves;

        //add item to list
        items.Add(item_);

        //calculate new stats
        calculateStats();
    }
    //remove item from inventory
    public void remove_item(Item item) {
        //remove item from list
        items.Remove(item);

        //calculate new stats
        calculateStats();
    }

    //open new item panel
    public void open_new_item_panel(Item item) {
        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerInventory component
        PlayerInventory playerInventory = playerUI.GetComponent<PlayerInventory>();

        //open new item panel
        playerInventory.open_new_item_panel(item);
    }

    //after boss fight
    //set did win and did lose
    public void set_did_win(bool value) {
        did_win = value;
        did_lose = !value;
    }

    //player win
    public void win() {
        //set did win to true
        set_did_win(true);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //add player name to list of players that won
        StaticValuesController.winners.Add(player_name);

        //go to next turn
        playerTurns.next_turn();
    }

    //player lose
    public void lose() {
        //set did lose to true
        set_did_win(false);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        PlayerTurns playerTurns = playerUI.GetComponent<PlayerTurns>();

        //go to next turn
        playerTurns.next_turn();
    }
    
}   
