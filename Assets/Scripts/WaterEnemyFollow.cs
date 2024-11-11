using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private float initialYPosition;

    public float playerInSight;
    public float closestDistanceToPlayer;

    private float distanceToPlayer;

    private Vector3 walkPoint;
    private Vector3 remainingDistance;
    public float walkPointRange;

    private bool isWalkPointSet;
    private float patrolPointReachedThreshold = 2;
    public LayerMask whatIsWater;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        // Store the initial Y position of the shark
        initialYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the players position 
        Vector3 playerPosition = player.position;

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
        // Get player's position but keep the shark's Y position fixed
        Vector3 playerPosition = new Vector3(player.position.x, initialYPosition, player.position.z);

        // Set the shark's destination to the player's X-Z position, but fixed Y position
        agent.SetDestination(playerPosition);

        // Adjust remainingDistance but keep Y fixed
        remainingDistance = new Vector3(transform.position.x + 2.0f, initialYPosition, transform.position.z + 2.0f);

        if (distanceToPlayer <= closestDistanceToPlayer)
        {
            agent.SetDestination(playerPosition + remainingDistance);
        }
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

        // Keep moving towards the current walk point, but fix Y position
        if (isWalkPointSet)
        {
            Vector3 fixedWalkPoint = new Vector3(walkPoint.x, initialYPosition, walkPoint.z);
            agent.SetDestination(fixedWalkPoint);
        }
    }


    // Method to generate a new walk point
    private void SetNewWalkPoint()
    {
        // Generate a random X and Z, but keep Y fixed
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // New walk point based on current position and random offsets, but with fixed Y position
        walkPoint = new Vector3(transform.position.x + randomX, initialYPosition, transform.position.z + randomZ);

        // Ensure the walk point is valid based on the water layer
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsWater))
        {
            isWalkPointSet = true;
        }
    }
}