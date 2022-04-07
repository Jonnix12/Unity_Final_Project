using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUi : MonoBehaviour
{
    AiCombat AiCombat;

    public HealthBar healthBar;

    int maxHealth;
    float currentHealth;

    private void Awake()
    {
        AiCombat = GetComponent<AiCombat>();
    }

    private void Start()
    {
        maxHealth = ((int)AiCombat.health);
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        currentHealth = (int)AiCombat.health;

        healthBar.SetHealth(currentHealth);
    }
}
