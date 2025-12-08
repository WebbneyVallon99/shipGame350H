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

    // Internals
    private float baseY;
    private Vector2 launchVelocity;
    private bool launched = false;

    // Randomized parameters
    private float randomFrequency;
    private float randomAmplitude;
    private float verticalDriftSpeed;
    private float dartTimer;

    void Start()
    {
        baseY = transform.position.y;

        // Small subtle wobble to prevent big wave motions
        randomFrequency = waveFrequency * Random.Range(0.5f, 1.5f);
        randomAmplitude = waveAmplitude * Random.Range(0.1f, 0.4f);

        // Slow upward/downward drift
        verticalDriftSpeed = Random.Range(-0.25f, 0.25f);

        // Timer for sudden forward dart
        dartTimer = Random.Range(1.5f, 4f);
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
            // Horizontal movement
            transform.Translate(Vector2.left * horizontalSpeed * Time.deltaTime);


            // 1. Slow vertical drift
            baseY += verticalDriftSpeed * Time.deltaTime;

            // 2. Smooth Perlin-noise organic movement
            float noise = Mathf.PerlinNoise(Time.time * 0.4f, transform.position.x * 0.2f);
            float noiseOffset = (noise - 0.5f) * 0.6f; 

            float newY = baseY + noiseOffset;

            // 3. Occasional tiny wave wiggle
            if (useWaveMovement && Random.value < 0.25f)
            {
                float waveOffset = Mathf.Sin(Time.time * randomFrequency) * randomAmplitude * 0.2f;
                newY += waveOffset;
            }

            // 4. Rare jerk-like dart up/down
            if (Random.value < 0.002f)
            {
                newY += Random.Range(-0.4f, 0.4f);
            }

            transform.position = new Vector3(transform.position.x, newY, 0);

            // 5. Dart burst handling
            HandleDarting();
        }

        if (transform.position.x < Camera.main.transform.position.x - 25f)
            Despawn();
    }

    void HandleDarting()
    {
        dartTimer -= Time.deltaTime;

        if (dartTimer <= 0)
        {
            horizontalSpeed *= Random.Range(1.25f, 1.8f);

            dartTimer = Random.Range(1.5f, 4f);
        }
    }

    public void Launch(Vector2 velocity)
    {
        launchVelocity = velocity;
        launched = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            OnCollected(other.GetComponent<PlayerShip>());

        if (other.CompareTag("DespawnZone"))
            Despawn();
    }

    public void OnCollected()
    {
        OnCollected(null);
    }

    public void OnCollected(PlayerShip player)
    {
        Debug.Log($"[COLLECTED] Player collected {fishType} worth {pointValue} points.");
        ScoreManager.Instance.AddPoints(fishType);
        Destroy(gameObject);
    }

    public void Despawn()
    {
        Debug.Log($"[DESPAWN] {fishType} despawned.");
        Destroy(gameObject);
    }
}
