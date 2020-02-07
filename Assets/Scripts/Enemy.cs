using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Transform player;
    Animator animatorParameter;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animatorParameter = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 40)
        {
            animatorParameter.SetBool("isWalking", true);
            navMesh.SetDestination(player.position);
        }
        else
            animatorParameter.SetBool("isWalking", false);
    }
}
