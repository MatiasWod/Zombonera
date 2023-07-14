using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour, IEnemy
{

    public NavMeshAgent policia;
    private AudioSource shooting;
    private Animator animator;

    public Transform player;

    public LayerMask whatIsPlayer;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject bullet;
    public float attackRange;

    public bool playerInAttackRange;

    private AudioSource _death_sound_source;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        policia = GetComponent<NavMeshAgent>();
        shooting = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        _death_sound_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        animator.SetBool("InShootingRange", true);
        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            animator.SetBool("InShootingRange", false);
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
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Stop()
    {
        policia.isStopped = true;
        policia.speed = 0;
        GetComponent<Collider>().enabled = false;
        _death_sound_source.Play();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
