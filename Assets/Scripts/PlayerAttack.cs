using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public int maxAmmo = 10;
    public int currentAmmo;

    public CoconutCounter coconutCounter;
    public GameObject coconutObject;
    private GameObject player;


    [SerializeField] private AudioClip coconutSpawnSound;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (PlayerPrefs.HasKey("CoconutCounter"))
        {
            currentAmmo = PlayerPrefs.GetInt("CoconutCounter");
        } else
        {
            currentAmmo = maxAmmo;
        }
        coconutCounter.SetCoconuts(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentAmmo > 0)
        {

            UseCoconut();
            SpawnCoconut();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentAmmo > 0)
        {
            UseCoconut();
            EatCoconut();
        }
    }

    void UseCoconut()
    {
        currentAmmo -= 1;

        coconutCounter.SetCoconuts(currentAmmo);
    }

    void SpawnCoconut()
    {
        Vector3 spawnPosition = transform.position - (-transform.right * 2.4f);
        Instantiate(coconutObject, spawnPosition, Random.rotation);

        SoundFXManager.instance.PlaySoundFXClip(coconutSpawnSound, transform, 1f);
    }

    void EatCoconut()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.HealPlayer(2);
        }
    }
}

