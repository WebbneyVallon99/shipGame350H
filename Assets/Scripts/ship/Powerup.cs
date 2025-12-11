//  it works version 1
// using UnityEngine;
// using System.Collections;
// // Assumes ICollectible and PowerupType are correctly defined

// public class Powerup : MonoBehaviour, ICollectible
// {
//     // --- IDENTITY & MOVEMENT FIELDS (Existing) ---
//  [SerializeField] private float powerUpDuration = 5.0f; 
//  [SerializeField] private float moveSpeedIncrease = 4.0f;
//  [SerializeField] private PowerupType type; 
//  [SerializeField] private float destroyYPosition = -6.0f;
    
//     // --- NEW: SPRITE REFERENCES ---
//     [Header("Visuals")]
//     [SerializeField] private Sprite plankSprite;
//     [SerializeField] private Sprite seagullSprite;
//     [SerializeField] private Sprite windSprite;
//     [SerializeField] private Sprite chestSprite;
    
//     private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
// void Start()
// {
//         // 1. Get the SpriteRenderer component (Must be present on this GameObject)
//         spriteRenderer = GetComponent<SpriteRenderer>();

//         // 2. Assign the correct visual based on the selected PowerupType
//         AssignSpriteBasedOnType();
// }

//     // --- NEW: SPRITE ASSIGNMENT LOGIC ---
//     private void AssignSpriteBasedOnType()
//     {
//         if (spriteRenderer == null) return; // Safety check

//         switch (type)
//         {
//             case PowerupType.WoodPlank:
//                 spriteRenderer.sprite = plankSprite;
//                 break;
//             case PowerupType.Seagull:
//                 spriteRenderer.sprite = seagullSprite;
//                 break;
//             case PowerupType.Wind:
//                 spriteRenderer.sprite = windSprite;
//                 break;
//             case PowerupType.TreasureChest:
//                 spriteRenderer.sprite = chestSprite;
//                 break;
//             default:
//                 spriteRenderer.sprite = null;
//                 break;
//         }
//     }

//     // Update is called once per frame
//  void Update()
// {
//         // Moves the powerup object downward
// transform.Translate(Vector2.down * moveSpeedIncrease * Time.deltaTime);

//         // Despawn check: check if it has moved below the destroyYPosition
// if (transform.position.y < destroyYPosition)
// {
// Destroy(gameObject);
// }
// }

//  public void OnCollected(PlayerShip player)
// {
// Debug.Log($"Powerup of type {type} collected!");

//         // 1. Apply the powerup effect using a Coroutine on the PlayerShip
//  player.StartCoroutine(ApplyEffect(player));

//         // 2. Remove the collected item from the scene
//  Destroy(gameObject);
//  }

//  private IEnumerator ApplyEffect(PlayerShip player)
//  {
//         // 1. Handle the effect based on the PowerupType
//  switch (type)
//  {
//  case PowerupType.WoodPlank:
// player.Heal(1); 
// break;
//  case PowerupType.Seagull:
//  player.StartCoroutine(player.EnableDoubleJump(powerUpDuration));
// break;
// case PowerupType.Wind:
// player.StartCoroutine(player.ApplySpeedBoost(powerUpDuration));
// break;
// case PowerupType.TreasureChest:
// player.StartCoroutine(player.ApplyInvincibility(powerUpDuration));
//  player.StartCoroutine(player.EnableDoubleJump(powerUpDuration)); 
// player.StartCoroutine(player.ApplySpeedBoost(powerUpDuration)); 
// break;
// }

// yield break;
//     }
// }

// using UnityEngine;
// using System.Collections; // Kept for legacy, though not strictly needed here
// // Assumes ICollectible and PowerupType are correctly defined

// public class Powerup : MonoBehaviour, ICollectible
// {
//     // --- IDENTITY & MOVEMENT FIELDS (Existing) ---
//     [SerializeField] private float powerUpDuration = 5.0f; 
//     [SerializeField] private float moveSpeedIncrease = 4.0f;
//     [SerializeField] private PowerupType type; 
//     [SerializeField] private float destroyYPosition = -6.0f;
    
//     // --- SPRITE REFERENCES ---
//     [Header("Visuals")]
//     [SerializeField] private Sprite plankSprite;
//     [SerializeField] private Sprite seagullSprite;
//     [SerializeField] private Sprite windSprite;
//     [SerializeField] private Sprite chestSprite;
    
//     private SpriteRenderer spriteRenderer; 
    
//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         AssignSpriteBasedOnType();
//     }

//     private void AssignSpriteBasedOnType()
//     {
//         if (spriteRenderer == null) return; 

//         switch (type)
//         {
//             case PowerupType.WoodPlank:
//                 spriteRenderer.sprite = plankSprite;
//                 break;
//             case PowerupType.Seagull:
//                 spriteRenderer.sprite = seagullSprite;
//                 break;
//             case PowerupType.Wind:
//                 spriteRenderer.sprite = windSprite;
//                 break;
//             case PowerupType.TreasureChest:
//                 spriteRenderer.sprite = chestSprite;
//                 break;
//             default:
//                 spriteRenderer.sprite = null;
//                 break;
//         }
//     }

//     void Update()
//     {
//         // Moves the powerup object downward
//         transform.Translate(Vector2.down * moveSpeedIncrease * Time.deltaTime);

//         // Despawn check
//         if (transform.position.y < destroyYPosition)
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void OnCollected(PlayerShip player)
//     {
//         Debug.Log($"Powerup of type {type} collected!");

//         // 1. Apply the powerup effect using the new, instant ApplyEffect method
//         ApplyEffect(player);

//         // 2. Remove the collected item from the scene
//         Destroy(gameObject);
//     }

//     // NOTE: This is now a regular method, NOT a Coroutine, as the buff timing is
//     // now managed by the PlayerShip's Linked List.
//     private void ApplyEffect(PlayerShip player)
//     {
//         switch (type)
//         {
//             case PowerupType.WoodPlank:
//                 // Heal is instant, not time-based
//                 player.Heal(1); 
//                 break;
//             case PowerupType.Seagull:
//                 // Calls the new method which registers the buff in the Linked List
//                 player.EnableDoubleJump(powerUpDuration);
//                 break;
//             case PowerupType.Wind:
//                 // Calls the new method which registers the buff in the Linked List
//                 player.ApplySpeedBoost(powerUpDuration);
//                 break;
//             case PowerupType.TreasureChest:
//                 // Calls the new methods which register the buff in the Linked List
//                 player.ApplyInvincibility(powerUpDuration);
//                 player.EnableDoubleJump(powerUpDuration); 
//                 player.ApplySpeedBoost(powerUpDuration); 
//                 break;
//         }
//     }
// }

using UnityEngine;
using System.Collections; 
// Assumes ICollectible and PowerupType are correctly defined

public class Powerup : MonoBehaviour, ICollectible
{
    // --- IDENTITY & MOVEMENT FIELDS (Existing) ---
    [SerializeField] private float powerUpDuration = 5.0f; 
    [SerializeField] private float moveSpeedIncrease = 4.0f;
    [SerializeField] private PowerupType type; 
    [SerializeField] private float destroyYPosition = -6.0f;
    
    // --- SPRITE REFERENCES ---
    [Header("Visuals")]
    [SerializeField] private Sprite plankSprite;
    [SerializeField] private Sprite seagullSprite;
    [SerializeField] private Sprite windSprite;
    [SerializeField] private Sprite chestSprite;
    
    private SpriteRenderer spriteRenderer; 
    
    // --- NEW: HASH TABLE REGISTRY REFERENCE ---
    private PowerupRegistry registry; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AssignSpriteBasedOnType();
        
        // Find the globally accessible registry on start
        registry = FindObjectOfType<PowerupRegistry>(); 
        if (registry == null)
        {
            Debug.LogError("PowerupRegistry not found in scene! Cannot execute powerup effects.");
        }
    }

    private void AssignSpriteBasedOnType()
    {
        if (spriteRenderer == null) return; 

        switch (type)
        {
            case PowerupType.WoodPlank:
                spriteRenderer.sprite = plankSprite;
                break;
            case PowerupType.Seagull:
                spriteRenderer.sprite = seagullSprite;
                break;
            case PowerupType.Wind:
                spriteRenderer.sprite = windSprite;
                break;
            case PowerupType.TreasureChest:
                spriteRenderer.sprite = chestSprite;
                break;
            default:
                spriteRenderer.sprite = null;
                break;
        }
    }

    void Update()
    {
        // Moves the powerup object downward
        transform.Translate(Vector2.down * moveSpeedIncrease * Time.deltaTime);

        // Despawn check
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollected(PlayerShip player)
    {
        Debug.Log($"Powerup of type {type} collected!");

        // 1. Apply the powerup effect using the Hash Table lookup
        ApplyEffect(player);

        // 2. Remove the collected item from the scene
        Destroy(gameObject);
    }

    /// <summary>
    /// Executes the powerup action using the O(1) Hash Table lookup.
    /// This replaces the complex switch statement.
    /// </summary>
    private void ApplyEffect(PlayerShip player)
    {
        if (registry != null)
        {
            // Delegate execution to the registry
            registry.ExecutePowerup(type, player, powerUpDuration);
        }
    }
}