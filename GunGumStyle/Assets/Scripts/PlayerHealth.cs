using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 3; 
    public float currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void AddHealth()
    {
        if(currentHealth < 6)
        {
            currentHealth += 1;
        }

        if(currentHealth > 6) { currentHealth =6; }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("Player has died");
        }
    }
}