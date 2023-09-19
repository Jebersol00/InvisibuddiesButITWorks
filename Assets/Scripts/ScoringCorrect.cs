using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringCorrect : MonoBehaviour
{
public TextMeshProUGUI scoreText;

    public int totalEnemies;
    private int enemiesRemaining;

    void Start()
    {
        InitializeScore();
    }

    void Update()
    {
        // You can call this function whenever you want to update the score,
        // such as in response to a game event or a specific condition.
        UpdateScore();
    }

    void InitializeScore()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemies = taggedObjects.Length;
        enemiesRemaining = totalEnemies;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = enemiesRemaining + " out of " + totalEnemies + " enemies remain";
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        UpdateScore();
    }
}
