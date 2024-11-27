using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemyFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;


    public float playerInSight;
    public float closestDistanceToPlayer;

    public float walkPointRange;


    private float patrolPointReachedThreshold = 2f;
    public LayerMask whatIsWater;

    public float distanceToATurtle;
    public AllTurtles allTurtles;


    private Vector3 lastPatrolpoint;
    private float distanceToPatrolPoint;
    public CinemachineDollyCart dollyCart;

    private bool isPatrolpointStored = false;

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
        float lowestDistanceToTurtle = 1000;
        List<GameObject> list = allTurtles.GetTurtlesList();
        {
            foreach (GameObject turtle in list)
            {
                if (turtle != null)
                {
                    float distance = GetDistance(turtle.transform.position);
                    if (distance < lowestDistanceToTurtle)
                    {
                        lowestDistanceToTurtle = distance;
                        closestsTurtle = turtle;
                    }
                }

            }
        }

        // If the player is not too close and within sight, chase them; otherwise, patrol
        if (GetDistance(player.position) > closestDistanceToPlayer && GetDistance(player.position) <= playerInSight)
        {
            if (!isPatrolpointStored) 
            {
                lastPatrolpoint = transform.position;
                isPatrolpointStored = true;
            }
            DeactivatePatrol();
            GoToObject(player.position);
            Debug.Log("Going to player");
        }
        if (lowestDistanceToTurtle < distanceToATurtle)
        {
            if (closestsTurtle != null)
            {
                GoToObject(closestsTurtle.transform.position);
                //Debug.Log("Going to Turtle");
                //Debug.Log(closestsTurtle);
            }
        }
        else if (GetDistance(player.position) > closestDistanceToPlayer && GetDistance(player.position) >= playerInSight)
        {
            ActivatePatrol();
        }
    }

    private float GetDistance(Vector3 pos)
    {
        return Vector3.Distance(agent.transform.position, pos);
    }

    private void GoToObject(Vector3 obj)
    {
        if (agent != null)
        {
            agent.SetDestination(obj);
        }
    }


    private void ActivatePatrol()
    {
        distanceToPatrolPoint = Vector3.Distance(lastPatrolpoint, transform.position);
        //Debug.Log("distance to Patrol Point: " + distanceToPatrolPoint);

        if (dollyCart.enabled == false && isPatrolpointStored)
        {
            agent.SetDestination(lastPatrolpoint);
        }

        if (distanceToPatrolPoint <= patrolPointReachedThreshold)
        {
            dollyCart.enabled = true;
            isPatrolpointStored = false;
            agent.enabled = false;
        }
    }


    private void DeactivatePatrol()
    {
        agent.enabled = true;

        if (dollyCart != null)
        {
            dollyCart.enabled = false;
        }
    }
}