using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFight : MonoBehaviour
{
    //player related objects
    public GameObject playerHealthBar;

    public TMP_Text playerHealthText;

    public TMP_Text playerNameText;

    //enemy related objects
    public GameObject enemyHealthBar;

    public TMP_Text enemyHealthText;

    public TMP_Text enemyNameText;

    //button objects
    public GameObject attackButton;
    public GameObject healButton;
    public GameObject retreatButton;
    public GameObject endTurnButton;
    public GameObject skipTurnButton;

    //popUp text prefab
    public GameObject popUpTextPrefab;

    //player turns
    private PlayerTurns playerTurns;

    //default colors
    private Color damageColor = new Color(1f, 0f, 0f, 1f); //red
    private Color healColor = new Color(0f, 1f, 0f, 1f); //green
    private Color critDamageColor = new Color(1f, 0f, 0.5f, 1f); //pink
    private Color expGainColor = new Color(1f, 1f, 1f, 1f); //white
    private Color escapeSuccessColor = new Color(0f, 1f, 0.5f, 1f); //light green
    private Color escapeFailColor = new Color(1f, 0.5f, 0f, 1f); //orange

    void Start() {
        //get playerTurns
        playerTurns = GetComponent<PlayerTurns>();
    }

    //set values healths bars and texts, names depending on players current turn at the start of a fight
    public void setValuesStart() {
        //get current player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //get sliders
        Slider slider = playerHealthBar.GetComponent<Slider>();
        Slider enemySlider = enemyHealthBar.GetComponent<Slider>();

        //set player health to max
        playerStats.setHealth(playerStats.getMaxHealth());

        //set values of current player
        slider.maxValue = playerStats.getMaxHealth();
        slider.value = playerStats.getHealth();

        playerHealthText.SetText(playerStats.getHealth() + "/" + playerStats.getMaxHealth());
        playerNameText.SetText(playerStats.getPlayerName());

        //if fighting with enemy not player
        if (playerStats.getEnemy() != null) {
            //get enemy
            Enemy enemy = playerStats.getEnemy();

            //set enemy health to max
            enemy.set_starting_health();

            //set enemy values
            enemySlider.maxValue = enemy.getMaxHealth();
            enemySlider.value = enemy.getHealth();

            enemyHealthText.SetText(enemy.getHealth() + "/" + enemy.getMaxHealth());
            enemyNameText.SetText(enemy.getEnemy_name());
        }
        //if fighting with player
        else if (playerStats.getEnemyPlayer() != null) {
            //get enemy player
            GameObject otherPlayer = playerStats.getEnemyPlayer();
            PlayerStats otherPlayerStats = otherPlayer.GetComponent<PlayerStats>();

            //set enemy player values
            enemySlider.maxValue = otherPlayerStats.getMaxHealth();
            enemySlider.value = otherPlayerStats.getHealth();

            enemyHealthText.SetText(otherPlayerStats.getHealth() + "/" + otherPlayerStats.getMaxHealth());
            enemyNameText.SetText(otherPlayerStats.getPlayerName());
        }

    }
    //set values of healths bars and texts, names depending on players current turn
    public void setValues() {
        //get current player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        //get sliders
        Slider slider = playerHealthBar.GetComponent<Slider>();
        Slider enemySlider = enemyHealthBar.GetComponent<Slider>();

        //set values of current player
        slider.maxValue = playerStats.getMaxHealth();
        slider.value = playerStats.getHealth();

        playerHealthText.SetText(playerStats.getHealth() + "/" + playerStats.getMaxHealth());
        playerNameText.SetText(playerStats.getPlayerName());

        //if fighting with enemy not player
        if (playerStats.getEnemy() != null) {
            //get enemy
            Enemy enemy = playerStats.getEnemy();;

            //set enemy values
            enemySlider.maxValue = enemy.getMaxHealth();
            enemySlider.value = enemy.getHealth();

            enemyHealthText.SetText(enemy.getHealth() + "/" + enemy.getMaxHealth());
            enemyNameText.SetText(enemy.getEnemy_name());
        }
        //if fighting with player
        else if (playerStats.getEnemyPlayer() != null) {
            //get enemy player
            GameObject otherPlayer = playerStats.getEnemyPlayer();
            PlayerStats otherPlayerStats = otherPlayer.GetComponent<PlayerStats>();

            //set enemy player values
            enemySlider.maxValue = otherPlayerStats.getMaxHealth();
            enemySlider.value = otherPlayerStats.getHealth();

            enemyHealthText.SetText(otherPlayerStats.getHealth() + "/" + otherPlayerStats.getMaxHealth());
            enemyNameText.SetText(otherPlayerStats.getPlayerName());
        }

    }

    //make buttons interactable or not
    public void setButtonIteractable(bool interactable) {
        //make buttons interactable or not
        attackButton.GetComponent<Button>().interactable = interactable;
        healButton.GetComponent<Button>().interactable = interactable;
        retreatButton.GetComponent<Button>().interactable = interactable;

        //show (hide) end turn button if skip turn button hidden (shown)
        skipTurnButton.SetActive(interactable);
        endTurnButton.SetActive(!interactable);
    }

    //make buttons interactable after delay
    public void setButtonInteractableAfterDelay(float delay = 0.3f) {
        //start coroutine
        StartCoroutine(setButtonInteractableAfterDelay_coroutine(delay));
    }
    //make buttons interactable after delay coroutine
    private IEnumerator setButtonInteractableAfterDelay_coroutine(float delay) {
        //wait for delay
        yield return new WaitForSeconds(delay);

        //make buttons interactable
        setButtonIteractable(true);
    }

    //buttons functions
    public void player_attack() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //attack enemy
        if (playerStats.getEnemy() != null) {
            //get enemy
            Enemy enemy = playerStats.getEnemy();
            //attack enemy and get damage dealt and create popup text with value of damage dealt
            //position of popup text is position of enemy health bar
            createDamagePopUpText(enemyHealthBar.transform.position, playerStats.attack(enemy));

            //set changed values
            setValues();

            
            
            //make buttons not interactable
            setButtonIteractable(false);

            //check if enemy did not died
            if (!enemy.checkDeath(player)) {
                //if it is an enemy ai act
                act();
            }
        }
        //attack player
        else if (playerStats.getEnemyPlayer() != null) {
            //get enemy player
            GameObject otherPlayer = playerStats.getEnemyPlayer();
            PlayerStats otherPlayerStats = otherPlayer.GetComponent<PlayerStats>();

            //attack enemy and get damage dealt and create popup text with value of damage dealt
            //position of popup text is position of enemy health bar
            createDamagePopUpText(enemyHealthBar.transform.position, playerStats.attack(otherPlayer));

            //make buttons not interactable
            setButtonIteractable(false);

            //set changed values
            setValues();
        }
        
    }

    public void player_heal() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //heal player and get heal amount and create popup text with value of heal amount
        //position of popup text is position of player health bar
        createHealPopUpText(playerHealthBar.transform.position, playerStats.heal());

        //set changed values
        setValues();

        //make buttons not interactable
        setButtonIteractable(false);

        //if it is an enemy ai act
        act();
    }

    public void player_escape() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        
        //make buttons not interactable
        setButtonIteractable(false);
        //try to escape
        //if vs enemy player
        if (playerStats.getEnemyPlayer() != null) {
            //get escape chance vs player
            double escapeChance = playerStats.getEscapeChanceVsPlayer(playerStats.getEnemyPlayer());

            //escape from enemy player and create popup text with value of escape
            //position of popup text is position of escape button
            createEscapePopUpText(
                retreatButton.transform.position,
                playerStats.escape(playerStats.getEnemyPlayer()),
                escapeChance //escape chance vs player
                );
            
        }
        //if vs enemy ai and is not boss
        else if (playerStats.getEnemy() != null && !playerStats.getEnemy().getIsBoss()) {
            //get escape chance vs enemy
            double escapeChance = playerStats.getEscapeChanceVsEnemy();

            //escape from enemy ai and create popup text with value of escape
            //position of popup text is position of escape button
            createEscapePopUpText(
                retreatButton.transform.position,
                playerStats.escape(),
                escapeChance //escape chance vs enemy
                );
        }
        //if vs enemy ai and is boss
        else if (playerStats.getEnemy() != null && playerStats.getEnemy().getIsBoss()) {
            //create unavaliable to escape popup text
            //position of popup text is position of escape button
            createEscapeUnavailablePopUpText(retreatButton.transform.position);

            //make buttons interactable without delay
            setButtonIteractable(true);
        }
        
    }

    //skip turn
    public void player_skip_turn() {
        //make buttons not interactable
        setButtonIteractable(false);

        //if it is an enemy ai act
        act();
    }

    //enemy ai functions
    //act
    public void act(float delay=0.1f) {
        //start coroutine
        StartCoroutine(actCouroutine(delay));
    }

    //act coroutine
    public IEnumerator actCouroutine(float delay) {
        //wait for delay
        yield return new WaitForSeconds(delay);

        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //if enemy is ai not other player
        if (playerStats.getEnemy() != null) {
            //get enemy
            Enemy enemy = playerStats.getEnemy();

            //attack if enought hp or already healed
            if (enemy.getHealth() >= enemy.getMaxHealth() * 0.5 || enemy.getHealed()) {               
                //attack player and get damage dealt and create popup text with value of damage dealt
                //position of popup text is position of player health bar
                createDamagePopUpText(playerHealthBar.transform.position, enemy.attack(player));

                //set changed values
                setValues();

                //set healed to false
                enemy.set_healed(false);
            }
            else if (enemy.getHealth() < enemy.getMaxHealth() * 0.5 && !enemy.getHealed()) {
                //create heal popup text with value of heal amount
                //position of popup text is position of enemy health bar
                createHealPopUpText(enemyHealthBar.transform.position, enemy.heal());

                //set changed values
                setValues();

                //set healed to true
                enemy.set_healed(true);
            }
        }
    }

    //popup functions
    //damage popUp text create function for 
    public PopUpText createDamagePopUpText(Vector3 position, int value)
    {
        //get playerUi
        GameObject playerUi = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //instantiate popUpText prefab in playerUi
        Transform PopUpTransform = Instantiate(popUpTextPrefab.transform, position, Quaternion.identity, playerUi.transform);

        //get script component
        PopUpText popUp = PopUpTransform.GetComponent<PopUpText>();

        //setup popUp
        //if last attack did critical damage
        if (StaticValuesController.lastAttackCrit) {
            //setup popup with crit damage color
            popUp.Setup(value, critDamageColor);
        }
        else {
            //setup popup with normal damage color
            popUp.Setup(value, damageColor);
        }

        return popUp;
    }

    //heal popUp text create function
    public PopUpText createHealPopUpText(Vector3 position, int value)
    {
        //get playerUi
        GameObject playerUi = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //instantiate popUpText prefab in playerUi
        Transform PopUpTransform = Instantiate(popUpTextPrefab.transform, position, Quaternion.identity, playerUi.transform);

        //get script component
        PopUpText popUp = PopUpTransform.GetComponent<PopUpText>();

        //setup popUp
        popUp.Setup(value, healColor);

        return popUp;
    }

    //exp gain popUp text create function
    public PopUpText createExpGainPopUpText(Vector3 position, int value)
    {
        //get playerUi
        GameObject playerUi = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //instantiate popUpText prefab in playerUi
        Transform PopUpTransform = Instantiate(popUpTextPrefab.transform, position, Quaternion.identity, playerUi.transform);

        //get script component
        PopUpText popUp = PopUpTransform.GetComponent<PopUpText>();

        //exp string
        string expString = "EXP +" + value.ToString();

        //setup popUp to disappear after 2 seconds
        popUp.Setup(expString, expGainColor, 2f);

        return popUp;
    }

    //escape success popUp text create function
    public PopUpText createEscapePopUpText(Vector3 position, bool escaped, double escapeChance)
    {
        //get playerUi
        GameObject playerUi = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //instantiate popUpText prefab in playerUi
        Transform PopUpTransform = Instantiate(popUpTextPrefab.transform, position, Quaternion.identity, playerUi.transform);

        //get script component
        PopUpText popUp = PopUpTransform.GetComponent<PopUpText>();

        //if escaped successfully setup success popup
        if (escaped) {
            //escape string
            string escapeString = "ESCAPED";

            //setup popUp to disappear
            popUp.Setup(escapeString, escapeSuccessColor);
        }
        //if failed to escape setup fail popup
        else {
            //escape string
            string escapeString = "FAILED " + (escapeChance * 100).ToString() + "%"; //escape chance in %

            //setup popUp to disappear
            popUp.Setup(escapeString, escapeFailColor);
        }

        return popUp;
    }

    //escape unavailable popUp text create function
    public PopUpText createEscapeUnavailablePopUpText(Vector3 position)
    {
        //get playerUi
        GameObject playerUi = GameObject.FindGameObjectsWithTag("player_ui")[0];

        //instantiate popUpText prefab in playerUi
        Transform PopUpTransform = Instantiate(popUpTextPrefab.transform, position, Quaternion.identity, playerUi.transform);

        //get script component
        PopUpText popUp = PopUpTransform.GetComponent<PopUpText>();

        //escape string
        string escapeString = "CANNOT ESCAPE THIS BATTLE";

        //setup popUp to disappear
        popUp.Setup(escapeString, escapeFailColor);

        return popUp;
    }

}
