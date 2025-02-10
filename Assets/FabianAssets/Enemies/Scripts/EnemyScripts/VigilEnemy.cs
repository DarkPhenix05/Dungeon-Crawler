using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VigilEnemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject proyectile; 

    public LayerMask whatIsGround, whatIsPlayer;

    // patrulla 

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        //checa la vista y el rango del ataque

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange) Patroling(); 
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint alcanzado 

        if (distanceToWalkPoint.magnitude <1f)
        walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Asegurandose que los enemigos no se muevan 

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //codigo de ataque 
            Rigidbody rb = Instantiate(proyectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 20f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); 
        
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; 
    }
}
