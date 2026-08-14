using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    private float health = 50f;
    private const float initialHealth = 50f;
    private bool isDead = false;
    public void TakeDamage(float amt)
    {
        health -= amt;
        if(health <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Respawned()
    {
        health = initialHealth;
        isDead = false;
    }

}
