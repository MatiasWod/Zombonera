using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    public NavMeshAgent policia;
    private AudioSource shooting;

    public Transform player;

    public LayerMask whatIsPlayer;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject bullet;
    public float attackRange;

    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        policia = GetComponent<NavMeshAgent>();
        shooting = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        policia.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        policia.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            shooting.Play();
            Instantiate(bullet, transform.position + transform.forward *2  , transform.rotation);
            print("atacando");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
