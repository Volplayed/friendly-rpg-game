using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Friendly RPG/Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public string enemyName;

    //stats
    public int health;
    public int damage;
    public double armor;

    public double crit_chance;
    

}
