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

    //enemy related objects
    public GameObject enemyHealthBar;

    public TMP_Text enemyHealthText;

    private PlayerTurns playerTurns;

    void Start() {
        playerTurns = GetComponent<PlayerTurns>();

    }

    //set values of healths bars and texts depending on players current turn
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

        //if fighting with enemy not player
        if (playerStats.get_enemy() != null) {
            //get enemy
            Enemy enemy = playerStats.get_enemy();

            //set enemy values
            enemySlider.maxValue = enemy.get_max_health();
            enemySlider.value = enemy.get_health();
            enemyHealthText.SetText(enemy.get_health() + "/" + enemy.get_max_health());
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
        }

    }

}
