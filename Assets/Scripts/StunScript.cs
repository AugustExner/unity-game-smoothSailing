using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunScript : MonoBehaviour
{
    float stunTime = 0f;
    bool isStunned = false;
    private Vector3 stunPlace;
    private float startStun = 0f;  // Time of the last attack

    public GameObject stunItem;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned && Time.time < startStun + stunTime)
        {
            transform.position = stunPlace;
        } else
        {
            isStunned = false;
        }


    }

    public void Stun(float seconds) 
    {
        isStunned = true;
        stunTime = seconds;
        stunPlace = transform.position;
        startStun = Time.time;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag(stunItem.tag))
        {
            Destroy(collision.gameObject);
            Stun(5);
        }
    }
}
