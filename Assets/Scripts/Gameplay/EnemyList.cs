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
        
        return enemies[Random.Range(0, n)];
    }
 
}
