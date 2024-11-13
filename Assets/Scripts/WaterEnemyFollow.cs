using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    private Vector3 lastPatrolpoint;
    private float distanceToPatrolPoint;
    public CinemachineDollyCart dollyCart;

    public float walkPointRange;

    private bool isWalkPointSet;
    private bool isPatroling;
    private bool isPatrolpointStored;

    private float patrolPointReachedThreshold = 2;
    public LayerMask whatIsWater;

    private GameObject enemyTrackPrefab; 
    private GameObject currentEnemyTrack;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player").transform;
        isPatrolpointStored = false;
       


        // Ensure to assign EnemyTrack prefab from Inspector
        //enemyTrackPrefab = GameObject.FindWithTag("EnemyTrack");




        // Store the initial Y position of the shark
        initialYPosition = transform.position.y;
      
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate distance to the player
        distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);

        // If the player is not too close and within sight, chase them; otherwise, patrol
        if (distanceToPlayer > closestDistanceToPlayer && distanceToPlayer <= playerInSight)
        {
            if (!isPatrolpointStored)
            {
                lastPatrolpoint = transform.position;
                isPatrolpointStored = true;
              
            }
            deactivatePatrol();
            GoToPlayer();


        }
        else if (distanceToPlayer > closestDistanceToPlayer && distanceToPlayer >= playerInSight) //Player not in sight
        {
            activatePatrol();
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

    private void deactivatePatrol()
    {
        agent.enabled = true;

        if (dollyCart != null)
        {
            dollyCart.enabled = false;
        }
    }

    private void activatePatrol()
    {
        distanceToPatrolPoint = Vector3.Distance(lastPatrolpoint, transform.position);
        Debug.Log("distance to Patrol Point -->: " + distanceToPatrolPoint);

        if (dollyCart.enabled == false && isPatrolpointStored) {
            agent.SetDestination(lastPatrolpoint);
        }

       if (distanceToPatrolPoint <= 2 )
        {
            dollyCart.enabled = true;
            isPatrolpointStored = false;
            agent.enabled = false;
        }
    }

   // private void destroyEnemyTrack()
   // {
   //     if (enemyTrackPrefab != null)
   //     {
   //         Destroy(enemyTrackPrefab);
   //         enemyTrackPrefab = null;
   //     }
   // }
   //
   // private void createEnemeyTrack()
   // {
   //     if (enemyTrackPrefab == null)
   //     {
   //         Vector3 trackPosition = new Vector3(transform.position.x, initialYPosition, transform.position.z);
   //         enemyTrackPrefab = Instantiate(enemyTrackPrefab, trackPosition, Quaternion.identity);
   //     }
   // }


   //     private void Patrol()
   //     {
   //         // If no walk point is set, generate a new one
   //         if (!isWalkPointSet)
   //         {
   //             SetNewWalkPoint();
   //         }
   //
   //         // Check if agent is close enough to the walk point
   //         if (isWalkPointSet && Vector3.Distance(agent.transform.position, walkPoint) <= patrolPointReachedThreshold)
   //         {
   //             isWalkPointSet = false; // Walk point reached, ready to set a new one
   //         }
   //
   //         // Keep moving towards the current walk point, but fix Y position
   //         if (isWalkPointSet)
   //         {
   //             Vector3 fixedWalkPoint = new Vector3(walkPoint.x, initialYPosition, walkPoint.z);
   //             agent.SetDestination(fixedWalkPoint);
   //         }
   //     }
    


    // Method to generate a new walk point
   // private void SetNewWalkPoint()
   // {
   //     // Generate a random X and Z, but keep Y fixed
   //     float randomZ = Random.Range(-walkPointRange, walkPointRange);
   //     float randomX = Random.Range(-walkPointRange, walkPointRange);
   //
   //     // New walk point based on current position and random offsets, but with fixed Y position
   //     walkPoint = new Vector3(transform.position.x + randomX, initialYPosition, transform.position.z + randomZ);
   //
   //     // Ensure the walk point is valid based on the water layer
   //     if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsWater))
   //     {
   //         isWalkPointSet = true;
   //     }
   // }
}