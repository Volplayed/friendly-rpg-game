using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulas : MonoBehaviour
{

    //damage reduce formula
    public static int calculateReducedDamage(int damageValue, double armor) {
        return System.Convert.ToInt32(damageValue * (100 - armor * Coefficient.armor)/100);
    }

    //health per strength
    public static int calculateHealthPerStrength(int strength) {
        return strength * Coefficient.healthPerStrength;
    }

    //heal amount
    public static int calculateHealAmount(int intelligence, int level) {
        return System.Convert.ToInt32(intelligence / Coefficient.intelligencePerHeal) * Coefficient.healApplication + level;
    }

    //armor per agility
    public static double calculateArmorPerAgility(int agility) {
        return agility * Coefficient.armorPerAgility;
    }

    //crit chance per intelligence
    public static double calculateCritChancePerIntelligence(int intelligence) {
        return intelligence * Coefficient.critChancePerIntelligence;
    }

    //moves
    public static int calculateMoves(int agility) {
        return agility / Coefficient.agilityPerMove;
    }

    //damage per level
    public static int calculateDamagePerLevel(int level) {
        return level * Coefficient.damagePerLevel;
    }

    //escape chance vs enemy
    public static double calculateEscapeChanceVsEnemy(int k, int level, int enemyLevel) {
        //k agility or intelligence * coefficient - enemy level and player level divercity * coefficient
        return k*Coefficient.enemyStatsEscapeChance - (level - enemyLevel) * Coefficient.enemyLevelEscapeChance;
        
    }

    //escape chacne vs player
    public static double calculateEscapeChanceVsPlayer(int k, int otherAgility) {
        //k - player agility or intelligence
        return k*Coefficient.playerStatsEscapeChance - otherAgility* Coefficient.playerEnemyStatsEscapeChance;
    }

}