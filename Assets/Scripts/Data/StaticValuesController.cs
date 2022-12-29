using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValuesController : MonoBehaviour
{
    //player amount
    public static int playerAmount;

    //turns before final boss
    public static int finalBossTurn = 10;

    //final boss
    public static Enemy finalBoss;
    
    //did last attack crit?
    public static bool lastAttackCrit = false;
}
