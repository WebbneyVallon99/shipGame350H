using UnityEngine;
using System;
using System.Collections.Generic; // Required for Dictionary<TKey, TValue>

// Define a delegate (a method signature) for the powerup effect actions.
// This allows us to store different methods in the Dictionary.
public delegate void PowerupAction(PlayerShip player, float duration);

// This script will be a Singleton or attached to a Game Manager to be globally accessible.
public class PowerupRegistry : MonoBehaviour
{
    // The Hash Table (Dictionary) mapping the PowerupType (Key) to the action method (Value).
    private Dictionary<PowerupType, PowerupAction> powerupActions;

    void Awake()
    {
        // Initialize the Hash Table and map all the effects.
        InitializeRegistry();
    }

    private void InitializeRegistry()
    {
        if (powerupActions != null) return;

        powerupActions = new Dictionary<PowerupType, PowerupAction>();

        // We use an Action (delegate) that matches the PowerupAction signature:
        // (PlayerShip player, float duration) => { /* implementation */ }

        // --- MAP ALL EFFECTS ---
        
        // 1. WOOD PLANK (Heal - Instant Action, Duration is ignored)
        powerupActions.Add(PowerupType.WoodPlank, (player, duration) => 
        {
            player.Heal(1);
        });

        // 2. SEAGULL (Double Jump - Timed Action)
        powerupActions.Add(PowerupType.Seagull, (player, duration) => 
        {
            player.EnableDoubleJump(duration);
        });

        // 3. WIND (Speed Boost - Timed Action)
        powerupActions.Add(PowerupType.Wind, (player, duration) => 
        {
            player.ApplySpeedBoost(duration);
        });

        // 4. TREASURE CHEST (Mega Buff - Timed Action)
        powerupActions.Add(PowerupType.TreasureChest, (player, duration) => 
        {
            // The Treasure Chest executes multiple actions simultaneously
            player.ApplyInvincibility(duration);
            player.EnableDoubleJump(duration); 
            player.ApplySpeedBoost(duration); 
        });

        Debug.Log($"Powerup Registry initialized. {powerupActions.Count} actions mapped in Hash Table.");
    }
    
    /// <summary>
    /// Executes the action associated with the given PowerupType using the Hash Table.
    /// </summary>
    public void ExecutePowerup(PowerupType type, PlayerShip player, float duration)
    {
        // O(1) Lookup: Check if the action exists in the Dictionary.
        if (powerupActions.TryGetValue(type, out PowerupAction action))
        {
            // If found, execute the stored method immediately.
            action.Invoke(player, duration);
        }
        else
        {
            Debug.LogError($"Powerup type {type} not found in Hash Table registry!");
        }
    }
}
