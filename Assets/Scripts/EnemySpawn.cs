using System.Collections.Generic;
using UnityEngine;

//TO-DO: ALSO SEE WHY ENEMIES KEEP PHASING THROUGH THE CORES EVEN THOUGH THEY HAVE COLLIDERS
//ALSO FOR THE FIRST TO DO JUST MAKE ALL ENEMIES INACTIVE AND SET VISIBLE TO FALSE
//SLOWLY MAKE THEM ACTIVE AFTER PLAYER EVENT
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemy;
    private CapsuleCollider cc;

    private static List<EnemyHealth> enemies;
    private static List<EnemyHealth> deadEnemies;
    private Vector3 minBounds = new Vector3(-30f, -1.8f, 30f);
    private Vector3 maxBounds = new Vector3(30f, -1.8f, 80f);
    private const int maxPossibleEnemies = 20;

    void Start()
    {
        cc = GetComponent<CapsuleCollider>();
        enemies = new List<EnemyHealth>();
        deadEnemies = new List<EnemyHealth>();

        EnemyHealth og = enemy.GetComponent<EnemyHealth>();
        if (og == null) og = enemy.AddComponent<EnemyHealth>();
        enemies.Add(og);
        CreateAllEnemies();
        WaitToActivateEnemies();
    }

    void OnEnable()
    {
        EnemyInitializer.PlayerPressed += ActivateEnemies;
    }
    void OnDisable()
    {
        EnemyInitializer.PlayerPressed -= ActivateEnemies;
    }

    void WaitToActivateEnemies()
    {
        foreach (EnemyHealth eh in enemies)
        {
            if (eh != null && eh.gameObject != null)
                eh.gameObject.SetActive(false);
        }
    }
    void ActivateEnemies(int throwaway)
    {
        foreach (EnemyHealth eh in enemies)
        {
            if (eh != null && eh.gameObject != null)
                eh.gameObject.SetActive(true);
        }
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

        GameObject go = Instantiate(enemy, spawnPos, Quaternion.identity);
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
        float xPos = Random.Range(minBounds.x, maxBounds.x);
        float yPos = Random.Range(minBounds.y, maxBounds.y);
        float zPos = Random.Range(minBounds.z, maxBounds.z);
        Vector3 respawnPos = new Vector3(xPos, yPos, zPos);
        if (deadEnemies.Count >= 3)
        {
            for (int i = deadEnemies.Count - 1; i >= 0; i--)
            {
                EnemyHealth eh = deadEnemies[i];
                enemies.Add(eh);
                deadEnemies.RemoveAt(i);
                eh.Respawned();
                eh.gameObject.transform.position = respawnPos;
                eh.gameObject.SetActive(true);
            }
        }
    }
}
