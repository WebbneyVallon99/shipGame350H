using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Crucial for Score Text
using System.Linq; // Required for the UpdateBuffDisplay logic

public class GameUI : MonoBehaviour
{
    // --- DEPENDENCIES ---
    [Header("Dependencies")]
    [Tooltip("Link to the PlayerShip instance in the scene")]
    [SerializeField] private PlayerShip playerShip; 
    
    // --- RED BOX: HEALTH (Existing) ---
    [Header("1. Health UI (Red Box)")]
    [SerializeField] private GameObject heartPrefab; 
    [SerializeField] private Transform heartsContainer; // Top Left container
    private List<GameObject> heartIcons = new List<GameObject>();

    // --- BLUE BOX: SCORE (New) ---
    [Header("2. Score UI (Blue Box)")]
    [Tooltip("The TextMeshPro object displaying the score.")]
    [SerializeField] private TextMeshProUGUI scoreText; // Top Right

    // --- YELLOW BOX: POWERUPS (New) ---
    [Header("3. Powerup UI (Yellow Box)")]
    [Tooltip("The visual prefab for a single powerup icon (e.g., Seagull sprite).")]
    [SerializeField] private GameObject buffIconPrefab; 
    [Tooltip("The container for the powerup icons (below the hearts).")]
    [SerializeField] private Transform buffIconsContainer; // Below Red Box
    
    // Internal dictionary to track instantiated buff icons by type
    // We use a Dictionary (Hash Table) here for efficient icon management
    private Dictionary<PowerupType, GameObject> activeBuffIcons = new Dictionary<PowerupType, GameObject>();
    
    // Placeholder score (will be updated by the final Scoring System script)
    private int currentScore = 0; 
    
    void Start()
    {
        // Safety check for PlayerShip
        if (playerShip == null)
        {
            playerShip = FindObjectOfType<PlayerShip>();
            if (playerShip == null)
            {
                Debug.LogError("GameUI cannot find PlayerShip in the scene!", this);
                return;
            }
        }
        
        // Initialize all UI elements
        InitializeHearts();
        UpdateScoreDisplay(0); // Initialize score to zero
    }

    void Update()
    {
        // Polling loop for all dynamic UI elements
        UpdateHeartDisplay();
        UpdateBuffDisplay(); // Reads the PlayerShip's Linked List
        // UpdateScoreDisplay(currentScore); // Uncomment when score logic is ready
    }

    // =========================================================================
    // 1. HEALTH UI (RED BOX)
    // =========================================================================

    private void InitializeHearts()
    {
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }
        heartIcons.Clear();

        for (int i = 0; i < playerShip.MaxHp; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            heartIcons.Add(newHeart);
        }

        UpdateHeartDisplay();
    }

    private void UpdateHeartDisplay()
    {
        for (int i = 0; i < heartIcons.Count; i++)
        {
            // If the index (i) is less than the current health, the heart is visible.
            heartIcons[i].SetActive(i < playerShip.CurrentHp);
        }
    }

    // =========================================================================
    // 2. SCORE UI (BLUE BOX)
    // =========================================================================

    /// <summary>
    /// Updates the displayed score in the top right corner.
    /// </summary>
    public void UpdateScoreDisplay(int newScore)
    {
        currentScore = newScore;
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {currentScore:D6}"; // D6 ensures leading zeros
        }
    }

    // =========================================================================
    // 3. POWERUP UI (YELLOW BOX)
    // =========================================================================

    /// <summary>
    /// Reads the PlayerShip's Linked List (ActiveBuffs) and creates/removes icons dynamically.
    /// </summary>
    private void UpdateBuffDisplay()
    {
        // PlayerShip.ActiveBuffs is the public property exposing the Linked List
        if (playerShip.ActiveBuffs == null) return;
        
        // --- 1. HANDLE ADDITIONS/UPDATES ---
        // Check for buffs that are active but don't have an icon yet
        foreach (var buff in playerShip.ActiveBuffs)
        {
            PowerupType type = buff.type;
            
            // Only display icons for active, time-based buffs (not instant heals)
            if (type == PowerupType.WoodPlank) continue;
            
            if (!activeBuffIcons.ContainsKey(type))
            {
                // Buff is new: Create a new icon
                GameObject newIcon = Instantiate(buffIconPrefab, buffIconsContainer);
                
                // Set the name for debugging (you'll later set the sprite here)
                newIcon.name = $"Icon_{type}";
                
                activeBuffIcons.Add(type, newIcon);
            }
            
            // Future step: Update the timer text on the icon here
        }

        // --- 2. HANDLE REMOVALS ---
        // Find icons that exist in the UI Dictionary but have expired (removed from the Linked List)
        List<PowerupType> typesToRemove = new List<PowerupType>();

        // Find icons that exist in the UI but are no longer in the PlayerShip's Linked List
        foreach (var kvp in activeBuffIcons)
        {
            PowerupType type = kvp.Key;
            
            // Check if the current icon type is NOT present in the player's active buffs
            if (!playerShip.ActiveBuffs.Any(b => b.type == type))
            {
                typesToRemove.Add(type);
                Destroy(kvp.Value); // Destroy the actual UI icon object
            }
        }
        
        // Remove the entries from the tracking dictionary
        foreach (var type in typesToRemove)
        {
            activeBuffIcons.Remove(type);
        }
    }
}