// using UnityEngine;

// public class ScoreManager : MonoBehaviour
// {
//     public static ScoreManager Instance;

//     public int totalScore = 0;

//     private void Awake()
//     {
//         // Basic singleton pattern so Fish can notify ScoreManager
//         if (Instance == null)
//             Instance = this;
//         else
//             Destroy(gameObject);
//     }

//     public void AddPoints(FishType type)
//     {
//         int points = GetPointValue(type);
//         totalScore += points;

//         Debug.Log($"[SCORE] Added {points} points from a {type}. Total = {totalScore}");
//         // FIX: Change reference from GameManager.Instance to LevelManager.Instance
//         if (LevelManager.Instance != null){ 

//             LevelManager.Instance.CheckLevelProgression(); // FIX: Call LevelManager

//         }
//     }

//     private int GetPointValue(FishType type)
//     {
//         switch (type)
//         {
//             case FishType.Small:
//                 return 3;

//             case FishType.Medium:
//                 return 6;

//             case FishType.Large:
//                 return 9;

//             default:
//                 return 1;
//         }
//     }

//     public void ResetScore()
//     {
//         totalScore = 0;
//         Debug.Log("[SCORE] Score reset.");
//     }
// }


using UnityEngine;
using System; // Required for Action<T>

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int totalScore = 0;
    
    // --- NEW: Event to notify listeners (like GameUI) of score changes ---
    public event Action<int> OnScoreChanged;

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
        
        // --- NEW: Invoke the event with the new total score ---
        OnScoreChanged?.Invoke(totalScore);

        // FIX: Change reference from GameManager.Instance to LevelManager.Instance
        if (LevelManager.Instance != null){ 

            LevelManager.Instance.CheckLevelProgression(); // FIX: Call LevelManager

        }
    }

    public int GetPointValue(FishType type)
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