using UnityEngine;
using UnityEngine.AI;


//ONLY FOR TESTING
public class EnemySpawn : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    private NavMeshAgent agent;

    private float enemies = 0;
    private const int maxPossibleEnemies = 40;

    private Vector3 minBounds = new Vector3(-30f, 1.53f, -29f);
    private Vector3 maxBounds = new Vector3(30f, 1.53f, -88f);
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void FixedUpdate()
    {
        if (enemies < maxPossibleEnemies) Spawn();
    }

    void Spawn()
    {
        for(int i = 0; i < maxPossibleEnemies; i++)
        {
            float xPos = Random.Range(minBounds.x, maxBounds.x);
            float yPos = Random.Range(minBounds.y, maxBounds.y);
            float zPos = Random.Range(minBounds.z, maxBounds.z);
            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

            Instantiate(enemy, spawnPos, Quaternion.identity);
            enemies++;
        }
    }
}
