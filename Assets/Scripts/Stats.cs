using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int MaxHealth { get; set; }
    public static float CurrentHealth { get; set; }
    public static float HealthPerSec { get; set; }
    Movement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Movement>();
        HealthPerSec = 40f;
        MaxHealth = 1000;
        CurrentHealth = 200;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth += HealthPerSec * Time.deltaTime;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.CompareTag("Enemy"))
            CurrentHealth -= 100;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            HealthPerSec = 0;
            player.Die();
        }
    }
}
