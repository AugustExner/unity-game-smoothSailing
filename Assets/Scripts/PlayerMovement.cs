using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHomeMade : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    float speedX, speedZ;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedZ = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.velocity = new Vector3 (speedX, 0, speedZ);
    }
}
