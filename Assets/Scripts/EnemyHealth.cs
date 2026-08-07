using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    private float health = 50f;

    public void TakeDamage(float amt)
    {
        health -= amt;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(enemy);
    }

}
