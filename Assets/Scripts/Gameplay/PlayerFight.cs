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


    private PlayerTurns playerTurns;

    void Start() {
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

    //buttons functions
    public void player_attack() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //attack enemy
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();

            playerStats.attack(enemy);

            //set changed values
            setValues();

            //check if enemy died
            enemy.check_death(player);

            //if it is an enemy ai act
            act();
        }
        //attack player
        else if (playerStats.get_enemy_player() != null) {
            //get enemy player
            GameObject other_player = playerStats.get_enemy_player();
            PlayerStats other_playerStats = other_player.GetComponent<PlayerStats>();

            playerStats.attack(other_player);

            //set changed values
            setValues();
        }
        
    }

    public void player_heal() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //heal player
        playerStats.heal();

        //set changed values
        setValues();

        //if it is an enemy ai act
        act();
    }

    public void player_escpae() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //check if escape is successful
        if (playerStats.escape()) {
            playerStats.finish_fight();
        }
        //else if it is an enemy ai act
        else {
            act();
        }
    }

    //enemy ai functions
    public void act() {
        //get player
        GameObject player = playerTurns.getCurrentPlayer();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        //if enemy is ai not other player
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();

            //attack if enought hp or already healed
            if (enemy.get_health() >= enemy.get_max_health() * 0.5 || enemy.get_healed()) {
                //attack player
                enemy.attack(player);

                //set changed values
                setValues();

                //set healed to false
                enemy.set_healed(false);
            }
            else if (enemy.get_health() < enemy.get_max_health() * 0.5 && !enemy.get_healed()) {
                //heal self
                enemy.heal();
                
                Debug.Log("healed");

                //set changed values
                setValues();

                //set healed to true
                enemy.set_healed(true);
            }
        }
    }

}
