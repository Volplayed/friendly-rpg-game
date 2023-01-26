using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private string playerName = "Player";

    //how to multiply exp needed each level
    public int expMultiplayer = 10;


    //default attributes
    public int defaultStrength = 5, defaultAgility = 5, defaultIntelligence = 5;

    //main values
    private int health, damage, level = 1;
    private double armor;

    private int exp, neededExp;
    private int strength, agility, intelligence;

    //additional values
    private double critChance;
    private int moves = 1;

    //bonus stats
    //main values
    private int bonusHealth = 0, bonusDamage = 0;
    private double bonusArmor = 0;
    private int bonusStrength = 0, bonusAgility = 0, bonusIntelligence = 0;

    //additional values
    private double bonusCritChance = 0;
    private int bonusMoves = 0;

    //items
    private List<Item> items = new List<Item>();
    //starting item
    public Item startingItem;

    //for turn management
    private bool hasTurn = false;
    private List<HexClickHandler> hexHandlers = new List<HexClickHandler>();

    //fight related values
    private bool inFight = false;
    private Enemy enemy = null;
    private GameObject enemyPlayer = null;

    //can player be attacked
    private bool canBeAttacked = false;

    //can be attacked marker
    public GameObject canBeAttackedMarker;

    //fight marker
    public GameObject fightMarker;

    //level up panel
    private GameObject levelUpPanel;
    //levelup panel text
    private TMP_Text levelUpText;

    //final player data
    private bool didWin = false;
    private bool didLose = false;

    //player turns component
    private PlayerTurns playerTurns;

    void Start()
    {       
        //starting values
        strength = defaultStrength;
        agility = defaultAgility;
        intelligence = defaultIntelligence;
        level = 1;
        calculateStats();
        calculateExp();

        //add starting item
        startingItem.equipItem(gameObject);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get player turns component
        playerTurns = playerUI.GetComponent<PlayerTurns>();

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
        neededExp = level * expMultiplayer;
    }

    //set health
    public void setHealth(int health_) {
        health = health_;
    }

    //set exp
    public void setExp(int exp_) {
        exp = exp_;
    }

    //get functions
    public string getPlayerName() {
        return playerName;
    }
    public int getHealth() {
        return health;
    }
    public int getDamage() {
        return damage;
    }
    public double getArmor() {
        return armor;
    }
    public int getStrength() {
        return strength;
    }
    public int getAgility() {
        return agility;
    }
    public int getIntelligence() {
        return intelligence;
    }
    public int getExp() {
        return exp;
    }
    public int getNeededExp() {
        return neededExp;
    }
    public int getLevel() {
        return level;
    }
    public double getCrit() {
        return critChance;
    }
    public int getHeal() {
        return Formulas.calculateHealAmount(intelligence, level);
    }
    public int getMoves() {
        return moves;
    }
    public bool getInFight() {
        return inFight;
    }
    public bool getCanBeAttacked() {
        return canBeAttacked;
    }
    public List<Item> getItems() {
        return items;
    }
    public bool getDidWin() {
        return didWin;
    }
    public bool getDidLose() {
        return didLose;
    }

    //calculates max health with formula
    public int getMaxHealth() {
        return Formulas.calculateHealthPerStrength(strength) + bonusHealth;
    }

    public Enemy getEnemy() {
        return enemy;
    }

    public GameObject getEnemyPlayer() {
        return enemyPlayer;
    }


    //add|remove bonus values
    public void addBonusStrength(int bonus) {

        bonusStrength += bonus;

    } 
    public void removeBonusStrength(int bonus) {

        bonusStrength -= bonus;

    } 
    public void addBonusAgility(int bonus) {

        bonusAgility += bonus;

    } 
    public void removeBonusAgility(int bonus) {

        bonusAgility -= bonus;

    } 
    public void addBonusIntelligence(int bonus) {

        bonusIntelligence += bonus;

    } 
    public void removeBonusIntelligence(int bonus) {

        bonusIntelligence -= bonus;

    } 
    public void addBonusHealth(int bonus) {

        bonusHealth += bonus;

    } 
    public void removeBonusHealth(int bonus) {

        bonusHealth -= bonus;

    } 
    public void addBonusDamage(int bonus) {

        bonusDamage += bonus;

    } 
    public void removeBonusDamage(int bonus) {

        bonusDamage -= bonus;

    } 
    public void addBonusArmor(double bonus) {

        bonusArmor += bonus;

    } 
    public void removeBonusArmor(double bonus) {

        bonusArmor -= bonus;

    } 
    public void addBonusCritChance(double bonus) {

        bonusCritChance += bonus;

    } 
    public void removeBonusCritChance(double bonus) {

        bonusCritChance -= bonus;

    } 
    public void addBonusMoves(int bonus) {

        bonusMoves += bonus;

    } 
    public void removeBonusMoves(int bonus) {

        bonusMoves -= bonus;

    }

    //add to attributes with level up
    public void gainStrength() {
        defaultStrength += 1;

        //calculate new stats
        calculateStats();
    }
    public void gainAgility() {
        defaultAgility += 1;

        //calculate new stats
        calculateStats();
    }
    public void gainIntelligence() {
        defaultIntelligence += 1;

        //calculate new stats
        calculateStats();
    }

    //give exp
    public void giveExp(int value) {
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
        hasTurn = what;
    }

    private void calculateStats() {
        //callculate attributes
        strength = defaultStrength + bonusStrength;
        agility = defaultAgility + bonusAgility;
        intelligence = defaultIntelligence + bonusIntelligence;

        //calculate other
        health = getMaxHealth();
        armor = Formulas.calculateArmorPerAgility(agility) + bonusArmor;
        critChance = Formulas.calculateCritChancePerIntelligence(intelligence) + bonusCritChance;
        moves = Formulas.calculateMoves(agility) + bonusMoves;
        //min moves value is 1, set 1 if less than 1
        if (moves < 1) {
            moves = 1;
        }
        //if moves is 1 and there are bonus moves, set moves to 1 plus bonus moves
        if (moves == 1 && bonusMoves > 0) {
            moves = 1 + bonusMoves;
        }

        damage = Formulas.calculateDamagePerLevel(level) + bonusDamage;

        //calculate exp
        calculateExp();

    }

    //levelUp
    private void checkExp() {
        if (exp >= neededExp) {
            levelUp();
        }
    }

    //change player level + 1, reset exp and open levelUp menu
    public void levelUp() {
        //+level and reset exp
        level++;
        calculateExp();
        exp = 0;
        //set canBeAttacked to true
        canBeAttacked = true;

        //hide can be attacked maeker
        showCanBeAttackedMarker(false);

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
        int reducedDamage = Formulas.calculateReducedDamage(value, armor);

        //minimal damage
        if (reducedDamage <= 0) {
            reducedDamage = 1;
        }
        health -= reducedDamage;
        
        //check if player is dead
        checkDeath();

        return reducedDamage;
    }

    public int heal() {
        //health before healing
        int healthBefore = health;
        //max health
        int maxHealth = getMaxHealth();

        //heal amount
        int heal = getHeal();

        //set min health to 1
        if (heal < 1) {
            heal = 1;
        }
        Debug.Log("healed " + heal);
        if (maxHealth - health >= heal) {
            health += heal;
        }
        else {
            health = maxHealth;
        }

        

        //difference of new health after heal and health before heal
        return health - healthBefore;
    }

    public void finishFight() {
        //set fight values
        enemy = null;
        enemyPlayer = null;
        inFight = false;

        //fully heal player
        health = getMaxHealth();

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerFight
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //close fight panel
        fightPanel.SetActive(false);

        //set in-fight buttons iteractable to make them work when the other player turn starts
        playerFight.setButtonIteractable(true);

        //enable player movement after delay if there are moves still left
        playerTurns.enableMovementAfterDelay();

        //hide fight marker
        showFightMarker(false);
    }

    /////////////////////////////////////////////////////
    //show/hide fight marker
    public void showFightMarker(bool value) {
        //get fight marker sprite renderer
        SpriteRenderer fightMarkerSprite = fightMarker.GetComponent<SpriteRenderer>();

        //hide or show fight marker
        fightMarkerSprite.enabled = value;

    }

    //show/hide can be attacked marker
    public void showCanBeAttackedMarker(bool value) {
        //get can be attacked marker sprite renderer
        SpriteRenderer canBeAttackedMarker_sprite = canBeAttackedMarker.GetComponent<SpriteRenderer>();

        //hide or show can be attacked marker
        canBeAttackedMarker_sprite.enabled = value;

    }

    //vs enemy

    public void startFight(Enemy enemy_) {
        //set fight values
        inFight = true;
        enemy = enemy_;

         //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerFigth component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //open fight panel and set values
        fightPanel.SetActive(true);
        playerFight.setValuesStart();

        //make fight buttons interactable after delay
        playerFight.setButtonInteractableAfterDelay();

        //disable player movement
        playerTurns.enableMovement(false);

        //show fight marker
        showFightMarker(true);
    }

    public int attack(Enemy enemy) {
        //damage
        int value = damage;

        //set did last attack crit to false
        StaticValuesController.lastAttackCrit = false;

        //crit
        if (Random.Range(0f, 1) <= critChance) {
            value = System.Convert.ToInt32(value * 1.6);

            //set did last attack crit to true
            StaticValuesController.lastAttackCrit = true;
        }
        return enemy.damageSelf(value);
    }
    
    //get escape chance vs enemy
    public double getEscapeChanceVsEnemy() {
        //chance depends only on player intelligence or agility
        double escapeChance;
        int k; //or intelligence or agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        //k agility or intelligence * coefficient - enemy level and player level divercity * coefficient
        escapeChance = Formulas.calculateEscapeChanceVsEnemy(k, level, enemy.getLevel());
        //if to high chance
        if (escapeChance > 0.9) {
            escapeChance = 0.9;
        }
        //if to low chance
        else if (escapeChance < 0.01) {
            escapeChance = 0.01;
        }
        return escapeChance;
    }

    public bool escape() {
        //chance depends only on player intelligence or agility
        double escapeChance = getEscapeChanceVsEnemy();

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];
            
        //result
        bool result;
        //success
        if (Random.Range(0f, 1) <= escapeChance) {
            result = true;
            
            //give player exp for escaping depending on enemy level plus random value
            giveExp(enemy.getLevel() * Coefficient.escapeExpPerEnemyLevel + Random.Range(0, 3));

            //add escape to game statistics
            PlayerGameStatistics.addFightsEscaped(playerTurns.getCurrentPlayerTurn() + 1);

            //finish fight
            finishFight();
        }
        //fail
        else {
            result = false;

            
            //get playerFight component
            PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

            //enemy ai act 
            playerFight.act();
        }
        return result;
    }
    ///////////////////////////////////////////////////////////////////
    //vs player
    public void setFightVsPlayer(GameObject otherPlayer) {
        //set fight values
        enemyPlayer = otherPlayer;
        inFight = true;
    }

    public void startFight(GameObject otherPlayer) {
        //set fight values
        inFight = true;
        enemyPlayer = otherPlayer;

        //get other player stats
        PlayerStats otherPlayerStats = otherPlayer.GetComponent<PlayerStats>();

        //set fight values for other player
        otherPlayerStats.setFightVsPlayer(gameObject);

        //show other player fight marker
        otherPlayerStats.showFightMarker(true);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerFigth component
        PlayerFight playerFight = playerUI.GetComponent<PlayerFight>();

        //get fight panel
        GameObject fightPanel = playerTurns.fightPanel;

        //open fight panel and set values
        fightPanel.SetActive(true);
        playerFight.setValuesStart();

        //make fight buttons interactable after delay
        playerFight.setButtonInteractableAfterDelay();

        //disable player movement
        playerTurns.enableMovement(false);

        //show fight marker
        showFightMarker(true);
    }

    public int attack(GameObject player) {
        //get other player stats
        PlayerStats stats = player.GetComponent<PlayerStats>();

        //set did last attack crit to false
        StaticValuesController.lastAttackCrit = false;

        int value = damage;
        if (Random.Range(0f, 1) <= critChance) {
            value = System.Convert.ToInt32(value * 1.7);

            //set did last attack crit to true
            StaticValuesController.lastAttackCrit = true;
        }
        return stats.damageSelf(value);
    }
    
    //get escape chance vs player
    public double getEscapeChanceVsPlayer(GameObject otherPlayer) {
        //chance depends on player intelligence or agility and other player agility
        PlayerStats stats = otherPlayer.GetComponent<PlayerStats>();

        double escapeChance;
        int k; //or intelligence or agility
        int otherAgility = stats.getAgility(); //other player agility
        if (intelligence > agility) {
            k = intelligence;
        }
        else {
            k = agility;
        }
        escapeChance = Formulas.calculateEscapeChanceVsPlayer(k, otherAgility);
        //if to high chance
        if (escapeChance > 0.85) {
            escapeChance = 0.85;
        }
        //if to low chance
        else if (escapeChance < 0.01) {
            escapeChance = 0.01;
        }
        return escapeChance;
    }

    public bool escape(GameObject otherPlayer) {
        //chance depends on player intelligence or agility and other player agility
        double escapeChance = getEscapeChanceVsPlayer(otherPlayer);
        
        //result
        bool result;

        //success
        if (Random.Range(0f, 1) <= escapeChance) {
            result = true;

            //get enemy player stats
            PlayerStats otherPlayerStats = otherPlayer.GetComponent<PlayerStats>();

            //finish fight for enemy player
            otherPlayerStats.finishFight();

            //give player exp for escaping depending on other player level plus random value
            giveExp(otherPlayerStats.getLevel() + Random.Range(0, 3));  

            //finish fight for player
            finishFight();  

            //get player UI
            GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];


            //add escape to game statistics
            PlayerGameStatistics.addFightsEscaped(playerTurns.getCurrentPlayerTurn() + 1);
        }
        //fail
        else {
            result = false;
        }
        return result;
    }

    //player lost fight
    public void checkDeath() {
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
        if (defaultStrength > defaultAgility && defaultStrength > defaultIntelligence) {
            defaultStrength -= 1;
        }
        //agility
        else if (defaultAgility > defaultStrength && defaultAgility > defaultIntelligence) {
            defaultAgility -= 1;
        }
        //intelligence
        else if (defaultIntelligence > defaultStrength && defaultIntelligence > defaultAgility) {
            defaultIntelligence -= 1;
        }
        //if all are equal decrease random stat
        else {
            int randomStat = Random.Range(0, 3);
            if (randomStat == 0) {
                defaultStrength -= 1;
            }
            else if (randomStat == 1) {
                defaultAgility -= 1;
            }
            else {
                defaultIntelligence -= 1;
            }
        }

        //minimal values
        if (level < 1) {
            level = 1;
        }
        if (defaultStrength < 1) {
            defaultStrength = 1;
        }
        if (defaultAgility < 1) {
            defaultAgility = 1;
        }
        if (defaultIntelligence < 1) {
            defaultIntelligence = 1;
        }
        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //add death to game statistics
        PlayerGameStatistics.addDeaths(playerTurns.getPlayerTurn(gameObject) + 1);

        //calculate new stats
        calculateStats();

        //finish fight if vs enemy that is not boss
        if (enemy != null && !enemy.getIsBoss()) {
            finishFight();

            //set dead player can be attacked to false
            canBeAttacked = false;

            //show can be attacked marker
            showCanBeAttackedMarker(true);
        } 
        //if fighting vs enemy that is boss
        else if (enemy != null && enemy.getIsBoss()) {
            //set did player win to false and did lose to true and finish turn
            lose();

            //finish fight
            finishFight();
        }
        //if fighting vs player give other player exp
        else if (enemyPlayer != null) {

            //get enemy player stats
            PlayerStats enemyPlayerStats = enemyPlayer.GetComponent<PlayerStats>();
            
            finishFight();

            //enemy player finish fight
            enemyPlayerStats.finishFight();

            //give exp to other player
            enemyPlayerStats.giveExp((level + 1) * Coefficient.expPerPlayerLevel);

            //set dead player can be attacked to false
            canBeAttacked = false;

            //show can be attacked marker
            showCanBeAttackedMarker(true);
        
            //give other player player kills to game statistics
            PlayerGameStatistics.addPlayersKilled(playerTurns.getCurrentPlayerTurn() + 1);
        }
    }

    //items management
    //add item to inventory
    public void addItem(Item item) {
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
        item_.critChance = item.critChance;
        item_.armor = item.armor;
        item_.moves = item.moves;

        //add item to list
        items.Add(item_);

        //calculate new stats
        calculateStats();
    }
    //remove item from inventory
    public void removeItem(Item item) {
        //remove item from list
        items.Remove(item);

        //calculate new stats
        calculateStats();
    }

    //open new item panel
    public void openNewItemPanel(Item item) {
        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //get playerInventory component
        PlayerInventory playerInventory = playerUI.GetComponent<PlayerInventory>();

        //open new item panel
        playerInventory.openNewItemPanel(item);
    }

    //after boss fight
    //set did win and did lose
    public void setDidWin(bool value) {
        didWin = value;
        didLose = !value;
    }

    //player win
    public void win() {
        //set did win to true
        setDidWin(true);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //add player name to list of players that won
        StaticValuesController.winners.Add(playerName);

        //add win to game statistics
        PlayerGameStatistics.addGamesWon(playerTurns.getCurrentPlayerTurn() + 1);

        //go to next turn
        playerTurns.nextTurn();
    }

    //player lose
    public void lose() {
        //set did lose to true
        setDidWin(false);

        //get player UI
        GameObject playerUI = GameObject.FindGameObjectsWithTag("player_ui")[0];
        
        //add lose to game statistics
        PlayerGameStatistics.addGamesLost(playerTurns.getCurrentPlayerTurn() + 1);

        //go to next turn
        playerTurns.nextTurn();
    }
    
}   
