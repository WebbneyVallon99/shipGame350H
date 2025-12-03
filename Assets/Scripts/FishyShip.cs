using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private int currentLevel;
     private int pointThreshold;
    private bool isGameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
    public void StartLevel(int level){}

    public void EndLevel(bool won){}

    public void AddScore(int amount){}

    public void TakeDamage(int dmgAmount){}    
}
