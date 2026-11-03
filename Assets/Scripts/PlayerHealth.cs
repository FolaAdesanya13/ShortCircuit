using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    protected static int health = 250;
    private const int maxHealth = 250;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = 1;
        }
    }

    public void TakeDmg(int dmg)
    {
        health -= dmg;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }
}
