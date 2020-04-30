using System;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    protected int MaxHealth;
    protected float CurrentHealth;
    protected float HealthPerSec;

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public void Heal(float value)
    {
        CurrentHealth += value;
    }
}
