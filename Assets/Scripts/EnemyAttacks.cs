using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    private int attackDmg = 5;
    private float detectionRange = 3f;
    private PlayerHealth player;
    private float attackCooldown = 0f;
    private float attackDelay = 1f; // Delay between attacks in seconds

    void Start()
    {
        // Find the actual player in the scene, don't create a new instance
        player = FindFirstObjectByType<PlayerHealth>();
        if (player == null)
        {
            Debug.LogError("PlayerHealth not found in scene!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate direction to player
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if player is within range
        if (distanceToPlayer <= detectionRange)
        {
            // Raycast from enemy to player to check line of sight
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (attackCooldown <= 0f)
                    {
                        Attack();
                        attackCooldown = attackDelay;
                    }
                }
            }
        }

        attackCooldown -= Time.deltaTime;
    }

    void Attack()
    {
        if (player != null)
        {
            player.TakeDmg(attackDmg);
        }
    }
}
