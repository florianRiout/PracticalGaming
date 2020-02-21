﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Transform player;
    Animator animatorParameter;

    public static int MaxHealth { get; set; }
    public static float CurrentHealth { get; set; }

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }
}
