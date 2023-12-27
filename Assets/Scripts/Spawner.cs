using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform[] spawnPoints; // Array of spawn points
    private List<GameObject> enemies = new List<GameObject>(); // List to keep track of spawned enemies
    private int maxEnemies = 4; // Maximum number of enemies
    public float spawnDelay = 300f; // Delay before spawning a new enemy

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

IEnumerator SpawnEnemies()
{
    // Spawn the first four enemies immediately
    for (int i = 0; i < maxEnemies; i++)
    {
        SpawnEnemy();
    }

    // Continuously spawn enemies
    while (true)
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds between checks

        // Remove null entries from the list (dead enemies)
        enemies.RemoveAll(item => item == null);

        // Only spawn a new enemy if the current number of enemies is less than the maximum
        if (enemies.Count < maxEnemies)
        {
            yield return new WaitForSeconds(spawnDelay); // Wait for the spawn delay before spawning a new enemy
            SpawnEnemy();
        }
    }
}

void SpawnEnemy()
{
    // Choose a random location within a 10 unit radius around the spawner
    Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10;
    spawnPosition.y = transform.position.y; // Keep the y-coordinate the same as the spawner's

    // Spawn the enemy
    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    enemies.Add(enemy); // Add the new enemy to the list
}
}