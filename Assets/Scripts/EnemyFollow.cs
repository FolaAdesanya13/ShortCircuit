using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    private NavMeshAgent agent;
    private float playerDetectionRange = 30f, coreDetectionRange = 30f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
    }

    //if enemy is within a certain distance of the player then obviously follow player
    //if not then go to nearest cpu core, meaning we are constantly calculating distance
    //so try making that calculation on another thread so as to not affect main game
    void UpdatePath()
    {
        if (!enemy.activeSelf) return;

        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (player != null && distanceFromPlayer<=playerDetectionRange)
        {
            agent.SetDestination(player.position);
        }
    }
}