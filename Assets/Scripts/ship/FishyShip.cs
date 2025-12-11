using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("Level Progression")]
    public int[] scoreThresholds;   // e.g. [20, 50, 100]
    [SerializeField] private int currentLevel = 0;
    public int CurrentLevel => currentLevel;

    [Header("Backgrounds")]
    public Renderer backgroundRenderer; // Your Quad's renderer
    public Material[] levelMaterials;  // Drop backgrounds here

    [Header("Difficulty Scaling")]
    public float[] scrollSpeeds;       // Background scroll speed per level
    public float[] fishSpeeds;         // Fish base speed per level
    public float[] spawnIntervals;     // Spawn rate per level

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartLevel(0);
    }

    public void StartLevel(int level)
    {
        Debug.Log($"GameManager: Starting Level {level}.");

        currentLevel = level;
        ApplyLevelSettings(level);
    }

    private void ApplyLevelSettings(int level)
    {
        Debug.Log($"Applying settings for Level {level}");

        // 1. Change background
        if (level < levelMaterials.Length)
            backgroundRenderer.sharedMaterial = levelMaterials[level];

        // 2. Adjust scroll speed
        var scroller = FindFirstObjectByType<BackgroundScroller>();
        if (level < scrollSpeeds.Length)
            scroller.scrollSpeed = scrollSpeeds[level];

        // 3. Adjust fish base speed for new spawns
        var spawner = FindFirstObjectByType<FishSpawner>();
        if (level < fishSpeeds.Length)
            spawner.baseFishSpeed = fishSpeeds[level];

        // 4. Adjust spawn rate
        if (level < spawnIntervals.Length)
            spawner.spawnInterval = spawnIntervals[level];

        // 5. Update ALL existing fish
        if (level < fishSpeeds.Length)
        {
            FishQuirks[] allFish = Object.FindObjectsByType<FishQuirks>(FindObjectsSortMode.None);
            foreach (var f in allFish)
            {
                f.horizontalSpeed = fishSpeeds[level];
            }
        }
    }

    public void CheckLevelProgression()
    {
        // stop leveling if we have no more levels defined
        if (currentLevel + 1 >= scoreThresholds.Length ||
            currentLevel + 1 >= levelMaterials.Length ||
            currentLevel + 1 >= scrollSpeeds.Length ||
            currentLevel + 1 >= fishSpeeds.Length ||
            currentLevel + 1 >= spawnIntervals.Length)
            return;

        if (ScoreManager.Instance.totalScore >= scoreThresholds[currentLevel + 1])
        {
            Debug.Log("LEVEL UP!");
            StartLevel(currentLevel + 1);
        }
    }


    public void EndLevel(bool won)
    {
        isGameOver = true;
        Debug.Log($"Game Over! Won: {won}");
    }

    public void TakeDamage(int dmgAmount) { }
}
