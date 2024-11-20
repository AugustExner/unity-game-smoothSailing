using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class CoconutScript : MonoBehaviour
{

    public GameObject shark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shark)
        {
            Destroy(gameObject);
            Debug.Log("YESS U STUNNED");
        }
    }
}
