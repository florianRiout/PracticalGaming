using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
    private int maxHealth;
    private float currentHealth;
    private float healthPerSec;
    private Image health;
    private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponentInChildren<Image>();
        healthText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthPerSec = Stats.HealthPerSec;
        maxHealth = Stats.MaxHealth;
        currentHealth = Stats.CurrentHealth;
        health.fillAmount = currentHealth / maxHealth;
        healthText.text = (int)currentHealth + " HP";
    }
}
