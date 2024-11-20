using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurtleHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public ParticleSystem deathParticles;
    public AllTurtles allTurtles;

    [SerializeField] private AudioClip deadClip;
    [SerializeField] private float soundVolume;



    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage(1);
        }

        if (currentHealth <= 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(deadClip, transform, soundVolume);
            Quaternion rotationOffset = Quaternion.Euler(-90, 0, 0);
            Instantiate(deathParticles, transform.position , rotationOffset);
            Destroy(gameObject);
            allTurtles.TurtleDead(gameObject);
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Turtle took damage");
    }
}
