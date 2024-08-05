using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float thrust;
    [SerializeField] private float rotationSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float speed)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(speed * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }
    }
}
