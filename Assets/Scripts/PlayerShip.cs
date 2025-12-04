// using Unity.VisualScripting;
// using UnityEngine;

// public class PlayerShip : MonoBehaviour, IMoveable
// {
//     private Rigidbody2D rb;
//     private Collider2D catchCollider;
//     [SerializeField] private int MaxHp = 10; // Sets a default value and shows in Inspector
//     private int CurrentHp;
//     [SerializeField] private float MoveSpeed = 15.0f;
//     [SerializeField] private float JumpForce = 10.0f;
//     private bool CanDoubleJump;
//     private bool isInvincible; 
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
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
//         Vector2 jumpVector = Vector2.up * JumpForce;

//         rb.AddForce(jumpVector, ForceMode2D.Impulse);
//     }
//     //public void ApplyPowerUp(PowerupType, float duration){}
//     //public void Heal(int amount){}
//     public void TakeDamage (int amount)
//     {
//         // 1. Apply the damage
//     CurrentHp =  MaxHp - amount;

//     // 2. Log the hit message for immediate testing feedback
//     Debug.Log($"OUCH! PlayerShip hit for {amount} damage. Remaining HP: {CurrentHp}");

//     // 3. Check for Game Over condition (log the event, but DO NOT call Die() yet)
//     if (CurrentHp <= 0)
//     {
//         CurrentHp = 0; 
        
//         // Log the destruction event without executing the complex Die() logic
//         Debug.Log("SHIP DESTROYED! Game Over condition met (Die() deferred).");
//     }
//     }
// }


using UnityEngine;

public class PlayerShip : MonoBehaviour, IMoveable, IDamageable
{
    private Rigidbody2D rb;
    private Collider2D catchCollider;
    [SerializeField] private int MaxHp = 10;
    private int CurrentHp;
    [SerializeField] private float MoveSpeed = 15.0f;
    [SerializeField] private float JumpForce = 10.0f;
    private bool CanDoubleJump;
    private bool isInvincible; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentHp = MaxHp; 
    }

    void Update()
    {
       
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
        Vector2 jumpVector = Vector2.up * JumpForce;

        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }
    
    // public void ApplyPowerUp(PowerupType, float duration){}
    // public void Heal(int amount){}
    
    public void TakeDamage (int amount)
    {
        CurrentHp -= amount;

        
        Debug.Log($"OUCH! PlayerShip hit for {amount} damage. Remaining HP: {CurrentHp}");

      
        if (CurrentHp <= 0)
        {
            CurrentHp = 0; 
            
            
            Debug.Log("SHIP DESTROYED! Game Over condition met (Die() deferred).");
            Die();
        }
    }
    
    private void Die()
    {
        
        Debug.Log("Die() method called.");
    }
}