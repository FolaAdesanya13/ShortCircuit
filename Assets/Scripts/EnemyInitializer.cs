using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EnemyInitializer : MonoBehaviour
{
    float detectionRadius = 5f;
    private Collider[] checks;
    public static event Action<int> PlayerPressed;
    private bool invokedYet;
    [SerializeField] private Button activateButton;

    void Start()
    {
        checks = new Collider[2];
        invokedYet = false;
        activateButton.gameObject.SetActive(false);
    }
    void checkPlayerRange()
    {
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, checks);
        foreach(Collider c in checks)
        {
            if (c.CompareTag("Player"))
            {
                activateButton.gameObject.SetActive(true);
            }
        }
    }

    public void InvokeEnemies(InputAction.CallbackContext input)
    {
        if (!input.performed || !activateButton.IsActive()) return;
        PlayerPressed.Invoke(1);
        invokedYet = true;
    }
    void Update()
    {
        if (!invokedYet) checkPlayerRange();
    }
}
