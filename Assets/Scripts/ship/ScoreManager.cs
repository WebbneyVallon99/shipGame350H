using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int totalScore = 0;

    private void Awake()
    {
        // Basic singleton pattern so Fish can notify ScoreManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(FishType type)
    {
        int points = GetPointValue(type);
        totalScore += points;

        Debug.Log($"[SCORE] Added {points} points from a {type}. Total = {totalScore}");
        if (GameManager.Instance != null){

            GameManager.Instance.CheckLevelProgression();

        }
    }

    private int GetPointValue(FishType type)
    {
        switch (type)
        {
            case FishType.Small:
                return 3;

            case FishType.Medium:
                return 6;

            case FishType.Large:
                return 9;

            default:
                return 1;
        }
    }

    public void ResetScore()
    {
        totalScore = 0;
        Debug.Log("[SCORE] Score reset.");
    }
}
