using Unity.VisualScripting;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IMoveable
{
    private Rigidbody2D rb;
    private Collider2D catchCollider;
    private int MaxHp;
    private int CurrentHp;
    [SerializeField] private float MoveSpeed = 15.0f;
    [SerializeField] private float JumpForce = 10.0f;
    private bool CanDoubleJump;
    private bool isInvincible; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
    //public void ApplyPowerUp(PowerupType, float duration){}
    //public void Heal(int amount){}
    public void TakeDamage (int amount){}
}
