using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


//ONLY FOR TESTING
public class EnemySpawn : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public static List<EnemyHealth> enemies;
    public static List<EnemyHealth> deadEnemies;

    private const int maxPossibleEnemies = 20;

    private Vector3 minBounds = new Vector3(-30f, -1.8f, 30f);
    private Vector3 maxBounds = new Vector3(30f, -1.8f, 80f);

    void Start()
    {
        enemies = new List<EnemyHealth>();
        deadEnemies = new List<EnemyHealth>();

        EnemyHealth og = enemy.GetComponent<EnemyHealth>();
        if (og == null) og = enemy.AddComponent<EnemyHealth>();
        enemies.Add(og);

        CreateAllEnemies();
    }

    void CreateAllEnemies()
    {
        for (int i = 0; i < maxPossibleEnemies; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        float xPos = Random.Range(minBounds.x, maxBounds.x);
        float yPos = Random.Range(minBounds.y, maxBounds.y);
        float zPos = Random.Range(minBounds.z, maxBounds.z);
        Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

        GameObject go = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
        if (go == null)
        {
            Debug.LogError("Failed to instantiate enemy prefab.");
            return;
        }

        EnemyHealth eh = go.GetComponent<EnemyHealth>();
        if (eh == null) eh = go.AddComponent<EnemyHealth>();

        enemies.Add(eh);
    }

    private void FixedUpdate()
    {
        DeadChecker();
        Respawn();
    }

    void DeadChecker()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyHealth eh = enemies[i];
            if (eh.IsDead())
            {
                deadEnemies.Add(eh);
                enemies.RemoveAt(i);
                eh.gameObject.SetActive(false);
            }
        }
    }

    void Respawn()
    {
        if (deadEnemies.Count >= 3)
        {
            for (int i = deadEnemies.Count - 1; i >= 0; i--)
            {
                EnemyHealth eh = deadEnemies[i];
                enemies.Add(eh);
                deadEnemies.RemoveAt(i);
                eh.Respawned();
                eh.gameObject.SetActive(true);
            }
        }
    }
}
