using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Transform player;
    Animator animatorParameter;

    private int maxHealth;
    private float currentHealth;

    Outline myScript;

    void Start()
    {
        myScript = gameObject.GetComponent<Outline>();
        myScript.enabled = false;
        navMesh = GetComponent<NavMeshAgent>();
        player = GameManager.Player.transform;
        animatorParameter = GetComponent<Animator>();
        maxHealth = 2500;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 20 && Vector3.Distance(player.position, transform.position) > navMesh.stoppingDistance)
        {
            animatorParameter.SetBool("isWalking", true);
            navMesh.SetDestination(player.position);
        }
        else
        {
            animatorParameter.SetBool("isWalking", false);
            navMesh.SetDestination(transform.position);
        }
    }

    internal void Select()
    {
        myScript.enabled = true;
    }

    internal void Unselect()
    {
        myScript.enabled = false;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
