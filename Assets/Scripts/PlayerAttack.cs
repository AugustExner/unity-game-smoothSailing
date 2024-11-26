using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int maxAmmo = 10;
    public int currentAmmo;

    public CoconutCounter coconutCounter;
    public GameObject coconutObject;


    [SerializeField] private AudioClip coconutSpawnSound;

    void Start()
    {
        currentAmmo = maxAmmo;
        coconutCounter.SetCoconuts(maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && currentAmmo > 0)
        {

            UseCoconut();
            SpawnCoconut();
        }

    }

    void UseCoconut()
    {
        currentAmmo -= 1;

        coconutCounter.SetCoconuts(currentAmmo);
    }

    void SpawnCoconut()
    {
        Vector3 spawnPosition = transform.position - (-transform.right * 2.2f);
        GameObject spawnObject = Instantiate(coconutObject, spawnPosition, Quaternion.identity);
        spawnObject.layer = 3;

        SoundFXManager.instance.PlaySoundFXClip(coconutSpawnSound, transform, 1f);
    }
}

