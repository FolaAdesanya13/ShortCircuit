using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    private float health = 50f;
    private const float initialHealth = 50f;
    private bool isDead = false;
    private int timesDied = 0;
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
        timesDied++;
    }
    private void DeadFR()
    {
        if (timesDied >= 3) Destroy(enemy);
    }
    private void FixedUpdate()
    {
        DeadFR();
    }

}
