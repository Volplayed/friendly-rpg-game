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
        playerStats.set_health(playerStats.get_max_health());

        //set values of current player
        slider.maxValue = playerStats.get_max_health();
        slider.value = playerStats.get_health();

        playerHealthText.SetText(playerStats.get_health() + "/" + playerStats.get_max_health());
        playerNameText.SetText(playerStats.get_player_name());

        //if fighting with enemy not player
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();

            //set enemy health to max
            enemy.set_starting_health();

            //set enemy values
            enemySlider.maxValue = enemy.get_max_health();
            enemySlider.value = enemy.get_health();

            enemyHealthText.SetText(enemy.get_health() + "/" + enemy.get_max_health());
            enemyNameText.SetText(enemy.get_enemy_name());
        }
        //if fighting with player
        else if (playerStats.get_enemy_player() != null) {
            //get enemy player
            GameObject other_player = playerStats.get_enemy_player();
            PlayerStats other_playerStats = other_player.GetComponent<PlayerStats>();

            //set enemy player values
            enemySlider.maxValue = other_playerStats.get_max_health();
            enemySlider.value = other_playerStats.get_health();

            enemyHealthText.SetText(other_playerStats.get_health() + "/" + other_playerStats.get_max_health());
            enemyNameText.SetText(other_playerStats.get_player_name());
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
        slider.maxValue = playerStats.get_max_health();
        slider.value = playerStats.get_health();

        playerHealthText.SetText(playerStats.get_health() + "/" + playerStats.get_max_health());
        playerNameText.SetText(playerStats.get_player_name());

        //if fighting with enemy not player
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();;

            //set enemy values
            enemySlider.maxValue = enemy.get_max_health();
            enemySlider.value = enemy.get_health();

            enemyHealthText.SetText(enemy.get_health() + "/" + enemy.get_max_health());
            enemyNameText.SetText(enemy.get_enemy_name());
        }
        //if fighting with player
        else if (playerStats.get_enemy_player() != null) {
            //get enemy player
            GameObject other_player = playerStats.get_enemy_player();
            PlayerStats other_playerStats = other_player.GetComponent<PlayerStats>();

            //set enemy player values
            enemySlider.maxValue = other_playerStats.get_max_health();
            enemySlider.value = other_playerStats.get_health();

            enemyHealthText.SetText(other_playerStats.get_health() + "/" + other_playerStats.get_max_health());
            enemyNameText.SetText(other_playerStats.get_player_name());
        }

    }

    //make buttons interactable or not
    public void set_button_interactable(bool interactable) {
        //make buttons interactable or not
        attackButton.GetComponent<Button>().interactable = interactable;
        healButton.GetComponent<Button>().interactable = interactable;
        retreatButton.GetComponent<Button>().interactable = interactable;

        //show (hide) end turn button if skip turn button hidden (shown)
        skipTurnButton.SetActive(interactable);
        endTurnButton.SetActive(!interactable);
    }

    //make buttons interactable after delay
    public void set_button_interactable_after_delay(float delay = 0.3f) {
        //start coroutine
        StartCoroutine(set_button_interactable_after_delay_coroutine(delay));
    }
    //make buttons interactable after delay coroutine
    private IEnumerator set_button_interactable_after_delay_coroutine(float delay) {
        //wait for delay
        yield return new WaitForSeconds(delay);

        //make buttons interactable
        set_button_interactable(true);
    }

    //buttons functions
    public void player_attack() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //attack enemy
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();
            //attack enemy and get damage dealt and create popup text with value of damage dealt
            //position of popup text is position of enemy health bar
            createDamagePopUpText(enemyHealthBar.transform.position, playerStats.attack(enemy));

            //set changed values
            setValues();

            
            
            //make buttons not interactable
            set_button_interactable(false);

            //check if enemy did not died
            if (!enemy.check_death(player)) {
                //if it is an enemy ai act
                act();
            }
        }
        //attack player
        else if (playerStats.get_enemy_player() != null) {
            //get enemy player
            GameObject other_player = playerStats.get_enemy_player();
            PlayerStats other_playerStats = other_player.GetComponent<PlayerStats>();

            //attack enemy and get damage dealt and create popup text with value of damage dealt
            //position of popup text is position of enemy health bar
            createDamagePopUpText(enemyHealthBar.transform.position, playerStats.attack(other_player));

            //make buttons not interactable
            set_button_interactable(false);

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
        set_button_interactable(false);

        //if it is an enemy ai act
        act();
    }

    public void player_escape() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        
        //make buttons not interactable
        set_button_interactable(false);
        //try to escape
        //if vs enemy player
        if (playerStats.get_enemy_player() != null) {
            //get escape chance vs player
            double escape_chance = playerStats.get_escape_chance_vs_player(playerStats.get_enemy_player());

            //escape from enemy player and create popup text with value of escape
            //position of popup text is position of escape button
            createEscapePopUpText(
                retreatButton.transform.position,
                playerStats.escape(playerStats.get_enemy_player()),
                escape_chance //escape chance vs player
                );
            
        }
        //if vs enemy ai and is not boss
        else if (playerStats.get_enemy() != null && !playerStats.get_enemy().get_is_boss()) {
            //get escape chance vs enemy
            double escape_chance = playerStats.get_escape_chance_vs_enemy();

            //escape from enemy ai and create popup text with value of escape
            //position of popup text is position of escape button
            createEscapePopUpText(
                retreatButton.transform.position,
                playerStats.escape(),
                escape_chance //escape chance vs enemy
                );
        }
        //if vs enemy ai and is boss
        else if (playerStats.get_enemy() != null && playerStats.get_enemy().get_is_boss()) {
            //create unavaliable to escape popup text
            //position of popup text is position of escape button
            createEscapeUnavailablePopUpText(retreatButton.transform.position);

            //make buttons interactable without delay
            set_button_interactable(true);
        }
        
    }

    //skip turn
    public void player_skip_turn() {
        //make buttons not interactable
        set_button_interactable(false);

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
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();

            //attack if enought hp or already healed
            if (enemy.get_health() >= enemy.get_max_health() * 0.5 || enemy.get_healed()) {               
                //attack player and get damage dealt and create popup text with value of damage dealt
                //position of popup text is position of player health bar
                createDamagePopUpText(playerHealthBar.transform.position, enemy.attack(player));

                //set changed values
                setValues();

                //set healed to false
                enemy.set_healed(false);
            }
            else if (enemy.get_health() < enemy.get_max_health() * 0.5 && !enemy.get_healed()) {
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
    public PopUpText createEscapePopUpText(Vector3 position, bool escaped, double escape_chance)
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
            string escapeString = "FAILED " + (escape_chance * 100).ToString() + "%"; //escape chance in %

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
