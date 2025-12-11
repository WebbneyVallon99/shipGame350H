using UnityEngine;
public enum PowerupType
{
    
    None = 0, // no power up
    WoodPlank = 1,      // Heals 1 HP (calls PlayerShip.Heal)
    Seagull = 2,        // Gives double jump (sets PlayerShip.CanDoubleJump = true)
    Wind = 3,           // Gives move speed (boosts PlayerShip.MoveSpeed)
    TreasureChest = 4   // Gives invincibility + Seagull + Wind (handles multiple effects)
}