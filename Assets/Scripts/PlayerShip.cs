using Unity.VisualScripting;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D catchCollider;
    private int MaxHp;
    private int CurrentHp;
    private float MoveSpeed;
    private float JumpForce;
    private bool CanDoubleJump;
    private bool isInvincible; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(float x){}
    public void jump(){}
    //public void ApplyPowerUp(PowerupType, float duration){}
    //public void Heal(int amount){}
    public void TakeDamage (int amount){}
}
