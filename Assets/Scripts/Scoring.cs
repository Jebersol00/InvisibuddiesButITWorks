using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Scoring : MonoBehaviour
{

    public TMP_Text scoreText;
    public GameObject winText;
    public GameObject button;
    public int score = -1;
    //public SpawnEnemy remaining;
    public List<GameObject> remaining = new List<GameObject>();
    public GameObject[] enemies;
    public int numberEnemies;
    bool ran = false;


    void Start(){
        winText.SetActive(false);
        button.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject i in remaining){
            if (!i.activeInHierarchy){
                remaining.Remove(i);
            }
        }

    }

    void Update() {
        if (!ran){
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            for (int i = 0; i < numberEnemies; i++){
                remaining.Add(enemies[i]);
            }
            ran = true;
        }
        
        if (score == 0){
            winText.SetActive(true);
            button.SetActive(true);
        }
        score = remaining.Count;
        scoreText.text = score + " out of " + numberEnemies + " enemies remaining";
    }
}
