using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //lists of enemies
    public Enemy[] enemies;

    //list of bosses
    public Enemy[] bosses;

    //get enemies
    public Enemy get_random_enemy() {
        //get length of list
        int n = enemies.Length;
        
        //create example
        Enemy ex = enemies[Random.Range(0, n)];

        //create enemy instance
        Enemy enemy = ScriptableObject.CreateInstance<Enemy>();

        //set values of instance based on example values
        enemy.set_name(ex.get_enemy_name());
        enemy.set_max_health(ex.get_max_health());
        enemy.set_damage(ex.get_damage());
        enemy.set_armor(ex.get_armor());
        enemy.set_crit_chance(ex.get_crit_chance());
        enemy.set_level(ex.get_level());
        enemy.set_heal(ex.get_heal());
        enemy.set_drop_chance(ex.get_drop_chance());
        enemy.set_drop_items(ex.get_drop_items());
        enemy.set_is_boss(ex.get_is_boss());

        return enemy;

    }

    //get bosses
    public Enemy[] get_random_boss_list(int player_amount) {
        //list of copies of bosses
        Enemy[] bossList = new Enemy[player_amount];

        //get length of list
        int n = bosses.Length;
        
        //create example
        Enemy ex = bosses[Random.Range(0, n)];

        //create boss instance for each player
        for (int i = 0; i < player_amount; i++) {
            //create boss instance
            Enemy boss = ScriptableObject.CreateInstance<Enemy>();

            //set values of instance based on example values
            boss.set_name(ex.get_enemy_name());
            boss.set_max_health(ex.get_max_health());
            boss.set_damage(ex.get_damage());
            boss.set_armor(ex.get_armor());
            boss.set_crit_chance(ex.get_crit_chance());
            boss.set_level(ex.get_level());
            boss.set_heal(ex.get_heal());
            boss.set_drop_chance(ex.get_drop_chance());
            boss.set_drop_items(ex.get_drop_items());
            boss.set_is_boss(ex.get_is_boss());

            //add boss to list
            bossList[i] = boss;
        }

        return bossList;
    }
 
}
