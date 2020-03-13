using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetStatusBars : MonoBehaviour
{
    private int maxHealth;
    private float currentHealth;
    private Image health;
    private Text healthText;
    private Text targetName;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponentInChildren<Image>();
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        for(int i = 0; i < texts.Length; i++)
        {
            if (texts[i].name.Equals("Health"))
                healthText = texts[i];
            else
                targetName = texts[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.SelectedEnemy != null)
        {
            health.gameObject.SetActive(true);
            healthText.gameObject.SetActive(true);
            targetName.gameObject.SetActive(true);
            maxHealth = GameManager.SelectedEnemy.GetMaxHealth();
            currentHealth = GameManager.SelectedEnemy.GetCurrentHealth();
            health.fillAmount = currentHealth / maxHealth;
            healthText.text = (int)currentHealth + " HP";
        }
        else
        {
            health.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
            targetName.gameObject.SetActive(false);
        }
    }
}
