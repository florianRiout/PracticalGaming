using UnityEngine;
using UnityEngine.UI;

public class StatusBars : MonoBehaviour
{
    private int maxHealth;
    private float currentHealth;
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
        maxHealth = Player.MaxHealth;
        currentHealth = Player.CurrentHealth;
        health.fillAmount = currentHealth / maxHealth;
        healthText.text = (int)currentHealth + " HP";
    }
}
