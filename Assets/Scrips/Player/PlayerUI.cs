using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    PlayerManager manager;

    public HealthBar healthBar;
    public Text lives;
    public Text coins;
    public Text potions;
    public Text fireballs;

    int maxHealth;
    float currentHealth;
    int currentLives;
    

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        lives.text = $"{manager.combet.currentLives}";
        coins.text = $"{PlayerManager.Coins}";
        potions.text = $"{PlayerCombet.healthPotion}";
        fireballs.text = $"{manager.combet.fireBalls.Count}";
        maxHealth = PlayerCombet.maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        lives.text = $"{manager.combet.currentLives}";
        coins.text = $"{PlayerManager.Coins}";
        potions.text = $"{PlayerCombet.healthPotion}";
        fireballs.text = $"{manager.combet.fireBalls.Count}";
        currentHealth = manager.combet.currentHealth;
        currentLives = manager.combet.currentLives;

        healthBar.SetHealth(currentHealth);
        lives.text = $"X {currentLives}";
    }
}

