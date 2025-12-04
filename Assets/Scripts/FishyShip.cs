using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private int currentLevel;
     private int pointThreshold;
    private bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private const int DemoLevel = 1;

    void Start()
    {
        isGameOver = false;

        StartLevel(DemoLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
    public void StartLevel(int level)
    {
        Debug.Log($"GameManager: Starting Level {level}, game will end if ship Hp reaches 0.");
        currentLevel = level;
    }

    public void EndLevel(bool won)
    {
        isGameOver = true;
    }

    public void AddScore(int amount){}

    public void TakeDamage(int dmgAmount){}    
}
