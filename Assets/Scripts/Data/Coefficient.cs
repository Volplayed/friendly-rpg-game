using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coefficient : MonoBehaviour
{
    
    //armor coefficient
    public static int armor = 12;

    //health per strength
    public static int healthPerStrength = 2;

    //armor per agility
    public static double armorPerAgility = 0.2;

    //crit chance per intelligence
    public static double critChancePerIntelligence = 0.01;

    //moves per intelligence
    public static int movesPerIntelligence = 4;

    //damage per level
    public static int damagePerLevel = 2;

    //vs enemy escape chance
    public static double enemyLevelEscapeChance = 0.05;
    public static double enemyStatsEscapeChance = 0.1;

    //vs player escape chance
    public static double playerEnemyStatsEscapeChance = 0.05;
    public static double playerStatsEscapeChance = 0.1;

}
