using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject objectPrefab;

    public int numberOfObjectsToInstantiate = 4; // Set the number of objects to instantiate

    void Awake()
    {
        // Find all empty GameObjects in the scene with the "SpawnPoint" tag
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // Shuffle the spawnPoints array to randomize the order
        ShuffleArray(spawnPoints);

        // Instantiate objects at the positions of the shuffled spawn points
        for(int i = 0; i < numberOfObjectsToInstantiate; i++)
        {
            Instantiate(objectPrefab, spawnPoints[i].transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }
    }

    // Fisher-Yates shuffle algorithm for shuffling the positions array
    void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
