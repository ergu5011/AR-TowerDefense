using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private float damageTaken;
    private float score;

    // Interface for taking damage
    public void Damage(float damageAmount)
    {
        Damaged();
        damageTaken = damageAmount;
    }

    private void Damaged()
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    private void Defeat()
    {

    }
}
