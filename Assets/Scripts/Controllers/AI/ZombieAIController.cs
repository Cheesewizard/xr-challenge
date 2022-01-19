using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;

    private Transform target;

    private Enemy enemy;

    public int range = 5;
    public int aggroRange;
    public Vector3 startPos;
    private bool alreadyAttacked;

    [SerializeField]
    private int attackDelay = 5;

    [SerializeField]
    private float attackDistance;

    private

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();

        // Store the start position of the zombie
        startPos = transform.position;
        InvokeRepeating("CheckDistance", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ChaseTarget();
    }

    private void CheckDistance()
    {
        var dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist < range)
        {
            target = player;
        }
        else if (dist > aggroRange)
        {
            target = null;
        }

        if (dist < attackDistance)
        {
            Attack();
        }
    }

    private void ChaseTarget()
    {
        if (target != null)
        {
            // Move towards the player
            agent.destination = target.transform.position;
        }
        else if (agent.destination != startPos)
        {
            // Return back to its original start position
            agent.destination = startPos;
        }

        // Animates the zombie
        AnimatorEventManager.Instance.EnemyMove(enemy, agent.transform.position.magnitude);
    }

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            AnimatorEventManager.Instance.EnemyAttack(enemy);
            enemy.DoDamage();
            alreadyAttacked = true;

            // Reset the attack bool after x seconds
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // This is my preffered method, but I cant seem to get it working correctly
            //enemy.DoDamage();
        }
    }
}

