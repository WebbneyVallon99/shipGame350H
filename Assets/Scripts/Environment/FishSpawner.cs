using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;     
    public float spawnInterval = 2f;     
    public float spawnRangeY = 3f;       
    public float spawnX = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFish();
            timer = 0f;
        }
    }

    private void SpawnFish()
    {
        
        int index = Random.Range(0, fishPrefabs.Length);
        GameObject fish = fishPrefabs[index];

        Vector2 pos = new Vector2(spawnX, Random.Range(-spawnRangeY, spawnRangeY));

        Instantiate(fish, pos, Quaternion.identity);
    }
}
