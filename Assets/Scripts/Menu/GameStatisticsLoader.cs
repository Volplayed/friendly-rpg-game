using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatisticsLoader : MonoBehaviour
{
    //games played text
    public TMP_Text gamesPlayed;

    //player1 texts
    public TMP_Text player1Wins;
    public TMP_Text player1Losses;
    public TMP_Text player1EnemyKills;
    public TMP_Text player1PlayerKills;
    public TMP_Text player1Deaths;
    public TMP_Text player1DamageDealt;
    public TMP_Text player1DamageReceived;
    public TMP_Text player1HealingDone;
    public TMP_Text player1FightsEscaped;
    public TMP_Text player1LevelsGained;
    public TMP_Text player1MaxLevelReached;
    public TMP_Text player1ItemsEquipped;
    public TMP_Text player1ItemsDiscarded;
    public TMP_Text player1MovesMade;

    //player2 texts
    public TMP_Text player2Wins;
    public TMP_Text player2Losses;
    public TMP_Text player2EnemyKills;
    public TMP_Text player2PlayerKills;
    public TMP_Text player2Deaths;
    public TMP_Text player2DamageDealt;
    public TMP_Text player2DamageReceived;
    public TMP_Text player2HealingDone;
    public TMP_Text player2FightsEscaped;
    public TMP_Text player2LevelsGained;
    public TMP_Text player2MaxLevelReached;
    public TMP_Text player2ItemsEquipped;
    public TMP_Text player2ItemsDiscarded;
    public TMP_Text player2MovesMade;

    //player3 texts
    public TMP_Text player3Wins;
    public TMP_Text player3Losses;
    public TMP_Text player3EnemyKills;
    public TMP_Text player3PlayerKills;
    public TMP_Text player3Deaths;
    public TMP_Text player3DamageDealt;
    public TMP_Text player3DamageReceived;
    public TMP_Text player3HealingDone;
    public TMP_Text player3FightsEscaped;
    public TMP_Text player3LevelsGained;
    public TMP_Text player3MaxLevelReached;
    public TMP_Text player3ItemsEquipped;
    public TMP_Text player3ItemsDiscarded;
    public TMP_Text player3MovesMade;

    //player4 texts
    public TMP_Text player4Wins;
    public TMP_Text player4Losses;
    public TMP_Text player4EnemyKills;
    public TMP_Text player4PlayerKills;
    public TMP_Text player4Deaths;
    public TMP_Text player4DamageDealt;
    public TMP_Text player4DamageReceived;
    public TMP_Text player4HealingDone;
    public TMP_Text player4FightsEscaped;
    public TMP_Text player4LevelsGained;
    public TMP_Text player4MaxLevelReached;
    public TMP_Text player4ItemsEquipped;
    public TMP_Text player4ItemsDiscarded;
    public TMP_Text player4MovesMade;

    //load and set game statistics
    public void loadAndSetGameStatistics() {
        //load player prefs
        PlayerGameStatistics.loadGameStatistics();

        //set games played
        gamesPlayed.SetText("Games Played: " + PlayerGameStatistics.gamesPlayed.ToString());

        //set player1 stats
        player1Wins.SetText("Wins: " + PlayerGameStatistics.gamesWonPlayer1.ToString());
        player1Losses.SetText("Losses: " + PlayerGameStatistics.gamesLostPlayer1.ToString());
        player1EnemyKills.SetText("Enemy Killed: " + PlayerGameStatistics.mobsKilledPlayer1.ToString());
        player1PlayerKills.SetText("Players Killed: " + (PlayerGameStatistics.playersKilledPlayer1.ToString()));
        player1Deaths.SetText("Deaths: " + PlayerGameStatistics.deathsPlayer1.ToString());
        player1DamageDealt.SetText("Damage Dealt: " + PlayerGameStatistics.damageDealtPlayer1.ToString());
        player1DamageReceived.SetText("Damage Received: " + PlayerGameStatistics.damageReceivedPlayer1.ToString());
        player1HealingDone.SetText("Health Healed: " + PlayerGameStatistics.healingDonePlayer1.ToString());
        player1FightsEscaped.SetText("Escapes: " + PlayerGameStatistics.fightsEscapedPlayer1.ToString());
        player1LevelsGained.SetText("Levels Gained: " + PlayerGameStatistics.levelsGainedPlayer1.ToString());
        player1MaxLevelReached.SetText("Max Level: " + PlayerGameStatistics.maxLevelReachedPlayer1.ToString());
        player1ItemsEquipped.SetText("Items Equipped: " + PlayerGameStatistics.itemsEquippedPlayer1.ToString());
        player1ItemsDiscarded.SetText("Items Discarded: " + PlayerGameStatistics.itemsDiscardedPlayer1.ToString());
        player1MovesMade.SetText("Moves Made: " + PlayerGameStatistics.movesMadePlayer1.ToString());

         //set player2 stats
        player2Wins.SetText("Wins: " + PlayerGameStatistics.gamesWonPlayer2.ToString());
        player2Losses.SetText("Losses: " + PlayerGameStatistics.gamesLostPlayer2.ToString());
        player2EnemyKills.SetText("Enemy Killed: " + PlayerGameStatistics.mobsKilledPlayer2.ToString());
        player2PlayerKills.SetText("Players Killed: " + (PlayerGameStatistics.playersKilledPlayer2.ToString()));
        player2Deaths.SetText("Deaths: " + PlayerGameStatistics.deathsPlayer2.ToString());
        player2DamageDealt.SetText("Damage Dealt: " + PlayerGameStatistics.damageDealtPlayer2.ToString());
        player2DamageReceived.SetText("Damage Received: " + PlayerGameStatistics.damageReceivedPlayer2.ToString());
        player2HealingDone.SetText("Health Healed: " + PlayerGameStatistics.healingDonePlayer2.ToString());
        player2FightsEscaped.SetText("Escapes: " + PlayerGameStatistics.fightsEscapedPlayer2.ToString());
        player2LevelsGained.SetText("Levels Gained: " + PlayerGameStatistics.levelsGainedPlayer2.ToString());
        player2MaxLevelReached.SetText("Max Level: " + PlayerGameStatistics.maxLevelReachedPlayer2.ToString());
        player2ItemsEquipped.SetText("Items Equipped: " + PlayerGameStatistics.itemsEquippedPlayer2.ToString());
        player2ItemsDiscarded.SetText("Items Discarded: " + PlayerGameStatistics.itemsDiscardedPlayer2.ToString());
        player2MovesMade.SetText("Moves Made: " + PlayerGameStatistics.movesMadePlayer2.ToString());


         //set player3 stats
        player3Wins.SetText("Wins: " + PlayerGameStatistics.gamesWonPlayer3.ToString());
        player3Losses.SetText("Losses: " + PlayerGameStatistics.gamesLostPlayer3.ToString());
        player3EnemyKills.SetText("Enemy Killed: " + PlayerGameStatistics.mobsKilledPlayer3.ToString());
        player3PlayerKills.SetText("Players Killed: " + (PlayerGameStatistics.playersKilledPlayer3.ToString()));
        player3Deaths.SetText("Deaths: " + PlayerGameStatistics.deathsPlayer3.ToString());
        player3DamageDealt.SetText("Damage Dealt: " + PlayerGameStatistics.damageDealtPlayer3.ToString());
        player3DamageReceived.SetText("Damage Received: " + PlayerGameStatistics.damageReceivedPlayer3.ToString());
        player3HealingDone.SetText("Health Healed: " + PlayerGameStatistics.healingDonePlayer3.ToString());
        player3FightsEscaped.SetText("Escapes: " + PlayerGameStatistics.fightsEscapedPlayer3.ToString());
        player3LevelsGained.SetText("Levels Gained: " + PlayerGameStatistics.levelsGainedPlayer3.ToString());
        player3MaxLevelReached.SetText("Max Level: " + PlayerGameStatistics.maxLevelReachedPlayer3.ToString());
        player3ItemsEquipped.SetText("Items Equipped: " + PlayerGameStatistics.itemsEquippedPlayer3.ToString());
        player3ItemsDiscarded.SetText("Items Discarded: " + PlayerGameStatistics.itemsDiscardedPlayer3.ToString());
        player3MovesMade.SetText("Moves Made: " + PlayerGameStatistics.movesMadePlayer3.ToString());

         //set player4 stats
        player4Wins.SetText("Wins: " + PlayerGameStatistics.gamesWonPlayer4.ToString());
        player4Losses.SetText("Losses: " + PlayerGameStatistics.gamesLostPlayer4.ToString());
        player4EnemyKills.SetText("Enemy Killed: " + PlayerGameStatistics.mobsKilledPlayer4.ToString());
        player4PlayerKills.SetText("Players Killed: " + (PlayerGameStatistics.playersKilledPlayer4.ToString()));
        player4Deaths.SetText("Deaths: " + PlayerGameStatistics.deathsPlayer4.ToString());
        player4DamageDealt.SetText("Damage Dealt: " + PlayerGameStatistics.damageDealtPlayer4.ToString());
        player4DamageReceived.SetText("Damage Received: " + PlayerGameStatistics.damageReceivedPlayer4.ToString());
        player4HealingDone.SetText("Health Healed: " + PlayerGameStatistics.healingDonePlayer4.ToString());
        player4FightsEscaped.SetText("Escapes: " + PlayerGameStatistics.fightsEscapedPlayer4.ToString());
        player4LevelsGained.SetText("Levels Gained: " + PlayerGameStatistics.levelsGainedPlayer4.ToString());
        player4MaxLevelReached.SetText("Max Level: " + PlayerGameStatistics.maxLevelReachedPlayer4.ToString());
        player4ItemsEquipped.SetText("Items Equipped: " + PlayerGameStatistics.itemsEquippedPlayer4.ToString());
        player4ItemsDiscarded.SetText("Items Discarded: " + PlayerGameStatistics.itemsDiscardedPlayer4.ToString());
        player4MovesMade.SetText("Moves Made: " + PlayerGameStatistics.movesMadePlayer4.ToString());
    }
}
