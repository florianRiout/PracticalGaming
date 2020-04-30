using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : HealthSystem
{
    private NavMeshAgent navMesh;
    private Transform player;
    Animator animatorParameter;

    Outline myScript;

    public GameObject potionPrefab;

    void Start()
    {
        myScript = gameObject.GetComponent<Outline>();
        myScript.enabled = false;
        navMesh = GetComponent<NavMeshAgent>();
        player = GameManager.Player.transform;
        animatorParameter = GetComponent<Animator>();
        MaxHealth = 2500;
        CurrentHealth = MaxHealth;
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            Die();
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

    private void Die()
    {
        GameObject pot = Instantiate(potionPrefab) as GameObject;
        pot.transform.position = this.transform.position;
        GameManager.Items.Add(pot);

        Destroy(this.gameObject);
    }
}
