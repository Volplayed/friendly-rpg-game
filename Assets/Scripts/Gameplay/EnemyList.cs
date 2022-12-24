using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //lists of enemies
    public Enemy[] enemies;

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
        enemy.set_drop_items(ex.get_drop_items());

        return enemy;

    }
 
}
