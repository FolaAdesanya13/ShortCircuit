using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

//TO-DO: NOW ADD LIKE A SPHERE CHECKER KINDA LIKE isGrounded TO CHECK IF OTHER ENEMIES ARE AROUND
//AND IF SO HOW MANY IF A CERTAIN NUMBER GO TO NEAREST CORE THIS REDUCING EXCESSIVE BUNCHING
//AND MAKES IT HARDER TO KILL MULTIPLE ENEMIES IN A SWOOP,
//ALSO WE CAN CHANGE THE SPEED OF EACH ENEMY HAVE IT BE LIKE RANDOMIZED, MAKING THE BUNCH LOOK BETTER
//ACTUALLY MIGHT JUST DO THAT FIRST BECAUSE IT WILL BE EASIER AND LESS PERFORMANCE TAXING.
public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> cores = new List<Transform>();
    [SerializeField] private GameObject enemy;

    [SerializeField] private float playerDetectionRange = 15f;

    private NavMeshAgent agent;
    private Thread distanceCalculationThread;
    private volatile bool shouldCalculate = true;
    private readonly object lockObj = new object();

    // Thread-safe position storage (updated on main thread)
    private Vector3 cachedEnemyPosition;
    private Vector3 cachedPlayerPosition;
    private Dictionary<Transform, Vector3> cachedCorePositions = new Dictionary<Transform, Vector3>();

    // Thread-safe distance storage
    private float cachedDistanceFromPlayer;
    private Dictionary<Transform, float> cachedCoreDistances = new Dictionary<Transform, float>();
    private float reduceBunching = 12f, minSpeed = 7.5f, maxSpeed = 12f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cachedEnemyPosition = transform.position;
        cachedPlayerPosition = player != null ? player.position : Vector3.zero;

        foreach (var core in cores)
        {
            if (core != null)
            {
                cachedCoreDistances[core] = float.MaxValue;
                cachedCorePositions[core] = core.position;
            }
        }

        distanceCalculationThread = new Thread(CalculateDistancesThreaded)
        {
            IsBackground = true
        };
        distanceCalculationThread.Start();
        InvokeRepeating(nameof(UpdatePath), 0f, 0.2f);
    }

    void CalculateDistancesThreaded()
    {
        while (shouldCalculate)
        {
            Vector3 enemyPos;
            Vector3 playerPos;
            Dictionary<Transform, Vector3> corePositions;

            // Safely read cached positions from main thread
            lock (lockObj)
            {
                enemyPos = cachedEnemyPosition;
                playerPos = cachedPlayerPosition;
                corePositions = new Dictionary<Transform, Vector3>(cachedCorePositions);
            }

            if (player != null)
            {
                float newPlayerDistance = Vector3.Distance(enemyPos, playerPos);
                lock (lockObj)
                {
                    cachedDistanceFromPlayer = newPlayerDistance;
                }
            }

            foreach (var core in cores)
            {
                if (core != null && corePositions.ContainsKey(core))
                {
                    float newCoreDistance = Vector3.Distance(enemyPos, corePositions[core]);
                    lock (lockObj)
                    {
                        cachedCoreDistances[core] = newCoreDistance;
                    }
                }
            }
            Thread.Sleep(100);
        }
    }

    void UpdatePath()
    {
        // Cache all positions from main thread (this is safe)
        lock (lockObj)
        {
            cachedEnemyPosition = transform.position;
            cachedPlayerPosition = player != null ? player.position : Vector3.zero;

            cachedCorePositions.Clear();
            foreach (var core in cores)
            {
                if (core != null)
                    cachedCorePositions[core] = core.position;
            }
        }
        if (!enemy.activeSelf) return;

        float playerDistance;
        Dictionary<Transform, float> coreDistances;

        // Safely read cached distances
        lock (lockObj)
        {
            playerDistance = cachedDistanceFromPlayer;
            coreDistances = new Dictionary<Transform, float>(cachedCoreDistances);
        }

        Transform nearestCore = FindNearestCore(coreDistances);
        float antiBunch = Vector3.Distance(transform.position, nearestCore.position);

        if (player != null && playerDistance <= playerDetectionRange && ((playerDistance - antiBunch) < reduceBunching))
        {
            agent.SetDestination(player.position);
        }
        else
        {
            if (nearestCore != null)
            {
                agent.SetDestination(nearestCore.position);
            }
        }
        agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    Transform FindNearestCore(Dictionary<Transform, float> coreDistances)
    {
        if (coreDistances.Count == 0) return null;
        return coreDistances.OrderBy(kvp => kvp.Value).First().Key;
    }

    void OnDestroy()
    {
        shouldCalculate = false;
        if (distanceCalculationThread != null && distanceCalculationThread.IsAlive)
        {
            distanceCalculationThread.Join(1000);
        }
    }
}