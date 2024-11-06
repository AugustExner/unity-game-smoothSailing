using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

        if (currentHealth <= 0)
        {
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
