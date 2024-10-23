using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    public float playerInSight;
    public float closestDistanceToPlayer;

    private float distanceToPlayer;

    private Vector3 walkPoint;
    public float walkPointRange;

    private bool isWalkPointSet;
    private float patrolPointReachedThreshold = 5; 
    public LayerMask whatIsWater;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance to the player
        distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

        // If the player is not too close and within sight, chase them; otherwise, patrol
        if (distanceToPlayer > closestDistanceToPlayer && distanceToPlayer <= playerInSight)
        {
            GoToPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void GoToPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Patrol()
    {
        // If no walk point is set, generate a new one
        if (!isWalkPointSet)
        {
            SetNewWalkPoint();
        }

        // Check if agent is close enough to the walk point
        if (isWalkPointSet && Vector3.Distance(agent.transform.position, walkPoint) <= patrolPointReachedThreshold)
        {
            isWalkPointSet = false; // Walk point reached, ready to set a new one

          
        }

        // Keep moving towards the current walk point
        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
    }



    // Method to generate a new patrol point
    private void SetNewWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // New walk point based on current position and random offsets
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsWater))
        {
            isWalkPointSet = false;
        }
        else { isWalkPointSet = true; }
        
    }
}
