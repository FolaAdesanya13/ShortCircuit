using UnityEngine;
using UnityEngine.AI;


//ONLY FOR TESTING
public class EnemySpawn : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;

    private const int maxPossibleEnemies = 5;

    private Vector3 minBounds = new Vector3(-30f, -1.8f, 30f);
    private Vector3 maxBounds = new Vector3(30f, -1.8f, 80f);
    void Start()
    {
        enemy.SetActive(false);
        Spawn();
    }

    void Spawn()
    {
        enemy.SetActive(true);
        for (int i = 0; i < maxPossibleEnemies; i++)
        {
            float xPos = Random.Range(minBounds.x, maxBounds.x);
            float yPos = Random.Range(minBounds.y, maxBounds.y);
            float zPos = Random.Range(minBounds.z, maxBounds.z);
            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
