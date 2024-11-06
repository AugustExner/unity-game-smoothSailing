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


    public int attackDamage = 5;
    public float attackRange = 3f;
    public float attackCooldown = 10f;  // Cooldown duration 
    private float distanceToPlayer;
    private float lastAttackTime = 0f;  // Time of the last attack


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
        GetDistanceToPlayer();

        // Check if within range and if cooldown period has passed
        if (distanceToPlayer < attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            //Debug.Log("Distance to player: " + (distanceToPlayer < attackRange));

            DamagePlayer();
            lastAttackTime = Time.time;  // Reset the attack cooldown timer

            //Debug.Log("Time.deltatTime >= lastAttackTime + attackCooldown: " + (Time.deltaTime >= lastAttackTime + attackCooldown));
            //Debug.Log("Time.deltaTime: " + Time.deltaTime);

        }
    }

    void GetDistanceToPlayer()
    {
        // Calculate distance to the player
        distanceToPlayer = Vector3.Distance(shark.transform.position, player.transform.position);
    }

    void DamagePlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Attack Player");
            playerHealth.TakeDamage(attackDamage);
        }
        else { Debug.Log("no enemey attacked"); 
        }
        
        
    }
}
