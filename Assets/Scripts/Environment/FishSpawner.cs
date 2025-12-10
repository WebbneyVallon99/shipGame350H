using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Difficulty Scaling")]
    public float baseFishSpeed = 3f;
    public float spawnInterval = 2f;
    public GameObject[] fishPrefabs;

    private bool isSpawning = false;
    private Coroutine spawnRoutine;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (isSpawning) return;

        isSpawning = true;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (!isSpawning) return;

        isSpawning = false;

        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    private System.Collections.IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            SpawnFish();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnFish()
    {
        if (fishPrefabs == null || fishPrefabs.Length == 0)
        {
            Debug.LogWarning("No fish prefabs assigned!");
            return;
        }

        int index = Random.Range(0, fishPrefabs.Length);
        GameObject chosenFish = fishPrefabs[index];

        float minY = Camera.main.transform.position.y - 3f;
        float maxY = Camera.main.transform.position.y + 3f;

        float y = Random.Range(minY, maxY);


        Vector3 spawnPos = new Vector3(
            transform.position.x,
            y,
            0f
        );

        GameObject fish = Instantiate(chosenFish, spawnPos, Quaternion.identity);
        FishQuirks quirks = fish.GetComponent<FishQuirks>();
        if (quirks != null)
        {
            quirks.horizontalSpeed = GameManager.Instance.fishSpeeds[GameManager.Instance.CurrentLevel];

        }

    }
}
