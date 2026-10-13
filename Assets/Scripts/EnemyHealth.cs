using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private float health = 50f;
    private const float initialHealth = 50f;
    private bool isDead = false;
    private int timesDied = 0;
    [SerializeField] private GameObject explosionEffect;
    public void TakeDamage(float amt)
    {
        health -= amt;
        if(health <= 0)
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
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
        if (timesDied >= 10) Destroy(enemy);
    }
    private void FixedUpdate()
    {
        DeadFR();
    }

}
