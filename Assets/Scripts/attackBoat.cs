using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class AttackBoat : MonoBehaviour
{
    private NavMeshAgent shark;
    private GameObject player;

    [SerializeField] private AudioClip sharkAttackClip;
    [SerializeField] private AudioClip sharkAttackTurtleClip;


    public int attackDamage = 5;
    public float attackRange = 3f;
    public float attackCooldown = 10f;  // Cooldown duration 
    private float distanceToPlayer;
    private float lastAttackTime = 0f;  // Time of the last attack

    public AllTurtles allTurtles;


    // Start is called before the first frame update
    void Start()
    {
        shark = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        lastAttackTime = Time.time - attackCooldown;
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

        // Check if within range and if cooldown period has passed
        if (GetDistance(player.transform.position) < attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            //Debug.Log("Distance to player: " + (distanceToPlayer < attackRange));

            DamagePlayer();
            lastAttackTime = Time.time;  // Reset the attack cooldown timer

            //Debug.Log("Time.deltatTime >= lastAttackTime + attackCooldown: " + (Time.deltaTime >= lastAttackTime + attackCooldown));
            //Debug.Log("Time.deltaTime: " + Time.deltaTime);

        }

        if (lowestDistanceToTurtle < attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            DamageTurtle(closestsTurtle);
            lastAttackTime = Time.time;
        }
    }

    private float GetDistance(Vector3 pos)
    {
        return Vector3.Distance(shark.transform.position, pos);
    }

    void DamageTurtle(GameObject turtle)
    {
        TurtleHealth turtleHealth = turtle.GetComponent<TurtleHealth>();
        turtleHealth.TakeDamage(attackDamage);
        Debug.Log("Attempted to eat turtle");
        SoundFXManager.instance.PlaySoundFXClip(sharkAttackTurtleClip, transform, 0.3f);
    }

    void DamagePlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Attack Player");
            playerHealth.TakeDamage(attackDamage);
            SoundFXManager.instance.PlaySoundFXClip(sharkAttackClip, transform, 0.3f);
        }
        else { Debug.Log("no enemey attacked"); 
        }
        
        
    }
}
