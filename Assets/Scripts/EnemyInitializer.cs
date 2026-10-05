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
        checks = new Collider[10];
        invokedYet = false;
        activateButton.gameObject.SetActive(false);
    }
    void CheckPlayerRange()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, checks);
        for(int i = 0; i < hits; i++)
        {
            if (checks[i].CompareTag("Player"))
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
        activateButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!invokedYet) CheckPlayerRange();
    }
}
