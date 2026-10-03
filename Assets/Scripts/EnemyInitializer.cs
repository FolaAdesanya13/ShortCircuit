using System;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    float detectionRadius = 5f;
    private Collider[] checks;
    public static event Action<int> PlayerPressed;
    private bool invokedYet;

    void Start()
    {
        checks = new Collider[2];
        invokedYet = false;
    }
    void checkPlayerRange()
    {
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, checks);
        foreach(Collider c in checks)
        {
            if (c.CompareTag("Player"))
            {
                InvokeEnemies();
            }
        }
    }

    void InvokeEnemies()
    {
        //UI to show "click button to extract enemies"
        PlayerPressed?.Invoke(5);
        invokedYet = true;
    }
    void Update()
    {
        if (!invokedYet) checkPlayerRange();
    }
}
