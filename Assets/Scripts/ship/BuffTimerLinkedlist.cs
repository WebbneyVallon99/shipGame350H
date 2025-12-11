using System;
using UnityEngine;

// Renamed the class as requested: BuffTimerLinkedlist
[Serializable]
public class BuffTimerLinkedlist
{
    // The type of powerup granted (e.g., Seagull, Wind)
    public PowerupType type;

    // The point in time when this buff should expire
    public float expirationTime;
    
    // An internal flag to track the state of the buff (currently unused but good for future expansion)
    public bool isBuffActive = true; 

    public BuffTimerLinkedlist(PowerupType type, float duration)
    {
        this.type = type;
        // Calculate the absolute time the buff should end
        this.expirationTime = Time.time + duration;
    }
}