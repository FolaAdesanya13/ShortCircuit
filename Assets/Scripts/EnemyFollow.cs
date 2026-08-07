using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
    }

    void UpdatePath()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}