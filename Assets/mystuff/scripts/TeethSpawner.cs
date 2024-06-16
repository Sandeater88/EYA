using System.Collections;
using UnityEngine;

public class TeethSpawner : MonoBehaviour
{
    public GameObject garlicPrefab; // Reference to the garlic prefab
    public float spawnInterval = 10f; // Time interval between spawns
    public int minGarlicCount = 2; // Minimum number of garlics to spawn
    public int maxGarlicCount = 5; // Maximum number of garlics to spawn
    public float yStep = 1f; // Vertical step between garlic spawns
    public float spawnX = 0f; // X position for spawning garlics
    public float spawnZ = 0f; // Z position for spawning garlics
    public Vector2 spawnYRange = new Vector2(2f, 5f); // Y range for spawning garlics

    void Start()
    {
        // Start spawning coroutine
        StartCoroutine(SpawnGarlicRoutine());
    }

    IEnumerator SpawnGarlicRoutine()
    {
        while (true)
        {
            // Calculate a random number of garlics to spawn
            int garlicCount = Random.Range(minGarlicCount, maxGarlicCount + 1);

            // Spawn garlics
            for (int i = 0; i < garlicCount; i++)
            {
                // Calculate position with fixed X and Z, and random Y within range
                Vector3 spawnPosition = new Vector3(spawnX, Random.Range(spawnYRange.x, spawnYRange.y), spawnZ);
                spawnPosition += transform.position; // Offset by spawner's position

                // Instantiate garlic prefab
                Instantiate(garlicPrefab, spawnPosition, Quaternion.identity);
            }

            // Wait for spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
