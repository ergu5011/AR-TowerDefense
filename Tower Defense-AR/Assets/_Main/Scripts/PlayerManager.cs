using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IDamageable
{
    private float maxHealth = 100f;
    private float currentHealth = 100f;
    private float damageTaken;
    private float score;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private UnityEvent onDefeat;

    private void Start()
    {
        fill.color = gradient.Evaluate(1f);
    }

    // Interface for taking damage
    public void Damage(float damageAmount)
    {
        Damaged();
        damageTaken = damageAmount;
    }

    private void Damaged()
    {
        currentHealth -= damageTaken;

        slider.value = currentHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (currentHealth <= 0)
        {
            Defeat();
        }
    }

    private void Defeat()
    {
        onDefeat.Invoke();

        Time.timeScale = 0f;
    }
}
