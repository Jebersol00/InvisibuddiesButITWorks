using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringCorrect : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int totalEnemies;
    public int enemiesRemaining;

    public GameObject button;
    public GameObject victory;

    void Start()
    {
        enemiesRemaining = totalEnemies;
    }

    void FixedUpdate()
    {
        scoreText.text = enemiesRemaining + " out of " + totalEnemies + " enemies remain";
        if(enemiesRemaining == 0)
        {
            button.SetActive(true);
            victory.SetActive(true);
        }
    }
}
