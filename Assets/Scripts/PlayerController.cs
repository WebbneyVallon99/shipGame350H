using UnityEngine;

public class PlayerController : MonoBehaviour
{
        [SerializeField] private PlayerShip movableObject; 
    private float horizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optional: Perform a safety check to ensure the link was made in the Inspector
        if (movableObject == null)
        {
            Debug.LogError("PlayerController needs a reference to the PlayerShip in the Inspector!", this);
        }
    }

    // Update is called once per frame (Best place to read input)
    void Update()
    {
        // Capture Input
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Process Input and Send Command
        HandleInput(); 
    }
    
    // Custom method to read input and send commands
    void HandleInput()
    {
       
        if (movableObject == null) return;
        movableObject.Move(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movableObject.Jump();
        }
    }
}