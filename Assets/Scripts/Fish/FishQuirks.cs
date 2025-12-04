using UnityEngine;

public class FishQuirks : MonoBehaviour, ICollectible
{
    [Header("Fish Stats")]
    public FishType fishType;
    public int pointValue = 10;

    [Header("Movement")]
    public float horizontalSpeed = 3f;
    public bool useWaveMovement = true;
    public float waveAmplitude = 0.5f;
    public float waveFrequency = 2f;

    private float baseY;
    private Vector2 launchVelocity;
    private bool launched = false;

    void Start()
    {
        baseY = transform.position.y;
    }

    void Update()
    {
        if (launched)
        {
            launchVelocity.y += Physics2D.gravity.y * Time.deltaTime;
            transform.Translate(launchVelocity * Time.deltaTime);
        }
        else
        {
            // NORMAL horizontal movement
            transform.Translate(Vector2.left * horizontalSpeed * Time.deltaTime);

            // OPTIONAL WAVE MOTION (Wave Man style)
            if (useWaveMovement)
            {
                float offset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
                transform.position = new Vector3(transform.position.x, baseY + offset, 0);
            }
        }

        if (transform.position.x < Camera.main.transform.position.x - 25f)
            Destroy(gameObject);
    }

    // Called by FishSpawner if we later want Cheep-Cheep jumping
    public void Launch(Vector2 velocity)
    {
        launchVelocity = velocity;
        launched = true;
    }

    public void OnCollected()
    {
        // TODO: add score via GameManager later
        Destroy(gameObject);
    }
}
