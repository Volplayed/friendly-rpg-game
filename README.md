# Friendly RPG
![GitHub last commit](https://img.shields.io/github/last-commit/Volplayed/friendly-rpg-game?style=flat-square)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/Volplayed/friendly-rpg-game?style=flat-square)
![GitHub repo size](https://img.shields.io/github/repo-size/Volplayed/friendly-rpg-game?style=flat-square)
![GitHub](https://img.shields.io/github/license/Volplayed/friendly-rpg-game?style=flat-square)

Turn-based RPG game for up to 4 players on one device.

## Description
The game is a turn-based RPG with a lot of different enemies, items, and bosses.
You can play with up to 4 players on one device.
The game has a lot of different mechanics, such as:
- **Movement** - you can move your character on the map.
- **Leveling** - you can level up your character by killing enemies.
- **Stats** - you can increase your stats by leveling up.
- **Items** - you can find items by killing enemies.
- **Equipment** - you can equip items to increase your stats.
- **Healing** - you can heal yourself.
- **Escape** - you can escape from the fight.
- **Bosses** - you can fight with bosses.
- **Win/Lose** - you can win or lose the game.
- **Game Stats** - you can see your game stats.

This project was greatly inspired by the [Pixel Dungeon](https://github.com/watabou/pixel-dungeon) game.

## Screenshots
<img src="./ReadmeImg/gameplay1.png" width=500> <img src="./ReadmeImg/gameplay2.png" width=500>
<img src="./ReadmeImg/gameplay3.png" width=500> <img src="./ReadmeImg/gameplay4.png" width=500>
<img src="./ReadmeImg/gameplay5.png" width=500> <img src="./ReadmeImg/gameplay6.png" width=500>
<img src="./ReadmeImg/gameplay7.png" width=500>

## Updates

Current version: **v1.2.0**

- **v1.2.0** - Added game statistics that hold information about all players.
- **v1.1.3** - Buff hunting bow, made tutorial look a bit better on some devices, moved all formulas to a separate file.
- **v1.1.2** - Added two new bosses, fixed tutorial problems.
- **v1.1.1d** - Changed code readability.
- **v1.1.1** - Added missing button click sounds, renamed player prefabs.
- **v1.1.0** - Sound options, player receives exp for escaping.
- **v1.0.0** - First release.

The game is using [Semantic Versioning](https://semver.org/).

## Contributing
Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

If you want to make your versions of the game some points might help.

Make sure you have [Unity](https://unity3d.com/get-unity/download) installed, as the game is made using Unity.

> If you want to add new enemies or items you need to know how to use Unity Editor, rebalancing for example can be done without Unity Editor only by changing values in the scripts.

### Creating new enemies
- Create an instance of a `ScriptableObject` `Enemy`.
    > You can create it in any folder you want, but 
    > it is recommended to save each enemy in the appropriate level folder.
- Set all enemy variables
    - `Name`
        > Try to make it not too long.
    - `Level` (recommended max level is 10)
    - `Health`
    - `Damage`
    - `Armor` (then multiplied by a coefficient)
        > You can see coefficients in [Coefficient.cs](./Assets/Scripts/Data/Coefficient.cs) script.
    - `Crit Chance`
    - `Drop Chance` (chance to drop an item)
    - `List of items`
        > You can add items to this list to make this enemy drop them.
        > Please note that each enemy except bosses must have at least one item in this list.
- If an enemy is a boss check the `is boss` value.
    > Bosses appear only at the end of the game.
- Add an enemy object to the `Enemies` list in the `EnemyList` prefab.
    > If the enemy is a boss add it to the `Bosses` list in the `EnemyList` prefab.

> For example, if you want to add a new level 5 enemy to the game, you need to create a new `ScriptableObject` `Enemy`, set its `name`, `level` to 5, `health`, `damage`, `armor`, `crit chance`, `drop chance`, and add it to the `Enemies` list in the `EnemyList` prefab.

### Creating new items
- Create an instance of a `ScriptableObject` `Item`.
    > You can create it in any folder you want, but
    > it is recommended to save each item in its type folder and sort in rarity folders.
- Set all variables
    - `Name`
    - `Description`
        > Long description may not fit in the item description window.
    - Create and add the item `Icon`
        > Icon should be 24x24 pixels.
        > You can use [this](https://www.piskelapp.com/p/create/sprite) website to create icons.
    - `Type` (Body, Head, Leg, Feet, Hands, Ring, Weapon)
    - `Rarity`
       - 0 - *Common*
       - 1 - *Uncommon*
       - 2 - *Rare*
       - 3 - *Epic*
       - 4 - *Legendary*
       - 5 - *Mythical*
    - Bonus variables.
        > For example, if the item is a weapon, you should set the `Damage` and `Crit Chance` variables.

> For example, if you want to add a new rare helmet to the game, you need to create a new `ScriptableObject` `Item`, set its `name`, `description`, set `type` value to head, `rarity` to 2 (rare), add `icon`, and set all bonuses this item gives the player.
> Then you need to add this item to the `List of items` in the enemy you want to drop this item.

### Rebalancing
- You can change all values in the scripts.
- You can change all values in the `ScriptableObject` `Enemy` and `Item` instances.
- You can change calculation formulas. 

You can **find all formulas** that are used for calculating values in [Formulas.cs](/Assets/Scripts/Data/Formulas.cs).
You can find **all variables** that are used for calculating values in [Coefficient.cs](/Assets/Scripts/Data/Coefficient.cs).

## Project status
This project is currently in a passive state. Occasionally new features and bug fixes will be added. Any ideas and suggestions are welcome.

## License
`Friendly RPG` is a free and open-source game licensed under [MIT](https://choosealicense.com/licenses/mit/).
All images and visuals were created by [Volodymyr Fedyniak](https://www.linkedin.com/in/volodymyr-fedyniak/) and distributed under a Creative Commons license ([CC BY-SA 4.0 International](https://creativecommons.org/licenses/by-sa/4.0/)).
