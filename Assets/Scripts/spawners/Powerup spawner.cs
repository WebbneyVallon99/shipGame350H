using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Required for Stack<T> and List
using Random = UnityEngine.Random; 

public class PowerupSpawner : MonoBehaviour
{
    [Header("Powerup Prefabs")]
    [Tooltip("Drag ALL unique Powerup Prefabs (Seagull, Plank, Wind, Chest) into this array.")]
    [SerializeField] private Powerup[] powerupPrefabs; 

    [Header("Stack Settings")]
    [Tooltip("The total number of powerups to spawn this level before the stack is empty.")]
    [SerializeField] private int maxPowerupsPerLevel = 10;
    
    // --- NEW: THE STACK DATA STRUCTURE ---
    // Stores the specific prefab assets in the order they will be spawned.
    private Stack<Powerup> powerupStack = new Stack<Powerup>();

    [Header("Spawn Settings")]
    [Tooltip("Minimum time delay between powerup spawns.")]
    [SerializeField] private float minSpawnDelay = 10.0f;
    [Tooltip("Maximum time delay between powerup spawns.")]
    [SerializeField] private float maxSpawnDelay = 20.0f;
    
    [Header("Position")]
    [Tooltip("Fixed starting Y position (top of screen)")]
    [SerializeField] private float spawnYPosition = 6.0f; 
    [Tooltip("Left boundary for random X spawn position")]
    [SerializeField] private float spawnXMin = -8.0f;     
    [Tooltip("Right boundary for random X spawn position")]
    [SerializeField] private float spawnXMax = 8.0f;      

    void Start()
    {
        if (powerupPrefabs == null || powerupPrefabs.Length == 0)
        {
            Debug.LogError("Powerup Prefabs array is empty or not linked in the Spawner!");
            return;
        }

        // 1. BUILD THE STACK: Randomly select and fill the stack with the level limit.
        BuildPowerupStack();

        // 2. Start the scheduled spawning loop
        StartCoroutine(SpawnLoop());
        Debug.Log($"Powerup Spawner initialized. Stack built with {powerupStack.Count} items.");
    }
    
    /// <summary>
    /// Fills the internal stack with a randomized selection of powerups up to the max limit.
    /// </summary>
    private void BuildPowerupStack()
    {
        // Use a List for intermediate random selection to make it easier
        List<Powerup> selectionList = new List<Powerup>();

        for (int i = 0; i < maxPowerupsPerLevel; i++)
        {
            // Select a random powerup prefab from the provided array
            int randomIndex = Random.Range(0, powerupPrefabs.Length);
            selectionList.Add(powerupPrefabs[randomIndex]);
        }
        
        // Push the items onto the Stack in reverse order of selection (or just sequentially).
        // The simple way is to push them directly onto the stack.
        // If we wanted to preserve the random order for spawning, we push them in the order selected.
        foreach (Powerup prefab in selectionList)
        {
            powerupStack.Push(prefab);
        }
        
        // NOTE: If you wanted a different random order (not LIFO), we would shuffle the list before pushing.
    }

    /// <summary>
    /// Coroutine that runs continuously until the stack is empty.
    /// </summary>
    private IEnumerator SpawnLoop()
    {
        int spawnCount = 0;
        
        // Loop runs ONLY while the stack has items remaining
        while (powerupStack.Count > 0) 
        {
            // 1. DETERMINE NEXT SPAWN DELAY AND WAIT
            float nextSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            
            Debug.Log($"Waiting for {nextSpawnDelay:F2} seconds until next powerup spawn. {powerupStack.Count} remaining.");
            yield return new WaitForSeconds(nextSpawnDelay);
            
            spawnCount++;
            
            // 2. RETRIEVE NEXT PREFAB USING POP()
            // This is the Stack operation: removes and returns the item from the top.
            Powerup selectedPrefab = powerupStack.Pop();
            
            // 3. DETERMINE RANDOM POSITION
            float randomX = Random.Range(spawnXMin, spawnXMax);
            Vector3 spawnPosition = new Vector3(randomX, spawnYPosition, 0);

            // 4. INSTANTIATE
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            
            Debug.Log($"Spawn #{spawnCount}: Popped and Spawned {selectedPrefab.GetType().Name} at X: {randomX:F2}");
        }
        
        Debug.Log("Powerup stack is empty. Spawning ceased until next level.");
    }
}
