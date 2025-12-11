// // test code 2 it works
// using UnityEngine;
// using System.Collections; // Required for Coroutines and Heal logic
// // Assumes IMovable interface is defined and implemented

// public class PlayerShip : MonoBehaviour, IMoveable, IDamageable
// {
//     private Rigidbody2D rb;
//     private Collider2D catchCollider;
    
//     [Header("Ship Stats")]
//     [SerializeField] private int MaxHp = 10;
//     private int CurrentHp;
//     [SerializeField] private float MoveSpeed = 15.0f;
//     [SerializeField] private float JumpForce = 10.0f;
    
//     // --- JUMP REFINEMENT FIELDS ---
//     [Header("Jump Refinement")]
//     [SerializeField] private Transform groundCheckPoint; 
//     [SerializeField] private float groundCheckRadius = 0.1f;
//     [SerializeField] private LayerMask groundLayer; 
    
//     // --- JUMP LOGIC VARIABLES ---
//     private bool CanDoubleJump;
//     private bool isInvincible; 
//     private bool isCurrentlyDoubleJumping = false; 
//     // This is the source of jumps (initialized to 1 jump)
//     private int jumpsAvailable = 1; 
    
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         CurrentHp = MaxHp; 
//     }

//    void Update()
// {
//     // Only reset jumps when ACTUALLY landing (falling or stationary)
//     if (IsGrounded() && rb.linearVelocity.y <= 0f)
//     {
//         jumpsAvailable = CanDoubleJump ? 2 : 1;
//         isCurrentlyDoubleJumping = false;
//     }

//     // Safety clamp
//     if (!CanDoubleJump && jumpsAvailable > 1)
//     {
//         jumpsAvailable = 1;
//     }
// }

//     // --- NEW: IS GROUNDED CHECK ---
//    public bool IsGrounded()
// {
//     if (groundCheckPoint == null) return false; // was incorrectly true
//     return Physics2D.OverlapCircle(
//         groundCheckPoint.position,
//         groundCheckRadius,
//         groundLayer
//     );
// }
    
//     // --- MOVEMENT ---

//     public void Move(float x)
//     {
//         if (rb == null) return;
//         float targetVelocityX = x * MoveSpeed;
//         rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);
//     }
    
//     // --- REFINED JUMP METHOD ---
//     public void Jump()
//     {
//         if (rb == null) return;
        
//         // >>>>>>>>>>>>>> DEBUGGING LINE <<<<<<<<<<<<<<<<
//         Debug.Log($"Jump Attempted. Jumps Available BEFORE check: {jumpsAvailable}");
//         // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        
//         // --- FINAL, ABSOLUTE FIX: ENFORCE SINGLE JUMP IF POWERUP IS OFF ---
//         // This is a redundant check, but it guarantees the counter is 1 or 0 if CanDoubleJump is false.
//         if (!CanDoubleJump && jumpsAvailable > 1)
//         {
//              jumpsAvailable = 1;
//         }


//         // --- CORE JUMP CONDITION CHECK ---
//         if (jumpsAvailable > 0)
//         {
//             // 1. Reset Vertical Velocity: Stop any current falling motion before jumping
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 

//             // 2. Perform the jump force application
//             Vector2 jumpVector = Vector2.up * JumpForce;
//             rb.AddForce(jumpVector, ForceMode2D.Impulse);
            
//             // 3. Decrease Jump Count
//             jumpsAvailable--;
            
//             // 4. Double Jump Feedback (Optional debug check)
//             if (CanDoubleJump && jumpsAvailable == 0 && !IsGrounded())
//             {
//                 isCurrentlyDoubleJumping = true;
//                 Debug.Log("Performed Double Jump!");
//             }
            
//             // --- NEW: SECOND FIX ATTEMPT (Removing this line as it was confusing the counter) ---
//             /*
//             if (!CanDoubleJump && jumpsAvailable == 1)
//             {
//                 jumpsAvailable = 0;
//             }
//             */
            
//         }
//         else
//         {
//             Debug.Log("No jumps available.");
//         }
//     }
    
//     // --- DAMAGE/HEALTH ---

//     public void TakeDamage (int amount)
//     {
//         if (isInvincible)
//         {
//             Debug.Log("Hit detected, but player is invincible! Damage ignored.");
//             return; 
//         }
        
//         CurrentHp -= amount;

//         Debug.Log($"OUCH! PlayerShip hit for {amount} damage. Remaining HP: {CurrentHp}");

//         if (CurrentHp <= 0)
//         {
//             CurrentHp = 0; 
//             Debug.Log("SHIP DESTROYED! Game Over condition met (Die() deferred).");
//             Die();
//         }
//     }
    
//     private void Die()
//     {
//         Debug.Log("Die() method called. Player ship destroyed!");
//         Destroy(gameObject);
//     }

//     // --- COLLECTION/POWERUPS ---
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
//         {
//             collectible.OnCollected(this);
//         }
//     }

//     public void Heal(int amount)
//     {
//         CurrentHp = Mathf.Min(CurrentHp + amount, MaxHp);
//         Debug.Log($"Healed {amount} HP. Current HP: {CurrentHp}");
//     }
    
//     public IEnumerator EnableDoubleJump(float duration)
//     {
//         Debug.Log("Powerup: Double Jump Enabled.");
//         CanDoubleJump = true;
        
//         // FIX: If player is mid-air and collects the Seagull, grant the second jump immediately.
//         if (jumpsAvailable == 0 && !IsGrounded()) 
//         {
//              jumpsAvailable = 1; 
//         }
        
//         yield return new WaitForSeconds(duration);
        
//         CanDoubleJump = false;
        
//         // FIX: If the powerup expires while grounded, reset jumpsAvailable to 1. 
//         if (IsGrounded())
//         {
//             jumpsAvailable = 1;
//         }
//         // If they are airborne and the powerup expires, jumpsAvailable remains at 0 until they land.
        
//         Debug.Log("Powerup: Double Jump Disabled.");
//     }
    
//     public IEnumerator ApplySpeedBoost(float duration)
//     {
//         Debug.Log("Powerup: Speed Boost Enabled.");
//         float originalSpeed = MoveSpeed;
//         MoveSpeed *= 1.5f; 
//         yield return new WaitForSeconds(duration);
//         MoveSpeed = originalSpeed;
//         Debug.Log("Powerup: Speed Boost Disabled.");
//     }
    
//     public IEnumerator ApplyInvincibility(float duration)
//     {
//         Debug.Log("Powerup: Invincibility Enabled.");
//         isInvincible = true;
//         yield return new WaitForSeconds(duration);
//         isInvincible = false;
//         Debug.Log("Powerup: Invincibility Disabled.");
//     }
// }

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic; // Required for LinkedList<T>
// using System.Linq; // Required for Linq extensions like Any()

// public class PlayerShip : MonoBehaviour, IMoveable, IDamageable
// {
//     private Rigidbody2D rb;
//     private Collider2D catchCollider;
    
//     [Header("Ship Stats")]
//     // NOTE: Maintaining naming convention while ensuring public access for HealthUI
//     [SerializeField] private int maxHp = 10;
//     public int MaxHp => maxHp; 
//     private int currentHp;
//     public int CurrentHp => currentHp; 
    
//     [SerializeField] private float MoveSpeed = 15.0f;
//     private float baseMoveSpeed; // Used to track original speed for boost expiry
//     [SerializeField] private float JumpForce = 10.0f;
    
//     // --- BUFF MANAGEMENT: The Linked List Data Structure ---
//     // Stores all currently active, timed buffs (Invincibility, Speed, Double Jump)
//     private LinkedList<BuffTimerLinkedlist> activeBuffs = new LinkedList<BuffTimerLinkedlist>();
    
//     // --- JUMP REFINEMENT FIELDS ---
//     [Header("Jump Refinement")]
//     [SerializeField] private Transform groundCheckPoint; 
//     [SerializeField] private float groundCheckRadius = 0.1f;
//     [SerializeField] private LayerMask groundLayer; 
    
//     // --- JUMP LOGIC VARIABLES ---
//     // These properties now check the Linked List for active buffs (O(1) average time complexity)
//     private bool CanDoubleJump => activeBuffs.Any(b => b.type == PowerupType.Seagull || b.type == PowerupType.TreasureChest);
//     private bool isInvincible => activeBuffs.Any(b => b.type == PowerupType.TreasureChest);
    
//     private bool isCurrentlyDoubleJumping = false; 
//     private int jumpsAvailable = 1; 
    
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         currentHp = maxHp; // Use the internal field
//         baseMoveSpeed = MoveSpeed; // Store the initial move speed
//     }

//     void Update()
//     {
//         // ----------------------------------------------------------------------
//         // 1. LINKED LIST MANAGEMENT: Apply effects and check for buff expiry
//         // ----------------------------------------------------------------------
//         ManageActiveBuffs(); // CORE Linked List Logic
        
//         // ----------------------------------------------------------------------
//         // 2. JUMP RESET LOGIC (Unchanged, now reads the CanDoubleJump property)
//         // ----------------------------------------------------------------------
//         if (IsGrounded())
//         {
//             // The CanDoubleJump check now reads from the Linked List
//             jumpsAvailable = CanDoubleJump ? 2 : 1; 
//             isCurrentlyDoubleJumping = false;
//         }
        
//         // --- CLAMPING FOR SAFETY (Unchanged) ---
//         if (!CanDoubleJump && jumpsAvailable > 1)
//         {
//             jumpsAvailable = 1;
//         }
//     }
    
//     /// <summary>
//     /// CORE LINKED LIST METHOD: Checks for expired buffs and applies persistent effects.
//     /// This method runs every frame (Update).
//     /// </summary>
//     private void ManageActiveBuffs()
//     {
//         bool speedIsActive = false;
        
//         // Iterate through the Linked List using its nodes
//         var node = activeBuffs.First;
//         while (node != null)
//         {
//             BuffTimerLinkedlist buff = node.Value;
//             var nextNode = node.Next; // Cache next node before possible removal (O(1) removal)

//             // Check 1: HAS THE BUFF EXPIRED?
//             if (Time.time >= buff.expirationTime)
//             {
//                 Debug.Log($"Buff expired: {buff.type}");
                
//                 // --- ON BUFF EXPIRY: Only logging necessary as properties handle state reset ---
//                 if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
//                 {
//                     Debug.Log("Speed boost removed.");
//                 }
                
//                 // --- LINKED LIST REMOVAL (O(1) efficiency) ---
//                 activeBuffs.Remove(node);
//             }
//             else
//             {
//                 // Check 2: APPLY PERSISTENT BUFF EFFECTS
//                 if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
//                 {
//                     speedIsActive = true;
//                 }
//             }
//             node = nextNode; // Move to the next node
//         }

//         // Apply Speed Effect: Handled here to ensure base speed is restored if no buffs grant speed
//         if (speedIsActive)
//         {
//             MoveSpeed = baseMoveSpeed * 1.5f;
//         }
//         else
//         {
//             // Restore base speed only if no speed buff is currently active
//             MoveSpeed = baseMoveSpeed;
//         }
//     }
    
//     /// <summary>
//     /// Handles adding/extending a buff to the Linked List.
//     /// </summary>
//     private void RegisterActiveBuff(PowerupType type, float duration)
//     {
//         // Use the new class name for the FirstOrDefault check
//         var existingBuff = activeBuffs.FirstOrDefault(b => b.type == type);
        
//         if (existingBuff != null)
//         {
//             // If the buff already exists, simply extend its expiration time
//             existingBuff.expirationTime = Time.time + duration;
//             Debug.Log($"Buff time extended for: {type}");
//         }
//         else
//         {
//             // Instantiates the BuffTimerLinkedlist class
//             BuffTimerLinkedlist newBuff = new BuffTimerLinkedlist(type, duration);
            
//             // Create and add the new buff node to the Linked List
//             activeBuffs.AddLast(newBuff);
//             Debug.Log($"New buff added: {type}. Expires at {newBuff.expirationTime:F2}");
//         }
//     }

//     // --- BUFF APPLICATION METHODS (REPLACED COROUTINES) ---

//     public void ApplyInvincibility(float duration)
//     {
//         RegisterActiveBuff(PowerupType.TreasureChest, duration);
//     }
    
//     public void EnableDoubleJump(float duration)
//     {
//         RegisterActiveBuff(PowerupType.Seagull, duration);
//     }
    
//     public void ApplySpeedBoost(float duration)
//     {
//         RegisterActiveBuff(PowerupType.Wind, duration);
//     }

//     // --- EXISTING METHODS (Move, Jump, Damage, Heal, etc. - Functionally unchanged) ---

//     public bool IsGrounded()
//     {
//         if (groundCheckPoint == null) return false; 
//         return Physics2D.OverlapCircle(
//             groundCheckPoint.position,
//             groundCheckRadius,
//             groundLayer
//         );
//     }
    
//     public void Move(float x)
//     {
//         if (rb == null) return;
//         float targetVelocityX = x * MoveSpeed;
//         rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);
//     }
    
//     public void Jump()
//     {
//         if (rb == null) return;
        
//         if (!CanDoubleJump && jumpsAvailable > 1)
//         {
//              jumpsAvailable = 1;
//         }

//         if (jumpsAvailable > 0)
//         {
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
//             Vector2 jumpVector = Vector2.up * JumpForce;
//             rb.AddForce(jumpVector, ForceMode2D.Impulse);
//             jumpsAvailable--;
            
//             if (CanDoubleJump && jumpsAvailable == 0 && !IsGrounded())
//             {
//                 isCurrentlyDoubleJumping = true;
//             }
//         }
//     }
    
//     public void TakeDamage (int amount)
//     {
//         // Reads the Linked List
//         if (isInvincible) 
//         {
//             Debug.Log("Hit detected, but player is invincible! Damage ignored.");
//             return; 
//         }
        
//         currentHp -= amount;

//         if (currentHp <= 0)
//         {
//             currentHp = 0; 
//             Die();
//         }
//     }
    
//     private void Die()
//     {
//         Debug.Log("Die() method called. Player ship destroyed!");
//         Destroy(gameObject);
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
//         {
//             collectible.OnCollected(this);
//         }
//     }
    
//     public void Heal(int amount)
//     {
//         currentHp = Mathf.Min(currentHp + amount, maxHp); 
//         Debug.Log($"Healed {amount} HP. Current HP: {CurrentHp}");
//     }
// }

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic; // Required for LinkedList<T>
// using System.Linq; // Required for Linq extensions like Any()

// public class PlayerShip : MonoBehaviour, IMoveable, IDamageable
// {
//     private Rigidbody2D rb;
//     private Collider2D catchCollider;
    
//     [Header("Ship Stats")]
//     [SerializeField] private int maxHp = 10;
//     public int MaxHp => maxHp; 
//     private int currentHp;
//     public int CurrentHp => currentHp; 
    
//     [SerializeField] private float MoveSpeed = 15.0f;
//     private float baseMoveSpeed; // Used to track original speed for boost expiry
//     [SerializeField] private float JumpForce = 10.0f;
    
//     // --- BUFF MANAGEMENT: The Linked List Data Structure ---
//     private LinkedList<BuffTimerLinkedlist> activeBuffs = new LinkedList<BuffTimerLinkedlist>();
    
//     // --- JUMP REFINEMENT FIELDS ---
//     [Header("Jump Refinement")]
//     [SerializeField] private Transform groundCheckPoint; 
//     [SerializeField] private float groundCheckRadius = 0.1f;
//     [SerializeField] private LayerMask groundLayer; 
    
//     // --- JUMP LOGIC VARIABLES ---
//     // These properties now check the Linked List for active buffs (O(1) average time complexity)
//     private bool CanDoubleJump => activeBuffs.Any(b => b.type == PowerupType.Seagull || b.type == PowerupType.TreasureChest);
//     private bool isInvincible => activeBuffs.Any(b => b.type == PowerupType.TreasureChest);
    
//     private bool isCurrentlyDoubleJumping = false; 
//     private int jumpsAvailable = 1; 
    
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         currentHp = maxHp; // Use the internal field
//         baseMoveSpeed = MoveSpeed; // Store the initial move speed
//     }

//     void Update()
//     {
//         // ----------------------------------------------------------------------
//         // 1. LINKED LIST MANAGEMENT: Apply effects and check for buff expiry
//         // ----------------------------------------------------------------------
//         ManageActiveBuffs(); // CORE Linked List Logic
        
//         // ----------------------------------------------------------------------
//         // 2. JUMP RESET LOGIC 
//         // ----------------------------------------------------------------------
//         if (IsGrounded())
//         {
//             // The CanDoubleJump check now reads from the Linked List
//             jumpsAvailable = CanDoubleJump ? 2 : 1; 
//             isCurrentlyDoubleJumping = false;
//         }
        
//         // --- CLAMPING FOR SAFETY ---
//         if (!CanDoubleJump && jumpsAvailable > 1)
//         {
//             jumpsAvailable = 1;
//         }
//     }
    
//     /// <summary>
//     /// CORE LINKED LIST METHOD: Checks for expired buffs and applies persistent effects.
//     /// This method runs every frame (Update).
//     /// </summary>
//     private void ManageActiveBuffs()
//     {
//         bool speedIsActive = false;
        
//         // Iterate through the Linked List using its nodes
//         var node = activeBuffs.First;
//         while (node != null)
//         {
//             BuffTimerLinkedlist buff = node.Value;
//             var nextNode = node.Next; // Cache next node before possible removal (O(1) removal)

//             // Check 1: HAS THE BUFF EXPIRED?
//             if (Time.time >= buff.expirationTime)
//             {
//                 Debug.Log($"Buff expired: {buff.type}");
                
//                 // --- ON BUFF EXPIRY: Only logging necessary as properties handle state reset ---
//                 if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
//                 {
//                     Debug.Log("Speed boost removed.");
//                 }
                
//                 // --- LINKED LIST REMOVAL (O(1) efficiency) ---
//                 activeBuffs.Remove(node);
//             }
//             else
//             {
//                 // Check 2: APPLY PERSISTENT BUFF EFFECTS
//                 if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
//                 {
//                     speedIsActive = true;
//                 }
//             }
//             node = nextNode; // Move to the next node
//         }

//         // Apply Speed Effect: Handled here to ensure base speed is restored if no buffs grant speed
//         if (speedIsActive)
//         {
//             MoveSpeed = baseMoveSpeed * 1.5f;
//         }
//         else
//         {
//             // Restore base speed only if no speed buff is currently active
//             MoveSpeed = baseMoveSpeed;
//         }
//     }
    
//     /// <summary>
//     /// Handles adding/extending a buff to the Linked List.
//     /// </summary>
//     private void RegisterActiveBuff(PowerupType type, float duration)
//     {
//         var existingBuff = activeBuffs.FirstOrDefault(b => b.type == type);
        
//         if (existingBuff != null)
//         {
//             // If the buff already exists, simply extend its expiration time
//             existingBuff.expirationTime = Time.time + duration;
//             Debug.Log($"Buff time extended for: {type}");
//         }
//         else
//         {
//             // Instantiates the BuffTimerLinkedlist class
//             BuffTimerLinkedlist newBuff = new BuffTimerLinkedlist(type, duration);
            
//             // Create and add the new buff node to the Linked List
//             activeBuffs.AddLast(newBuff);
//             Debug.Log($"New buff added: {type}. Expires at {newBuff.expirationTime:F2}");
//         }
//     }

//     // --- PUBLIC BUFF APPLICATION METHODS (Called by the PowerupRegistry Hash Table) ---

//     public void ApplyInvincibility(float duration)
//     {
//         RegisterActiveBuff(PowerupType.TreasureChest, duration);
//     }
    
//     public void EnableDoubleJump(float duration)
//     {
//         RegisterActiveBuff(PowerupType.Seagull, duration);
//     }
    
//     public void ApplySpeedBoost(float duration)
//     {
//         RegisterActiveBuff(PowerupType.Wind, duration);
//     }

//     // --- EXISTING METHODS ---

//     public void Heal(int amount)
//     {
//         currentHp = Mathf.Min(currentHp + amount, maxHp); 
//         Debug.Log($"Healed {amount} HP. Current HP: {CurrentHp}");
//     }
    
//     public bool IsGrounded()
//     {
//         if (groundCheckPoint == null) return false; 
//         return Physics2D.OverlapCircle(
//             groundCheckPoint.position,
//             groundCheckRadius,
//             groundLayer
//         );
//     }
    
//     public void Move(float x)
//     {
//         if (rb == null) return;
//         float targetVelocityX = x * MoveSpeed;
//         rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);
//     }
    
//     public void Jump()
//     {
//         if (rb == null) return;
        
//         if (!CanDoubleJump && jumpsAvailable > 1)
//         {
//              jumpsAvailable = 1;
//         }

//         if (jumpsAvailable > 0)
//         {
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
//             Vector2 jumpVector = Vector2.up * JumpForce;
//             rb.AddForce(jumpVector, ForceMode2D.Impulse);
//             jumpsAvailable--;
            
//             if (CanDoubleJump && jumpsAvailable == 0 && !IsGrounded())
//             {
//                 isCurrentlyDoubleJumping = true;
//             }
//         }
//     }
    
//     public void TakeDamage (int amount)
//     {
//         // Reads the Linked List
//         if (isInvincible) 
//         {
//             Debug.Log("Hit detected, but player is invincible! Damage ignored.");
//             return; 
//         }
        
//         currentHp -= amount;

//         if (currentHp <= 0)
//         {
//             currentHp = 0; 
//             Die();
//         }
//     }
    
//     private void Die()
//     {
//         Debug.Log("Die() method called. Player ship destroyed!");
//         Destroy(gameObject);
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
//         {
//             collectible.OnCollected(this);
//         }
//     }
// }

using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Required for LinkedList<T>
using System.Linq; // Required for Linq extensions like Any()

public class PlayerShip : MonoBehaviour, IMoveable, IDamageable
{
    public System.Collections.Generic.IReadOnlyCollection<BuffTimerLinkedlist> ActiveBuffs => activeBuffs;
    private Rigidbody2D rb;
    private Collider2D catchCollider;
    
    [Header("Ship Stats")]
    [SerializeField] private int maxHp = 10;
    public int MaxHp => maxHp; 
    private int currentHp;
    public int CurrentHp => currentHp; 
    
    [SerializeField] private float MoveSpeed = 15.0f;
    private float baseMoveSpeed; // Used to track original speed for boost expiry
    [SerializeField] private float JumpForce = 10.0f;
    
    // --- BUFF MANAGEMENT: The Linked List Data Structure ---
    private LinkedList<BuffTimerLinkedlist> activeBuffs = new LinkedList<BuffTimerLinkedlist>();
    
    // --- JUMP REFINEMENT FIELDS ---
    [Header("Jump Refinement")]
    [SerializeField] private Transform groundCheckPoint; 
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer; 
    
    // --- JUMP LOGIC VARIABLES ---
    // These properties now check the Linked List for active buffs (O(1) average time complexity)
    private bool CanDoubleJump => activeBuffs.Any(b => b.type == PowerupType.Seagull || b.type == PowerupType.TreasureChest);
    private bool isInvincible => activeBuffs.Any(b => b.type == PowerupType.TreasureChest);
    
    private bool isCurrentlyDoubleJumping = false; 
    private int jumpsAvailable = 1; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp; // Use the internal field
        baseMoveSpeed = MoveSpeed; // Store the initial move speed
    }

    void Update()
    {
        // ----------------------------------------------------------------------
        // 1. LINKED LIST MANAGEMENT: Apply effects and check for buff expiry
        // ----------------------------------------------------------------------
        ManageActiveBuffs(); // CORE Linked List Logic
        
        // ----------------------------------------------------------------------
        // 2. JUMP RESET LOGIC 
        // ----------------------------------------------------------------------
        // FIX: Only reset jumps if IsGrounded AND the player is not currently ascending (falling or stationary)
        if (IsGrounded() && rb.linearVelocity.y <= 0.01f) 
        {
            // The CanDoubleJump check now reads from the Linked List
            jumpsAvailable = CanDoubleJump ? 2 : 1; 
            isCurrentlyDoubleJumping = false;
        }
        
        // --- CLAMPING FOR SAFETY ---
        if (!CanDoubleJump && jumpsAvailable > 1)
        {
            jumpsAvailable = 1;
        }
    }
    
    /// <summary>
    /// CORE LINKED LIST METHOD: Checks for expired buffs and applies persistent effects.
    /// This method runs every frame (Update).
    /// </summary>
    private void ManageActiveBuffs()
    {
        bool speedIsActive = false;
        
        // Iterate through the Linked List using its nodes
        var node = activeBuffs.First;
        while (node != null)
        {
            BuffTimerLinkedlist buff = node.Value;
            var nextNode = node.Next; // Cache next node before possible removal (O(1) removal)

            // Check 1: HAS THE BUFF EXPIRED?
            if (Time.time >= buff.expirationTime)
            {
                Debug.Log($"Buff expired: {buff.type}");
                
                // --- ON BUFF EXPIRY: Only logging necessary as properties handle state reset ---
                if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
                {
                    Debug.Log("Speed boost removed.");
                }
                
                // --- LINKED LIST REMOVAL (O(1) efficiency) ---
                activeBuffs.Remove(node);
            }
            else
            {
                // Check 2: APPLY PERSISTENT BUFF EFFECTS
                if (buff.type == PowerupType.Wind || buff.type == PowerupType.TreasureChest)
                {
                    speedIsActive = true;
                }
            }
            node = nextNode; // Move to the next node
        }

        // Apply Speed Effect: Handled here to ensure base speed is restored if no buffs grant speed
        if (speedIsActive)
        {
            MoveSpeed = baseMoveSpeed * 1.5f;
        }
        else
        {
            // Restore base speed only if no speed buff is currently active
            MoveSpeed = baseMoveSpeed;
        }
    }
    
    /// <summary>
    /// Handles adding/extending a buff to the Linked List.
    /// </summary>
    private void RegisterActiveBuff(PowerupType type, float duration)
    {
        var existingBuff = activeBuffs.FirstOrDefault(b => b.type == type);
        
        if (existingBuff != null)
        {
            // If the buff already exists, simply extend its expiration time
            existingBuff.expirationTime = Time.time + duration;
            Debug.Log($"Buff time extended for: {type}");
        }
        else
        {
            // Instantiates the BuffTimerLinkedlist class
            BuffTimerLinkedlist newBuff = new BuffTimerLinkedlist(type, duration);
            
            // Create and add the new buff node to the Linked List
            activeBuffs.AddLast(newBuff);
            Debug.Log($"New buff added: {type}. Expires at {newBuff.expirationTime:F2}");
        }
    }

    // --- PUBLIC BUFF APPLICATION METHODS (Called by the PowerupRegistry Hash Table) ---

    public void ApplyInvincibility(float duration)
    {
        RegisterActiveBuff(PowerupType.TreasureChest, duration);
    }
    
    public void EnableDoubleJump(float duration)
    {
        RegisterActiveBuff(PowerupType.Seagull, duration);
    }
    
    public void ApplySpeedBoost(float duration)
    {
        RegisterActiveBuff(PowerupType.Wind, duration);
    }

    // --- EXISTING METHODS ---

    public void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp); 
        Debug.Log($"Healed {amount} HP. Current HP: {CurrentHp}");
    }
    
    public bool IsGrounded()
    {
        if (groundCheckPoint == null) return false; 
        return Physics2D.OverlapCircle(
            groundCheckPoint.position,
            groundCheckRadius,
            groundLayer
        );
    }
    
    public void Move(float x)
    {
        if (rb == null) return;
        float targetVelocityX = x * MoveSpeed;
        rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);
    }
    
    public void Jump()
    {
        if (rb == null) return;
        
        if (!CanDoubleJump && jumpsAvailable > 1)
        {
             jumpsAvailable = 1;
        }

        if (jumpsAvailable > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
            Vector2 jumpVector = Vector2.up * JumpForce;
            rb.AddForce(jumpVector, ForceMode2D.Impulse);
            jumpsAvailable--;
            
            if (CanDoubleJump && jumpsAvailable == 0 && !IsGrounded())
            {
                isCurrentlyDoubleJumping = true;
            }
        }
    }
    
    public void TakeDamage (int amount)
    {
        // Reads the Linked List
        if (isInvincible) 
        {
            Debug.Log("Hit detected, but player is invincible! Damage ignored.");
            return; 
        }
        
        currentHp -= amount;

        if (currentHp <= 0)
        {
            currentHp = 0; 
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Die() method called. Player ship destroyed!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            collectible.OnCollected(this);
        }
    }
}