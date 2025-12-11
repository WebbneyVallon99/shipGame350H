// using UnityEngine;

// public class BouyHazard : MonoBehaviour, IDamageable
// {   
//     [SerializeField] private int damageValue = 1;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void OnCollisionEnter2D(Collision2D detectedCollision){}
//     public void TakeDamage(int amount)
//     {
//         Debug.Log($"Bouy has been hit. Deleting from game.");
//         Destroy (gameObject);
//     }
// }

// using UnityEngine;
// using System.Collections; 

// public class BuoyHazard : MonoBehaviour, IDamageable
// { 	
//     // --- HAZARD/DAMAGE FIELDS ---
//  	[SerializeField] private int damageValue = 1;

//     // --- MOVEMENT FIELDS ---
//     [Header("Movement")]
//     [SerializeField] private float buoySpeed = 3.0f; 
//     [SerializeField] private float destroyXPosition = -10.0f; 

//     // --- TEMPORARY SPAWNING FIELDS (Updated for Cluster Logic) ---
//     [Header("Temporary Spawning")]
//     [SerializeField] private BuoyHazard buoyPrefab;
//     [SerializeField] private float spawnInterval = 3.0f; 
//     [SerializeField] private float horizontalSpacing = 1.2f; 
//     [SerializeField] private float spawnYPosition = -4.5f; 
//     [SerializeField] private float spawnXPosition = 10.0f; 
    
//  	void Start()
//  	{
//         if (buoyPrefab != null)
//         {
//             // Start the spawning coroutine
//             StartCoroutine(SpawnBuoysRepeatedly());
//         }
//  	}

//  	void Update()
//  	{
//         // Right-to-Left Movement Logic
//         transform.Translate(Vector2.left * buoySpeed * Time.deltaTime);

//         // Check if Buoy is Off-Screen
//         if (transform.position.x < destroyXPosition)
//         {
//             Destroy(gameObject);
//         }
//  	}

//     // --- TEMPORARY CLUSTER SPAWNING COROUTINE ---
//     private IEnumerator SpawnBuoysRepeatedly()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(spawnInterval);

//             // 1. Randomly decide how many buoys (1, 2, or 3) to spawn in this cluster
//             // We use the full namespace to ensure the correct Random class is used.
//             int buoyCount = 1; 
            
//             // >>>>>>>>>>>>>>>>> DEBUGGING LINE <<<<<<<<<<<<<<<<<
//             // CHECK THE CONSOLE: Does this number frequently show 1 or 2, or mostly 3?
//             Debug.Log($"Spawning a cluster of {buoyCount} buoys.");
//             // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            
//             // 2. Calculate the total width of the cluster (not strictly needed, but kept)
//             // float clusterWidth = (buoyCount - 1) * horizontalSpacing;
            
//             // 3. Define the start position
//             Vector3 startPosition = new Vector3(spawnXPosition, spawnYPosition, 0);

//             // 4. Loop to spawn each buoy in the cluster
//             for (int i = 0; i < buoyCount; i++)
//             {
//                 // Calculate the horizontal offset for the current buoy
//                 float offsetX = i * horizontalSpacing;
                
//                 // Final spawn position: offset horizontally from the start position
//                 Vector3 spawnPosition = startPosition + new Vector3(offsetX, 0, 0);

//                 // Instantiate the buoy
//                 Instantiate(buoyPrefab, spawnPosition, Quaternion.identity);
//             }
//         }
//     }

//     // --- IDamageable Implementation ---
//  	public void TakeDamage(int amount)
//  	{
//  	 	Debug.Log($"Buoy has been hit. Deleting from game.");
//  	}

//  	private void OnCollisionEnter2D(Collision2D detectedCollision)
//     {
//         // 1. Safely check if the object we collided with is damageable.
//     // TryGetComponent attempts to get the IDamageable component (like on the PlayerShip)
//     // and returns true only if it finds one, storing it in the 'damageable' variable.
//     if (detectedCollision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
//     {
//         // 2. Send the damage command to the target.
//         // We call the TakeDamage method on the detected object, using the Buoy's stored damageValue.
//         damageable.TakeDamage(damageValue);

//         // 3. Destroy the Buoy that was collided with.
//         // Once the damage is dealt, the hazard is removed from the scene.
//         // Since this script is attached to the Buoy, 'gameObject' refers to the Buoy itself.
//         Destroy(gameObject);
//     }

//     }
// }

// using UnityEngine;
// using System.Collections; 
// // assumes IDamageable interface is defined

// public class BuoyHazard : MonoBehaviour, IDamageable
// {   
//     // --- HAZARD/DAMAGE FIELDS ---
//  	[SerializeField] private int damageValue = 1;

//     // --- MOVEMENT FIELDS ---
//     [Header("Movement")]
//     [SerializeField] private float buoySpeed = 3.0f; 
//     [SerializeField] private float destroyXPosition = -10.0f; 

//     // --- TEMPORARY SPAWNING FIELDS (Updated for Cluster Logic) ---
//     [Header("Temporary Spawning")]
//     [SerializeField] private BuoyHazard buoyPrefab;
//     [SerializeField] private float spawnInterval = 3.0f; 
//     [SerializeField] private float horizontalSpacing = 1.2f; 
//     [SerializeField] private float spawnYPosition = -4.5f; 
//     [SerializeField] private float spawnXPosition = 10.0f; 
    
//  	void Start()
//  	{
//        // FOR TESTING: Comment out this line to ensure only ONE buoy exists at a time.
//         if (buoyPrefab != null)
//         {
//             StartCoroutine(SpawnBuoysRepeatedly());
//         }
//  	}

//  	void Update()
//  	{
//         // Right-to-Left Movement Logic
//         transform.Translate(Vector2.left * buoySpeed * Time.deltaTime);

//         // Check if Buoy is Off-Screen (This is an alternative destruction method)
//         if (transform.position.x < destroyXPosition)
//         {
//             Destroy(gameObject);
//         }
//  	}

//     // --- TEMPORARY CLUSTER SPAWNING COROUTINE (Commented out in Start()) ---
//     private IEnumerator SpawnBuoysRepeatedly()
//     {
//         // ... (This logic will only run if uncommented in Start())
//         while (true)
//         {
//             yield return new WaitForSeconds(spawnInterval);
//             int buoyCount = 1; // Testing with one buoy for now
            
//             Debug.Log($"Spawning a cluster of {buoyCount} buoys.");

//             Vector3 startPosition = new Vector3(spawnXPosition, spawnYPosition, 0);

//             for (int i = 0; i < buoyCount; i++)
//             {
//                 float offsetX = i * horizontalSpacing;
//                 Vector3 spawnPosition = startPosition + new Vector3(offsetX, 0, 0);
//                 Instantiate(buoyPrefab, spawnPosition, Quaternion.identity);
//             }
//         }
//     }

//     // --- IDamageable Implementation (Buoy is destroyed if *shot* by player, not primary interaction) ---
//  	public void TakeDamage(int amount)
//  	{
//         // Since the Buoy's primary role is to DEAL damage, we just destroy it if anything else damages it.
//  	 	Debug.Log($"Buoy hit by external force. Removing.");
//         Destroy(gameObject);
//  	}

//     // --- COLLISION LOGIC (Hazard-to-Player Interaction) ---
//  	public void OnCollisionEnter2D(Collision2D detectedCollision)
//     {
//         // 1. Safely check if the object we collided with is damageable (like the PlayerShip)
//         Debug.Log("Buoy collided with something. Checking for IDamageable..."); // Check 1
        
//         if (detectedCollision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
//         {
//             // 2. Send the damage command (HP reduction)
//             damageable.TakeDamage(damageValue);
            
//             // 3. Destroy the Buoy that was collided with (Addressing Buoy Despawn)
//             Debug.Log("Target found and damaged. Buoy destroying itself."); // Check 2
//             Destroy(gameObject);
//         }
//         else
//         {
//             Debug.Log("Buoy hit a non-damageable object. Ignoring damage command.");
//         }
//     }
// }

using UnityEngine;
using System.Collections; 
// assumes IDamageable interface is defined

public class BuoyHazard : MonoBehaviour, IDamageable
{   
    // --- HAZARD/DAMAGE FIELDS ---
 	[SerializeField] private int damageValue = 1;

    // --- MOVEMENT FIELDS ---
    [Header("Movement")]
    [SerializeField] private float buoySpeed = 3.0f; 
    [SerializeField] private float destroyXPosition = -10.0f; 
    
    // NOTE: All spawning-related fields and methods have been removed to prevent conflicts
    
 	void Start()
 	{
        // Must be empty or contain initialization specific to the BUOY itself.
 	}

 	void Update()
 	{
        // Right-to-Left Movement Logic
        transform.Translate(Vector2.left * buoySpeed * Time.deltaTime);

        // Check if Buoy is Off-Screen
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
 	}

    // --- IDamageable Implementation ---
 	public void TakeDamage(int amount)
 	{
 	 	Debug.Log($"Buoy hit by external force. Removing.");
        Destroy(gameObject);
 	}

    // --- COLLISION LOGIC ---
 	private void OnCollisionEnter2D(Collision2D detectedCollision)
    {
        // If the object hit is damageable (the PlayerShip)
        if (detectedCollision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damageValue);
            Destroy(gameObject); // Destroy the buoy itself
        }
    }
}