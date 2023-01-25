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

    //load stats values from PlayerPrefs
    public static void loadGameStatistics() {
        //get games played set to 0 if not found
        gamesPlayed = PlayerPrefs.GetInt("gamesPlayed", 0);
        //get games won set to 0 if not found
        gamesWonPlayer1 = PlayerPrefs.GetInt("gamesWonPlayer1", 0);
        gamesWonPlayer2 = PlayerPrefs.GetInt("gamesWonPlayer2", 0);
        gamesWonPlayer3 = PlayerPrefs.GetInt("gamesWonPlayer3", 0);
        gamesWonPlayer4 = PlayerPrefs.GetInt("gamesWonPlayer4", 0);
        //get games lost set to 0 if not found
        gamesLostPlayer1 = PlayerPrefs.GetInt("gamesLostPlayer1", 0);
        gamesLostPlayer2 = PlayerPrefs.GetInt("gamesLostPlayer2", 0);
        gamesLostPlayer3 = PlayerPrefs.GetInt("gamesLostPlayer3", 0);
        gamesLostPlayer4 = PlayerPrefs.GetInt("gamesLostPlayer4", 0);
        //get mobs killed set to 0 if not found
        mobsKilledPlayer1 = PlayerPrefs.GetInt("mobsKilledPlayer1", 0);
        mobsKilledPlayer2 = PlayerPrefs.GetInt("mobsKilledPlayer2", 0);
        mobsKilledPlayer3 = PlayerPrefs.GetInt("mobsKilledPlayer3", 0);
        mobsKilledPlayer4 = PlayerPrefs.GetInt("mobsKilledPlayer4", 0);
        //get players killed set to 0 if not found
        playersKilledPlayer1 = PlayerPrefs.GetInt("playersKilledPlayer1", 0);
        playersKilledPlayer2 = PlayerPrefs.GetInt("playersKilledPlayer2", 0);
        playersKilledPlayer3 = PlayerPrefs.GetInt("playersKilledPlayer3", 0);
        playersKilledPlayer4 = PlayerPrefs.GetInt("playersKilledPlayer4", 0);
        //get deaths set to 0 if not found
        deathsPlayer1 = PlayerPrefs.GetInt("deathsPlayer1", 0);
        deathsPlayer2 = PlayerPrefs.GetInt("deathsPlayer2", 0);
        deathsPlayer3 = PlayerPrefs.GetInt("deathsPlayer3", 0);
        deathsPlayer4 = PlayerPrefs.GetInt("deathsPlayer4", 0);
        //get damage dealt set to 0 if not found
        damageDealtPlayer1 = PlayerPrefs.GetInt("damageDealtPlayer1", 0);
        damageDealtPlayer2 = PlayerPrefs.GetInt("damageDealtPlayer2", 0);
        damageDealtPlayer3 = PlayerPrefs.GetInt("damageDealtPlayer3", 0);
        damageDealtPlayer4 = PlayerPrefs.GetInt("damageDealtPlayer4", 0);
        //get damage received set to 0 if not found
        damageReceivedPlayer1 = PlayerPrefs.GetInt("damageReceivedPlayer1", 0);
        damageReceivedPlayer2 = PlayerPrefs.GetInt("damageReceivedPlayer2", 0);
        damageReceivedPlayer3 = PlayerPrefs.GetInt("damageReceivedPlayer3", 0);
        damageReceivedPlayer4 = PlayerPrefs.GetInt("damageReceivedPlayer4", 0);
        //get healing done set to 0 if not found
        healingDonePlayer1 = PlayerPrefs.GetInt("healingDonePlayer1", 0);
        healingDonePlayer2 = PlayerPrefs.GetInt("healingDonePlayer2", 0);
        healingDonePlayer3 = PlayerPrefs.GetInt("healingDonePlayer3", 0);
        healingDonePlayer4 = PlayerPrefs.GetInt("healingDonePlayer4", 0);
        //get fights escaped set to 0 if not found
        fightsEscapedPlayer1 = PlayerPrefs.GetInt("fightsEscapedPlayer1", 0);
        fightsEscapedPlayer2 = PlayerPrefs.GetInt("fightsEscapedPlayer2", 0);
        fightsEscapedPlayer3 = PlayerPrefs.GetInt("fightsEscapedPlayer3", 0);
        fightsEscapedPlayer4 = PlayerPrefs.GetInt("fightsEscapedPlayer4", 0);
        //get levels gained set to 0 if not found
        levelsGainedPlayer1 = PlayerPrefs.GetInt("levelsGainedPlayer1", 0);
        levelsGainedPlayer2 = PlayerPrefs.GetInt("levelsGainedPlayer2", 0);
        levelsGainedPlayer3 = PlayerPrefs.GetInt("levelsGainedPlayer3", 0);
        levelsGainedPlayer4 = PlayerPrefs.GetInt("levelsGainedPlayer4", 0);
        //get max level reached set to 1 if not found
        maxLevelReachedPlayer1 = PlayerPrefs.GetInt("maxLevelReachedPlayer1", 1);
        maxLevelReachedPlayer2 = PlayerPrefs.GetInt("maxLevelReachedPlayer2", 1);
        maxLevelReachedPlayer3 = PlayerPrefs.GetInt("maxLevelReachedPlayer3", 1);
        maxLevelReachedPlayer4 = PlayerPrefs.GetInt("maxLevelReachedPlayer4", 1);
        //get items equipped set to 0 if not found
        itemsEquippedPlayer1 = PlayerPrefs.GetInt("itemsEquippedPlayer1", 0);
        itemsEquippedPlayer2 = PlayerPrefs.GetInt("itemsEquippedPlayer2", 0);
        itemsEquippedPlayer3 = PlayerPrefs.GetInt("itemsEquippedPlayer3", 0);
        itemsEquippedPlayer4 = PlayerPrefs.GetInt("itemsEquippedPlayer4", 0);
        //get items declined set to 0 if not found
        itemsDeclinedPlayer1 = PlayerPrefs.GetInt("itemsDeclinedPlayer1", 0);
        itemsDeclinedPlayer2 = PlayerPrefs.GetInt("itemsDeclinedPlayer2", 0);
        itemsDeclinedPlayer3 = PlayerPrefs.GetInt("itemsDeclinedPlayer3", 0);
        itemsDeclinedPlayer4 = PlayerPrefs.GetInt("itemsDeclinedPlayer4", 0);
        //get moves made set to 0 if not found
        movesMadePlayer1 = PlayerPrefs.GetInt("movesMadePlayer1", 0);
        movesMadePlayer2 = PlayerPrefs.GetInt("movesMadePlayer2", 0);
        movesMadePlayer3 = PlayerPrefs.GetInt("movesMadePlayer3", 0);
        movesMadePlayer4 = PlayerPrefs.GetInt("movesMadePlayer4", 0);
    }

    //save stats values to PlayerPrefs
    public static void saveGameStatistics() {
        //games played
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayed);
        //games won
        PlayerPrefs.SetInt("gamesWonPlayer1", gamesWonPlayer1);
        PlayerPrefs.SetInt("gamesWonPlayer2", gamesWonPlayer2);
        PlayerPrefs.SetInt("gamesWonPlayer3", gamesWonPlayer3);
        PlayerPrefs.SetInt("gamesWonPlayer4", gamesWonPlayer4);
        //games lost
        PlayerPrefs.SetInt("gamesLostPlayer1", gamesLostPlayer1);
        PlayerPrefs.SetInt("gamesLostPlayer2", gamesLostPlayer2);
        PlayerPrefs.SetInt("gamesLostPlayer3", gamesLostPlayer3);
        PlayerPrefs.SetInt("gamesLostPlayer4", gamesLostPlayer4);
        //mobs killed
        PlayerPrefs.SetInt("mobsKilledPlayer1", mobsKilledPlayer1);
        PlayerPrefs.SetInt("mobsKilledPlayer2", mobsKilledPlayer2);
        PlayerPrefs.SetInt("mobsKilledPlayer3", mobsKilledPlayer3);
        PlayerPrefs.SetInt("mobsKilledPlayer4", mobsKilledPlayer4);
        //players killed
        PlayerPrefs.SetInt("playersKilledPlayer1", playersKilledPlayer1);
        PlayerPrefs.SetInt("playersKilledPlayer2", playersKilledPlayer2);
        PlayerPrefs.SetInt("playersKilledPlayer3", playersKilledPlayer3);
        PlayerPrefs.SetInt("playersKilledPlayer4", playersKilledPlayer4);
        //deaths
        PlayerPrefs.SetInt("deathsPlayer1", deathsPlayer1);
        PlayerPrefs.SetInt("deathsPlayer2", deathsPlayer2);
        PlayerPrefs.SetInt("deathsPlayer3", deathsPlayer3);
        PlayerPrefs.SetInt("deathsPlayer4", deathsPlayer4);
        //damage dealt
        PlayerPrefs.SetInt("damageDealtPlayer1", damageDealtPlayer1);
        PlayerPrefs.SetInt("damageDealtPlayer2", damageDealtPlayer2);
        PlayerPrefs.SetInt("damageDealtPlayer3", damageDealtPlayer3);
        PlayerPrefs.SetInt("damageDealtPlayer4", damageDealtPlayer4);
        //damage received
        PlayerPrefs.SetInt("damageReceivedPlayer1", damageReceivedPlayer1);
        PlayerPrefs.SetInt("damageReceivedPlayer2", damageReceivedPlayer2);
        PlayerPrefs.SetInt("damageReceivedPlayer3", damageReceivedPlayer3);
        PlayerPrefs.SetInt("damageReceivedPlayer4", damageReceivedPlayer4);
        //healing done
        PlayerPrefs.SetInt("healingDonePlayer1", healingDonePlayer1);
        PlayerPrefs.SetInt("healingDonePlayer2", healingDonePlayer2);
        PlayerPrefs.SetInt("healingDonePlayer3", healingDonePlayer3);
        PlayerPrefs.SetInt("healingDonePlayer4", healingDonePlayer4);
        //fights escaped
        PlayerPrefs.SetInt("fightsEscapedPlayer1", fightsEscapedPlayer1);
        PlayerPrefs.SetInt("fightsEscapedPlayer2", fightsEscapedPlayer2);
        PlayerPrefs.SetInt("fightsEscapedPlayer3", fightsEscapedPlayer3);
        PlayerPrefs.SetInt("fightsEscapedPlayer4", fightsEscapedPlayer4);
        //levels gained
        PlayerPrefs.SetInt("levelsGainedPlayer1", levelsGainedPlayer1);
        PlayerPrefs.SetInt("levelsGainedPlayer2", levelsGainedPlayer2);
        PlayerPrefs.SetInt("levelsGainedPlayer3", levelsGainedPlayer3);
        PlayerPrefs.SetInt("levelsGainedPlayer4", levelsGainedPlayer4);
        //max level reached
        PlayerPrefs.SetInt("maxLevelReachedPlayer1", maxLevelReachedPlayer1);
        PlayerPrefs.SetInt("maxLevelReachedPlayer2", maxLevelReachedPlayer2);
        PlayerPrefs.SetInt("maxLevelReachedPlayer3", maxLevelReachedPlayer3);
        PlayerPrefs.SetInt("maxLevelReachedPlayer4", maxLevelReachedPlayer4);
        //items equipped
        PlayerPrefs.SetInt("itemsEquippedPlayer1", itemsEquippedPlayer1);
        PlayerPrefs.SetInt("itemsEquippedPlayer2", itemsEquippedPlayer2);
        PlayerPrefs.SetInt("itemsEquippedPlayer3", itemsEquippedPlayer3);
        PlayerPrefs.SetInt("itemsEquippedPlayer4", itemsEquippedPlayer4);
        //items declined
        PlayerPrefs.SetInt("itemsDeclinedPlayer1", itemsDeclinedPlayer1);
        PlayerPrefs.SetInt("itemsDeclinedPlayer2", itemsDeclinedPlayer2);
        PlayerPrefs.SetInt("itemsDeclinedPlayer3", itemsDeclinedPlayer3);
        PlayerPrefs.SetInt("itemsDeclinedPlayer4", itemsDeclinedPlayer4);
        //moves made
        PlayerPrefs.SetInt("movesMadePlayer1", movesMadePlayer1);
        PlayerPrefs.SetInt("movesMadePlayer2", movesMadePlayer2);
        PlayerPrefs.SetInt("movesMadePlayer3", movesMadePlayer3);
        PlayerPrefs.SetInt("movesMadePlayer4", movesMadePlayer4);
    }

}
