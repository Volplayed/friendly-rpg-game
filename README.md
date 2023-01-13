# Friendly RPG
Turn-based RPG game for up to 4 players on one device.
This project was greatly inspired by the [Pixel Dungeon](https://github.com/watabou/pixel-dungeon) game.
## Screenshots
<img src="./ReadmeImg/gameplay1.png" width=500> <img src="./ReadmeImg/gameplay2.png" width=500>
<img src="./ReadmeImg/gameplay3.png" width=500> <img src="./ReadmeImg/gameplay4.png" width=500>
<img src="./ReadmeImg/gameplay5.png" width=500> <img src="./ReadmeImg/gameplay6.png" width=500>
<img src="./ReadmeImg/gameplay7.png" width=500>

## Installation
The game will be available in Google Play Market.

## Contributing
Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

If you want to make your own versions of the game there are some points that might help.


### Creating new enemies
- Create instance of an `ScriptableObject` `Enemy`.
    > You can create it in any folder you want, but 
    > it is recommended to save each enemy in the apropriate level folder.
- Set all enemy variables
    - `Name`
    - `Level` (max level is 10)
    - `Health`
    - `Damage`
    - `Armor` (then multiplied by a coefficient)
        > You can see coefficients in [Coefficient.cs](./Assets/Scripts/Data/Coefficient.cs) script.
    - `Crit Chance`
    - `Drop Chance` (chance to drop an item)
    - `List of items`
- If enemy is a boss check `is boss` value.
- Add enemy object to `Enemies` list in `EnemyList` prefab.
    > If enemy is a boss add it to `Bosses` list in `EnemyList` prefab.

After that everything should perfectly work.

### Creating new items
- Create instance of an `ScriptableObject` `Item`.
    > You can create it in any folder you want, but
    > it is recommended to save each item it's type folder and sort in rarity folders.
- Set all variables
    - `Name`
    - `Description`
    - Create and add item `Icon`
    - `Type` (Body, Head, Leg, Feet, Hands, Ring, Weapon)
    - `Rarity`
       - 0 - *Common*
       - 1 - *Uncommon*
       - 2 - *Rare*
       - 3 - *Epic*
       - 4 - *Legendary*
       - 5 - *Mythical*
    - Bonus variables.
After that item is ready to be added to enemy drop list.

### Rebalancing
- Go to [Coefficient.cs](./Assets/Scripts/Data/Coefficient.cs) script.
- Change the value you need
    ```
    //armor coefficient
    public static int armor;
    ```
    ```
    //health per strength
    public static int healthPerStrength;
    ```
    ```
    //armor per agility
    public static double armorPerAgility;
    ```
    ```
    //crit chance per intelligence
    public static double critChancePerIntelligence;
    ```
    ```
    //amount of agility per one move
    public static int agilityPerMove;
    ```
    ```
    //damage per level
    public static int damagePerLevel;
    ```
    ```
    //vs enemy escape chance
    public static double enemyLevelEscapeChance;
    public static double enemyStatsEscapeChance;
    ```
    ```
    //vs player escape chance
    public static double playerEnemyStatsEscapeChance;
    public static double playerStatsEscapeChance;
    ```
    ```
    //heal
    public static int intelligencePerHeal;
    public static int healApplication;
    ```
    ```
    //exp per level
    public static int expPerPlayerLevel;
    public static int expPerEnemyLevel;
    ```
    ```
    //exp per enemy level for escape
    public static int escapeExp
    ```
    ```
    //default chance of fight while moving
    public static double defaultFightChance;
    ```
#### Formulas
- Damage reduction
    ```
    //value - amount of damage incoming
    value * (100 - armor * Coefficient.armor)/100
    ```
- Health per strength
    ```
    strength * Coefficient.healthPerStrength
    ```
- Heal amount
    ```
    (intelligence / Coefficient.intelligencePerHeal) * Coefficient.healApplication + level
    ```
- Armor per agility
    ```
    agility * Coefficient.armorPerAgility
    ```
- Crit Chance per intelligence
    ```
    intelligence * Coefficient.critChancePerIntelligence
    ```
- Moves
    ```
    agility / Coefficient.agilityPerMove
    ```
- Damage
    ```
    level * Coefficient.damagePerLevel
    ```
- Escape chance vs enemy
    ```
    //k - player agility or intelligence
    k*Coefficient.enemyStatsEscapeChance - (level - enemy.get_level()) * Coefficient.enemyLevelEscapeChance;
    ```
- Escape chance vs player
    ```
    //k - player agility or intelligence
    k*Coefficient.playerStatsEscapeChance - other_agility* Coefficient.playerEnemyStatsEscapeChance
    ```

## License
`Friendly RPG` is free and open-source game licensed under the [MIT](https://choosealicense.com/licenses/mit/).
All images and visuals were created by Volodymyr Fedyniak and distributed under Creative Commons license ([CC BY-SA 4.0 International](https://creativecommons.org/licenses/by-sa/4.0/)).

