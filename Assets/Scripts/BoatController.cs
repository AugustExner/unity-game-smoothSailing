using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;


[RequireComponent(typeof(FloatObjectScript))]
public class BoatController : MonoBehaviour
{
    public Vector3 COM;
    [Space(15)]

    public float speed = 1.0f;
    public float steeringSpeed = 1.0f;
    public float movementThreshold = 2.0f;

    Transform m_COM;

    float verticalInput;
    float movementFactor;

    float horizontalInput;
    float steeringFactor;


    // Update is called once per frame
    void Update()
    {
        Balance();
        //Movement(); 
        Steering();
    }

    void Balance()
    {
        if (m_COM == null) {
            m_COM = new GameObject("COM").transform;
            m_COM.SetParent(transform);
        }

        m_COM.position = COM;
        GetComponent<Rigidbody>().centerOfMass = m_COM.position;
    }

    void Movement()
    {
        verticalInput = UnityEngine.Input.GetAxis("Vertical");
        movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold);
        transform.Translate(-(movementFactor * speed), 0.0f, 0.0f);
    }

    void Steering()
    {
        horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        steeringFactor = Mathf.Lerp(steeringFactor, horizontalInput, Time.deltaTime / movementThreshold);
        transform.Rotate(0.0f,steeringFactor * steeringSpeed, 0.0f);
    }
}
