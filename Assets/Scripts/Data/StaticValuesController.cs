using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValuesController : MonoBehaviour
{
    //did game start?
    public static bool gameStarted = false;

    //player amount
    public static int playerAmount = 2;

    //turns before final boss
    public static int finalBossTurn = 10;

    //final boss
    public static Enemy finalBoss;
    
    //did last attack crit?
    public static bool lastAttackCrit = false;

    //players who won the game 
    public static List<string> winners = new List<string>();

    //first time playing?
    public static bool firstTimePlaying = false;
}
