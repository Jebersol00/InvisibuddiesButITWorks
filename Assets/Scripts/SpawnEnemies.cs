using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject objectPrefab;

    private GameObject[] enemies;

    private GameObject[] waypoints;

    private bool[] empty;

    public int numberOfObjectsToInstantiate = 4; // Set the number of objects to instantiate

    void Awake()
    {
        // Find all empty GameObjects in the scene with the "SpawnPoint" tag
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // Shuffle the spawnPoints array to randomize the order
        ShuffleArray(spawnPoints);

        waypoints = new GameObject[spawnPoints.Length];
        empty = new bool[spawnPoints.Length];
        enemies = new GameObject[numberOfObjectsToInstantiate];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            waypoints[i] = spawnPoints[i];
            empty[i] = true;
        }

        // Instantiate objects at the positions of the shuffled spawn points
        for(int i = 0; i < numberOfObjectsToInstantiate; i++)
        {
            enemies[i] = Instantiate(objectPrefab, spawnPoints[i].transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            enemies[i].transform.parent = this.gameObject.transform;
            empty[i] = false;
        }

        StartCoroutine(MoveEnemyRoutine());
    }
    IEnumerator MoveEnemyRoutine()
    {
        while (true)
        {
            // Wait for 5 seconds
            yield return new WaitForSeconds(4f);
            int move = Random.Range(0, 4);

            // Move one of the enemies to an empty waypoint
            if (move == 0)
                MoveEnemyToEmptyWaypoint();
        }
    }

    void MoveEnemyToEmptyWaypoint()
    {
        // Find an empty waypoint
        int emptyWaypointIndex = FindEmptyWaypoint();

        // If there is an empty waypoint and there is at least one enemy
        if (emptyWaypointIndex != -1 && enemies.Length > 0)
        {
            // Find an enemy to move
            int randomEnemyIndex = Random.Range(0, enemies.Length);

            // Move the selected enemy to the empty waypoint
            NavMeshAgent navMeshAgent = enemies[randomEnemyIndex].GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(waypoints[emptyWaypointIndex].transform.position);
                empty[emptyWaypointIndex] = false;
            }
        }
    }

    int FindEmptyWaypoint()
    {
        // Find an empty waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (empty[i])
            {
                return i;
            }
        }
        return -1; // No empty waypoint found
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
