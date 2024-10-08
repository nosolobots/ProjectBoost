using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    Vector3 startingPosition;
    float movementFactor;    

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {   
        if (period <= Mathf.Epsilon) { return; } // protect against period is zero
        
        float cycles = Time.time / period; // constinually growing over time

        const float tau = Mathf.PI * 2; 
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;   
        transform.position = startingPosition + offset;
    }
}
