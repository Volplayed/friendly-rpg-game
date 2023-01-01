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
    public int turns_until_next_enemy_list_iteration = 15;

    //enemy matrix
    private Enemy[][] enemy_matrix = new Enemy[10][];

    //current enemy list iteration
    private int current_stage_bottom = 0; //level 1
    private int current_stage_top = 1; //level 2

    void Awake() {
        //generate enemy matrix
        generate_enemy_matrix();
    }

    //get max enemy level
    private int get_max_enemy_level() {
        int max = 0;

        //get length of list
        int n = enemies.Length;

        //get max level
        for (int i = 0; i < n; i++) {
            if (enemies[i].get_level() > max) {
                max = enemies[i].get_level();
            }
        }

        return max;
    }

    //generate enemy matrix
    public void generate_enemy_matrix() {
        //get length of list
        int n = enemies.Length;

        //send each enemy to its level list
        foreach (Enemy enemy in enemies) {
            //get level
            int level = enemy.get_level();

            //add enemy to list
            switch (level) {
                case 1:
                    enemyLevel1 = add_enemy_to_list(enemyLevel1, enemy);
                    break;
                case 2:
                    enemyLevel2 = add_enemy_to_list(enemyLevel2, enemy);
                    break;
                case 3:
                    enemyLevel3 = add_enemy_to_list(enemyLevel3, enemy);
                    break;
                case 4:
                    enemyLevel4 = add_enemy_to_list(enemyLevel4, enemy);
                    break;
                case 5:
                    enemyLevel5 = add_enemy_to_list(enemyLevel5, enemy);
                    break;
                case 6:
                    enemyLevel6 = add_enemy_to_list(enemyLevel6, enemy);
                    break;
                case 7:
                    enemyLevel7 = add_enemy_to_list(enemyLevel7, enemy);
                    break;
                case 8:
                    enemyLevel8 = add_enemy_to_list(enemyLevel8, enemy);
                    break;
                case 9:
                    enemyLevel9 = add_enemy_to_list(enemyLevel9, enemy);
                    break;
                case 10:
                    enemyLevel10 = add_enemy_to_list(enemyLevel10, enemy);
                    break;
            }
        }

        //add lists to matrix
        enemy_matrix[0] = enemyLevel1;
        enemy_matrix[1] = enemyLevel2;
        enemy_matrix[2] = enemyLevel3;
        enemy_matrix[3] = enemyLevel4;
        enemy_matrix[4] = enemyLevel5;
        enemy_matrix[5] = enemyLevel6;
        enemy_matrix[6] = enemyLevel7;
        enemy_matrix[7] = enemyLevel8;
        enemy_matrix[8] = enemyLevel9;
        enemy_matrix[9] = enemyLevel10;
    }

    //add enemy to list
    private Enemy[] add_enemy_to_list(Enemy[] list, Enemy enemy) {
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
    public Enemy get_random_enemy() {
        //random value which determines which stage the enemy is in
        int stage = Random.Range(current_stage_bottom, current_stage_top + 1);

        //get list of enemies in stage
        Enemy[] enemyList = enemy_matrix[stage];

        //get length of list
        int n = enemyList.Length;
        
        //create example
        Enemy ex = enemyList[Random.Range(0, n)];

        //create enemy instance
        Enemy enemy = ScriptableObject.CreateInstance<Enemy>();

        //set values of instance based on example values
        enemy.set_name(ex.get_enemy_name());
        enemy.set_max_health(ex.get_max_health());
        enemy.set_damage(ex.get_damage());
        enemy.set_armor(ex.get_armor());
        enemy.set_crit_chance(ex.get_crit_chance());
        enemy.set_level(ex.get_level());
        enemy.set_heal(ex.get_heal());
        enemy.set_drop_chance(ex.get_drop_chance());
        enemy.set_drop_items(ex.get_drop_items());
        enemy.set_is_boss(ex.get_is_boss());

        return enemy;

    }

    //get bosses
    public Enemy[] get_random_boss_list(int player_amount) {
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
            boss.set_name(ex.get_enemy_name());
            boss.set_max_health(ex.get_max_health());
            boss.set_damage(ex.get_damage());
            boss.set_armor(ex.get_armor());
            boss.set_crit_chance(ex.get_crit_chance());
            boss.set_level(ex.get_level());
            boss.set_heal(ex.get_heal());
            boss.set_drop_chance(ex.get_drop_chance());
            boss.set_drop_items(ex.get_drop_items());
            boss.set_is_boss(ex.get_is_boss());

            //add boss to list
            bossList[i] = boss;
        }

        return bossList;
    }

    //next stage
    public void next_stage(int current_turn) {
        //check if current turn % next enemy list iteration = 0
        if (current_turn % turns_until_next_enemy_list_iteration == 0) {
            //increment current stage
            current_stage_bottom++;
            current_stage_top++;

            //check if current stage is greater than max enemy level
            if (current_stage_top > get_max_enemy_level()) {
                //set current stage to max enemy level
                current_stage_bottom = get_max_enemy_level();
                current_stage_top = get_max_enemy_level();
            }
        }
    }
 
}
