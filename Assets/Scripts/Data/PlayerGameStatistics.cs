using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameStatistics : MonoBehaviour
{
    //games played
    public static int gamesPlayed = 0;

    //games won
    public static int gamesWonPlayer1 = 0;
    public static int gamesWonPlayer2 = 0;
    public static int gamesWonPlayer3 = 0;
    public static int gamesWonPlayer4 = 0;

    //games lost
    public static int gamesLostPlayer1 = 0;
    public static int gamesLostPlayer2 = 0;
    public static int gamesLostPlayer3 = 0;
    public static int gamesLostPlayer4 = 0;

    //mobs killed
    public static int mobsKilledPlayer1 = 0;
    public static int mobsKilledPlayer2 = 0;
    public static int mobsKilledPlayer3 = 0;
    public static int mobsKilledPlayer4 = 0;

    //players killed
    public static int playersKilledPlayer1 = 0;
    public static int playersKilledPlayer2 = 0;
    public static int playersKilledPlayer3 = 0;
    public static int playersKilledPlayer4 = 0;

    //deaths
    public static int deathsPlayer1 = 0;
    public static int deathsPlayer2 = 0;
    public static int deathsPlayer3 = 0;
    public static int deathsPlayer4 = 0;
    
    //damage dealt
    public static int damageDealtPlayer1 = 0;
    public static int damageDealtPlayer2 = 0;
    public static int damageDealtPlayer3 = 0;
    public static int damageDealtPlayer4 = 0;

    //damage received
    public static int damageReceivedPlayer1 = 0;
    public static int damageReceivedPlayer2 = 0;
    public static int damageReceivedPlayer3 = 0;
    public static int damageReceivedPlayer4 = 0;

    //healing done
    public static int healingDonePlayer1 = 0;
    public static int healingDonePlayer2 = 0;
    public static int healingDonePlayer3 = 0;
    public static int healingDonePlayer4 = 0;

    //fights escaped
    public static int fightsEscapedPlayer1 = 0;
    public static int fightsEscapedPlayer2 = 0;
    public static int fightsEscapedPlayer3 = 0;
    public static int fightsEscapedPlayer4 = 0;

    //levels gained
    public static int levelsGainedPlayer1 = 0;
    public static int levelsGainedPlayer2 = 0;
    public static int levelsGainedPlayer3 = 0;
    public static int levelsGainedPlayer4 = 0;

    //max level reached
    public static int maxLevelReachedPlayer1 = 1;
    public static int maxLevelReachedPlayer2 = 1;
    public static int maxLevelReachedPlayer3 = 1;
    public static int maxLevelReachedPlayer4 = 1;

    //items equipped
    public static int itemsEquippedPlayer1 = 0;
    public static int itemsEquippedPlayer2 = 0;
    public static int itemsEquippedPlayer3 = 0;
    public static int itemsEquippedPlayer4 = 0;

    //items declined
    public static int itemsDeclinedPlayer1 = 0;
    public static int itemsDeclinedPlayer2 = 0;
    public static int itemsDeclinedPlayer3 = 0;
    public static int itemsDeclinedPlayer4 = 0;

    //moves made
    public static int movesMadePlayer1 = 0;
    public static int movesMadePlayer2 = 0;
    public static int movesMadePlayer3 = 0;
    public static int movesMadePlayer4 = 0;
    
    //functions
    public static void addGamesPlayed() {
        gamesPlayed++;
    }

    //add games won
    public static void addGamesWon(int playerNumber) {
        switch (playerNumber) {
            case 1:
                gamesWonPlayer1++;
                break;
            case 2:
                gamesWonPlayer2++;
                break;
            case 3:
                gamesWonPlayer3++;
                break;
            case 4:
                gamesWonPlayer4++;
                break;
        }
    }

    //add games lost
    public static void addGamesLost(int playerNumber) {
        switch (playerNumber) {
            case 1:
                gamesLostPlayer1++;
                break;
            case 2:
                gamesLostPlayer2++;
                break;
            case 3:
                gamesLostPlayer3++;
                break;
            case 4:
                gamesLostPlayer4++;
                break;
        }
    }

    //add mobs killed
    public static void addMobsKilled(int playerNumber) {
        switch (playerNumber) {
            case 1:
                mobsKilledPlayer1++;
                break;
            case 2:
                mobsKilledPlayer2++;
                break;
            case 3:
                mobsKilledPlayer3++;
                break;
            case 4:
                mobsKilledPlayer4++;
                break;
        }
    }

    //add players killed
    public static void addPlayersKilled(int playerNumber) {
        switch (playerNumber) {
            case 1:
                playersKilledPlayer1++;
                break;
            case 2:
                playersKilledPlayer2++;
                break;
            case 3:
                playersKilledPlayer3++;
                break;
            case 4:
                playersKilledPlayer4++;
                break;
        }
    }

    //add deaths
    public static void addDeaths(int playerNumber) {
        switch (playerNumber) {
            case 1:
                deathsPlayer1++;
                break;
            case 2:
                deathsPlayer2++;
                break;
            case 3:
                deathsPlayer3++;
                break;
            case 4:
                deathsPlayer4++;
                break;
        }
    }

    //add damage dealt
    public static void addDamageDealt(int playerNumber, int damage) {
        switch (playerNumber) {
            case 1:
                damageDealtPlayer1 += damage;
                break;
            case 2:
                damageDealtPlayer2 += damage;
                break;
            case 3:
                damageDealtPlayer3 += damage;
                break;
            case 4:
                damageDealtPlayer4 += damage;
                break;
        }
    }

    //add damage received
    public static void addDamageReceived(int playerNumber, int damage) {
        switch (playerNumber) {
            case 1:
                damageReceivedPlayer1 += damage;
                break;
            case 2:
                damageReceivedPlayer2 += damage;
                break;
            case 3:
                damageReceivedPlayer3 += damage;
                break;
            case 4:
                damageReceivedPlayer4 += damage;
                break;
        }
    }

    //add healing done
    public static void addHealingDone(int playerNumber, int healing) {
        switch (playerNumber) {
            case 1:
                healingDonePlayer1 += healing;
                break;
            case 2:
                healingDonePlayer2 += healing;
                break;
            case 3:
                healingDonePlayer3 += healing;
                break;
            case 4:
                healingDonePlayer4 += healing;
                break;
        }
    }

    //add fights escaped
    public static void addFightsEscaped(int playerNumber) {
        switch (playerNumber) {
            case 1:
                fightsEscapedPlayer1++;
                break;
            case 2:
                fightsEscapedPlayer2++;
                break;
            case 3:
                fightsEscapedPlayer3++;
                break;
            case 4:
                fightsEscapedPlayer4++;
                break;
        }
    }

    //add levels gained
    public static void addLevelsGained(int playerNumber) {
        switch (playerNumber) {
            case 1:
                levelsGainedPlayer1++;
                break;
            case 2:
                levelsGainedPlayer2++;
                break;
            case 3:
                levelsGainedPlayer3++;
                break;
            case 4:
                levelsGainedPlayer4++;
                break;
        }
    }

    //check max level reached
    public static void checkMaxLevelReached(int playerNumber, int level) {
        switch (playerNumber) {
            case 1:
                if (level > maxLevelReachedPlayer1) {
                    maxLevelReachedPlayer1 = level;
                }
                break;
            case 2:
                if (level > maxLevelReachedPlayer2) {
                    maxLevelReachedPlayer2 = level;
                }
                break;
            case 3:
                if (level > maxLevelReachedPlayer3) {
                    maxLevelReachedPlayer3 = level;
                }
                break;
            case 4:
                if (level > maxLevelReachedPlayer4) {
                    maxLevelReachedPlayer4 = level;
                }
                break;
        }
    }

    //add items equipped
    public static void addItemsEquipped(int playerNumber) {
        switch (playerNumber) {
            case 1:
                itemsEquippedPlayer1++;
                break;
            case 2:
                itemsEquippedPlayer2++;
                break;
            case 3:
                itemsEquippedPlayer3++;
                break;
            case 4:
                itemsEquippedPlayer4++;
                break;
        }
    }

    //add items declined
    public static void addItemsDeclined(int playerNumber) {
        switch (playerNumber) {
            case 1:
                itemsDeclinedPlayer1++;
                break;
            case 2:
                itemsDeclinedPlayer2++;
                break;
            case 3:
                itemsDeclinedPlayer3++;
                break;
            case 4:
                itemsDeclinedPlayer4++;
                break;
        }
    }

    //add moves made
    public static void addMovesMade(int playerNumber) {
        switch (playerNumber) {
            case 1:
                movesMadePlayer1++;
                break;
            case 2:
                movesMadePlayer2++;
                break;
            case 3:
                movesMadePlayer3++;
                break;
            case 4:
                movesMadePlayer4++;
                break;
        }
    }

}
