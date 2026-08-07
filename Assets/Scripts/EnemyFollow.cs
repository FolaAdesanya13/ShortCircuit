using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Dynamically locate the Player by Tag so the prefab knows who to chase
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Generate a random delay between 0.1 and 1.0 seconds for each individual enemy
        float randomStartDelay = Random.Range(0.1f, 1f);

        // Start the repeating path update using that random delay
        // This spreads the CPU load out so they never all calculate on the same frame
        InvokeRepeating(nameof(UpdatePath), randomStartDelay, 0.2f);
    }

    void UpdatePath()
    {
        // Only calculate a path if the player exists AND the agent is safely touching the NavMesh
        if (player != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }
}