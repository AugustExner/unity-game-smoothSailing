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
    public float distanceToATurtle;

    private float distanceToPlayer;

    private Vector3 walkPoint;
    public float walkPointRange;

    private bool isWalkPointSet;
    private float patrolPointReachedThreshold = 5; 
    public LayerMask whatIsWater;

    public AllTurtles allTurtles;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject closestsTurtle = null;
        float lowestDistanceToTurtle = 0;
        foreach(GameObject turtle in allTurtles.GetTurtlesList())
        {
            float distance = GetDistance(turtle.transform.position);
            if (distance < lowestDistanceToTurtle)
            {
                lowestDistanceToTurtle = distance;
                closestsTurtle = turtle;
            }

        }

        // If the player is not too close and within sight, chase them; otherwise, patrol
        if (GetDistance(player.position) > closestDistanceToPlayer && distanceToPlayer <= playerInSight)
        {
            GoToObject(player.position);
        }  
        if (lowestDistanceToTurtle < distanceToATurtle)
        {
            if (closestsTurtle != null)
            {
                GoToObject(closestsTurtle.transform.position);
            }
        }
        else
        {
            Patrol();
        }
    }

    private float GetDistance(Vector3 pos)
    {
        return Vector3.Distance(agent.transform.position, pos);
    }

    private void GoToObject(Vector3 obj)
    {
        agent.SetDestination(obj);
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
