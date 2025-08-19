using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;          // The enemy prefab to spawn
    public float spawnInterval = 2f;        // Time between spawns

    [Header("Spawners")]
    public List<Transform> enemySpawner;    // Add 3 spawners in Inspector

    void Start()
    {
        // Start spawning enemies repeatedly
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // keep spawning
        {
            if (enemySpawner.Count > 0)
            {
                // Pick a random spawner
                int randomIndex = Random.Range(0, enemySpawner.Count);
                Transform spawnPoint = enemySpawner[randomIndex];

                // Spawn enemy at random spawner
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
