using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health = 50f;

    public void TakeDamage(float amt)
    {
        health -= amt;
        if(health <= 0)
        {
            Debug.Log(Die());
        }
    }

    private String Die()
    {
        return "Dead";
    }

}
