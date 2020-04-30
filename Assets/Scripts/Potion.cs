using UnityEngine;
using System;

public class Potion : MonoBehaviour, IItem, IInteractable
{
    private int health;

    // Use this for initialization
    void Start()
    {
        health = UnityEngine.Random.Range(100, 500);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Drop()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        GameManager.Player.Heal(health);
    }

    public void Pick()
    {
        GameManager.Potions.Add(this);
    }

    public void Interact()
    {
        Pick();
    }
}
