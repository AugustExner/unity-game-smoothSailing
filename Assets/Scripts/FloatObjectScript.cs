using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FloatObjectScript : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f;
    public float downForceOnImpact = 10f;
    
  

    float forceFactor;
    Vector3 floatForce;

    private Animator animator;
    public PlayerHealth playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

        if (forceFactor > 0.0f )
        {
            floatForce = -Physics.gravity * GetComponent<Rigidbody>().mass *(forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
            floatForce += new Vector3(0.0f, -downForce * GetComponent<Rigidbody>().mass, 0.0f);
            GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Stone") || HasParentWithTag(collision.gameObject, "Stone"))
    //    {
    //        Debug.Log("Animation Start: SinkAnimation");
    //        animator.SetBool("startSink", true);
    //        // Drown
    //        Debug.Log("Drown Player");
    //        DrownPlayer();
    //    }
    //}

    //private bool HasParentWithTag(GameObject obj, string tag)
    //{
    //    Transform parent = obj.transform.parent;
    //    while (parent != null)
    //    {
    //        if (parent.CompareTag(tag))
    //            return true;
    //        parent = parent.parent;
    //    }
    //    return false;
    //}


    //void DrownPlayer()
    //{
    //    if (playerHealth != null)
    //    {
    //        Debug.Log("Attack Player");
    //        //forloop 1 sec
    //        playerHealth.TakeDamage(10);
    //    }
    //}


}
