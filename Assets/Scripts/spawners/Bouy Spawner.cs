using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Required for Queue<T>
using Random = UnityEngine.Random; // Use Unity's Random class
using System; 

public class BuoySpawner : MonoBehaviour
{
    [Header("Buoy Prefab")]
    [SerializeField] private BuoyHazard buoyPrefab;

    [Header("Random Wave Settings")]
    [Tooltip("Minimum number of buoys to spawn in a single wave (e.g., 1)")]
    [SerializeField] private int minBuoysPerWave = 1; 
    [Tooltip("Maximum number of buoys to spawn in a single wave (e.g., 3)")]
    [SerializeField] private int maxBuoysPerWave = 3;
    
    [Tooltip("Minimum time delay between waves.")]
    [SerializeField] private float minWaveDelay = 3.0f;
    [Tooltip("Maximum time delay between waves.")]
    [SerializeField] private float maxWaveDelay = 5.0f;
    
    [Tooltip("The fixed time delay between spawning individual buoys within a wave.")]
    [SerializeField] private float delayBetweenBuoysInWave = 0.5f;

    [Header("Fixed Position")]
    [Tooltip("The vertical position where all buoys float.")]
    [SerializeField] private float spawnYPosition = -4.5f;
    [Tooltip("The horizontal position far right (off-screen)")]
    [SerializeField] private float spawnXPosition = 10.0f; 

    // The Queue data structure to hold the specific delay until the NEXT wave spawns
    // Each float represents the delay *after* the current wave is finished.
    private Queue<float> waveDelayQueue;

    void Start()
    {
        if (buoyPrefab == null)
        {
            Debug.LogError("Buoy Prefab is not linked in the Spawner!");
            return;
        }

        // 1. Build the initial queue of random wave delays and start executing
        StartCoroutine(ExecuteSpawnPattern());
    }

    /// <summary>
    /// Coroutine that runs continuously, executing random wave spawns.
    /// This method implicitly uses a dynamic pattern (a queue of random events).
    /// </summary>
    private IEnumerator ExecuteSpawnPattern()
    {
        int waveCount = 0;
        
        while (true) // Keep the spawning continuous
        {
            waveCount++;
            
            // --- 1. RANDOMIZE AND SPAWN THE WAVE ---
            int buoysInWave = Random.Range(minBuoysPerWave, maxBuoysPerWave + 1);
            
            Debug.Log($"--- Starting Wave {waveCount}: Spawning {buoysInWave} buoys. ---");

            for (int i = 0; i < buoysInWave; i++)
            {
                // Instantiate the buoy at the fixed X/Y position
                Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0);
                Instantiate(buoyPrefab, spawnPosition, Quaternion.identity);
                
                // Delay between each individual buoy in the same wave
                yield return new WaitForSeconds(delayBetweenBuoysInWave); 
            }
            
            // --- 2. DETERMINE NEXT WAVE DELAY AND WAIT ---
            
            // Determine a random delay for the NEXT wave
            float nextWaveDelay = Random.Range(minWaveDelay, maxWaveDelay);
            
            Debug.Log($"Wave {waveCount} complete. Waiting for {nextWaveDelay:F2} seconds until next wave.");
            
            // Wait for the random delay time before the loop repeats
            yield return new WaitForSeconds(nextWaveDelay);
        }
    }
}