using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //lists of enemies
    public Enemy[] enemies;

    private Enemy[] enemyLevel1 = new Enemy[0];
    private Enemy[] enemyLevel2 = new Enemy[0];
    private Enemy[] enemyLevel3 = new Enemy[0];
    private Enemy[] enemyLevel4 = new Enemy[0];
    private Enemy[] enemyLevel5 = new Enemy[0];
    private Enemy[] enemyLevel6 = new Enemy[0];
    private Enemy[] enemyLevel7 = new Enemy[0];
    private Enemy[] enemyLevel8 = new Enemy[0];
    private Enemy[] enemyLevel9 = new Enemy[0];
    private Enemy[] enemyLevel10 = new Enemy[0];

    //list of bosses
    public Enemy[] bosses;

    //turns until next enemy list iteration
    public int turnsUntilNextEnemyListIteration = 15;

    //enemy matrix
    private Enemy[][] enemyMatrix = new Enemy[10][];

    //current enemy list iteration
    private int currentStageBottom = 0; //level 1
    private int currentStageTop = 1; //level 2

    void Awake() {
        //generate enemy matrix
        generateEnemyMatrix();
    }

    //get max enemy level
    private int getMaxEnemyLevel() {
        int max = 0;

        //get length of list
        int n = enemies.Length;

        //get max level
        for (int i = 0; i < n; i++) {
            if (enemies[i].getLevel() > max) {
                max = enemies[i].getLevel();
            }
        }

        return max;
    }

    //generate enemy matrix
    public void generateEnemyMatrix() {
        //get length of list
        int n = enemies.Length;

        //send each enemy to its level list
        foreach (Enemy enemy in enemies) {
            //get level
            int level = enemy.getLevel();

            //add enemy to list
            switch (level) {
                case 1:
                    enemyLevel1 = addEnemyToList(enemyLevel1, enemy);
                    break;
                case 2:
                    enemyLevel2 = addEnemyToList(enemyLevel2, enemy);
                    break;
                case 3:
                    enemyLevel3 = addEnemyToList(enemyLevel3, enemy);
                    break;
                case 4:
                    enemyLevel4 = addEnemyToList(enemyLevel4, enemy);
                    break;
                case 5:
                    enemyLevel5 = addEnemyToList(enemyLevel5, enemy);
                    break;
                case 6:
                    enemyLevel6 = addEnemyToList(enemyLevel6, enemy);
                    break;
                case 7:
                    enemyLevel7 = addEnemyToList(enemyLevel7, enemy);
                    break;
                case 8:
                    enemyLevel8 = addEnemyToList(enemyLevel8, enemy);
                    break;
                case 9:
                    enemyLevel9 = addEnemyToList(enemyLevel9, enemy);
                    break;
                case 10:
                    enemyLevel10 = addEnemyToList(enemyLevel10, enemy);
                    break;
            }
        }

        //add lists to matrix
        enemyMatrix[0] = enemyLevel1;
        enemyMatrix[1] = enemyLevel2;
        enemyMatrix[2] = enemyLevel3;
        enemyMatrix[3] = enemyLevel4;
        enemyMatrix[4] = enemyLevel5;
        enemyMatrix[5] = enemyLevel6;
        enemyMatrix[6] = enemyLevel7;
        enemyMatrix[7] = enemyLevel8;
        enemyMatrix[8] = enemyLevel9;
        enemyMatrix[9] = enemyLevel10;
    }

    //add enemy to list
    private Enemy[] addEnemyToList(Enemy[] list, Enemy enemy) {
        //get length of list
        int n = list.Length;

        //create new list
        Enemy[] newList = new Enemy[n + 1];

        //copy old list to new list
        for (int i = 0; i < n; i++) {
            newList[i] = list[i];
        }

        //add enemy to new list
        newList[n] = enemy;

        return newList;
    }

    //get enemies
    public Enemy getRandomEnemy() {
        //random value which determines which stage the enemy is in
        int stage = Random.Range(currentStageBottom, currentStageTop + 1);

        //get list of enemies in stage
        Enemy[] enemyList = enemyMatrix[stage];

        //get length of list
        int n = enemyList.Length;
        
        //create example
        Enemy ex = enemyList[Random.Range(0, n)];

        //create enemy instance
        Enemy enemy = ScriptableObject.CreateInstance<Enemy>();

        //set values of instance based on example values
        enemy.setName(ex.getEnemyName());
        enemy.setMaxHealth(ex.getMaxHealth());
        enemy.setDamage(ex.getDamage());
        enemy.setArmor(ex.getArmor());
        enemy.setCritChance(ex.getCritChance());
        enemy.setLevel(ex.getLevel());
        enemy.setHeal(ex.getHeal());
        enemy.setDropChance(ex.getDropChance());
        enemy.setDropItems(ex.getDropItems());
        enemy.setIsBoss(ex.getIsBoss());

        return enemy;

    }

    //get bosses
    public Enemy[] getRandomBossList(int player_amount) {
        //list of copies of bosses
        Enemy[] bossList = new Enemy[player_amount];

        //get length of list
        int n = bosses.Length;
        
        //create example
        Enemy ex = bosses[Random.Range(0, n)];

        //create boss instance for each player
        for (int i = 0; i < player_amount; i++) {
            //create boss instance
            Enemy boss = ScriptableObject.CreateInstance<Enemy>();

            //set values of instance based on example values
            boss.setName(ex.getEnemyName());
            boss.setMaxHealth(ex.getMaxHealth());
            boss.setDamage(ex.getDamage());
            boss.setArmor(ex.getArmor());
            boss.setCritChance(ex.getCritChance());
            boss.setLevel(ex.getLevel());
            boss.setHeal(ex.getHeal());
            boss.setDropChance(ex.getDropChance());
            boss.setDropItems(ex.getDropItems());
            boss.setIsBoss(ex.getIsBoss());

            //add boss to list
            bossList[i] = boss;
        }

        return bossList;
    }

    //next stage
    public void nextStage(int currentTurn) {
        //check if current turn % next enemy list iteration = 0
        if (currentTurn % turnsUntilNextEnemyListIteration == 0) {
            //increment current stage
            currentStageBottom++;
            currentStageTop++;

            //check if current stage is greater than max enemy level
            if (currentStageTop > getMaxEnemyLevel()) {
                //set current stage to max enemy level
                currentStageBottom = getMaxEnemyLevel();
                currentStageTop = getMaxEnemyLevel();
            }
        }
    }
 
}
