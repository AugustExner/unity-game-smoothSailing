using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject gameOverScreen;
    private BoatController boatController;

    public HealthBar healthBar;
    private Animator animator;



    private void Awake()
    {
        // Locate the "Wood_BoatV1" child and get the BoatController component
        GameObject woodBoat = GameObject.Find("Wood_BoatV1");
        if (woodBoat != null)
        {
            boatController = woodBoat.GetComponent<BoatController>();
        }
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }

        Debug.Log(currentHealth);
        if (currentHealth <= 0) {
            //Activate Game Over screen 
            GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }


    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

        //Deactivate BoatController
        if (boatController != null)
        {
            boatController.enabled = false;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone") || HasParentWithTag(collision.gameObject, "Stone"))
        {
            Debug.Log("Animation Start: SinkAnimation");
            animator.SetBool("startSink", true);
            // Drown
            Debug.Log("Drown Player");
            DrownPlayer();
        }
    }

    private bool HasParentWithTag(GameObject obj, string tag)
    {
        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            if (parent.CompareTag(tag))
                return true;
            parent = parent.parent;
        }
        return false;
    }


    void DrownPlayer()
    {
        StartCoroutine(TakeDamageEachSecond());
    }

    IEnumerator TakeDamageEachSecond()
    {
        for (int i = 0; i < 10; i++)
        {
            TakeDamage(1); 
            yield return new WaitForSeconds(1f); 
        }
    }



}
